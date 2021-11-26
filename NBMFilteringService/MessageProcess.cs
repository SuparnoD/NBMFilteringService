/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 15/11/2021
 * FILE NAME: MessageProcess.cs
 * PURPOSE: Deals with categorisation and sanitisation of a message
 * LAYER: Business
 */
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    public class MessageProcess
    {
        private static bool sms = false;
        private static bool email = false;
        private static bool tweet = false;
        private static bool sirChecked = false;
        private static string filePath = null;

        /*
         * Categorise messages according to what the ID starts with
         */
        public static void CategoriseMessage(string id)
        {
            id = id.ToUpper();
            if (id.StartsWith("S"))
            {
                sms = true;
            } else if (id.StartsWith("E"))
            {
                email = true;
            } else if (id.StartsWith("T"))
            {
                tweet = true;
            } 
        }

        /*
         * Sanitising input messages
         */
        public static void SanitiseMessage(string id, string sender, string subject, string body)
        {
            /*
             * Sanitising SMS Message
             */
            if (sms)
            {
                foreach(var dict in DAO.textList)
                {
                    if (body.Contains(dict.Key))
                    {
                        string replacement = dict.Key + " <" + dict.Value + ">";
                        body = body.Replace(dict.Key, replacement);
                    }
                }
                SMS sms = new SMS();
                sms.ID = id;
                sms.Sender = sender;
                sms.Subject = subject;
                sms.Text = body;
                DAO.smsList.Add(sms);

                try
                {
                    //Serialise into JSON
                    string path = getFilePath() + "/sms.json";

                    string json = JsonConvert.SerializeObject(sms);
                    if (File.Exists(path))
                    {
                        File.AppendAllText(path, json + Environment.NewLine);
                    }
                    else
                    {
                        File.WriteAllText(path, json + Environment.NewLine);
                    }
                } catch (Exception e)
                {

                }
            }

            /*
             * Sanitising Email Message
             */
            if (email)
            {
                string replacement = "<URL Quarantined>";

                // if email contains URL, replace the URL with <URL Quarantined> and add the URL to the quarantine list
                Regex rgx = new Regex(@"www.[^\s]+");
                foreach(Match m in rgx.Matches(body))
                {
                    DAO.quarantineList.Add(m.Value);
                }

                Regex rgx1 = new Regex(@"http[^\s]+");
                foreach (Match m in rgx1.Matches(body))
                {
                    DAO.quarantineList.Add(m.Value);
                }

                Regex rgx2 = new Regex(@"https[^\s]+");
                foreach (Match m in rgx2.Matches(body))
                {
                    DAO.quarantineList.Add(m.Value);
                }

                Regex rgx3 = new Regex(@".com[^\s]+");
                foreach (Match m in rgx3.Matches(body))
                {
                    DAO.quarantineList.Add(m.Value);
                }

                // Replace whole word containing 'www.', 'http', 'https' and '.com' with '<URL Quarantined>'
                body = Regex.Replace(body, @"www.[^\s]+", replacement);
                body = Regex.Replace(body, @"http[^\s]+", replacement);
                body = Regex.Replace(body, @"https[^\s]+", replacement);
                body = Regex.Replace(body, @".com[^\s]+", replacement);

                Email email = new Email();
                email.ID = id;
                email.Sender = sender;
                email.Subject = subject;
                email.Text = body;
                DAO.emailList.Add(email);

                string path = getFilePath() + "/email.json";

                try
                {
                    // Serialise into JSON
                    string json = JsonConvert.SerializeObject(email);
                    if (File.Exists(path))
                    {
                        File.AppendAllText(path, json + Environment.NewLine);
                    }
                    else
                    {
                        File.WriteAllText(path, json + Environment.NewLine);
                    }
                } catch (Exception e)
                {

                }
            }

            /*
             * Sanitising Tweet Message
             */
            if (tweet)
            {
                foreach (var dict in DAO.textList)
                {
                    if (body.Contains(dict.Key))
                    {
                        string replacement = dict.Key + " <" + dict.Value + ">";
                        body = body.Replace(dict.Key, replacement);
                    }
                }

                Regex rgx = new Regex(@"#[^\s]+");
                foreach (Match m in rgx.Matches(body))
                {
                    DAO.hashtagList.Add(m.Value);
                }

                Regex rgx1 = new Regex(@"@[^\s]+");
                foreach (Match m in rgx1.Matches(body))
                {
                    DAO.mentionsList.Add(m.Value);
                }

                Tweet tweet = new Tweet();
                tweet.ID = id;
                tweet.Sender = sender;
                tweet.Text = body;
                DAO.tweetList.Add(tweet);

                string path = getFilePath() + "/tweet.json";

                try
                {
                    // Serialise into JSON
                    string json = JsonConvert.SerializeObject(tweet);
                    if (File.Exists(path))
                    {
                        File.AppendAllText(path, json + Environment.NewLine);
                    }
                    else
                    {
                        File.WriteAllText(path, json + Environment.NewLine);
                    }
                } catch (Exception e)
                {

                }
            }
        }

        /*
         * Reset SMS, Email and Tweet to false
         */
        public static void ResetType()
        {
            sms = false;
            email = false;
            tweet = false;
        }

        /*
         * Groups the hashtag list by removing duplicates and increasing count
         */
        public static void GroupHashtagList()
        {
            var q = DAO.hashtagList.GroupBy(x => x)
                                   .Select(g => new { Value = g.Key, Count = g.Count() })
                                   .OrderByDescending(x => x.Count);
            foreach(var x in q)
            {
                Tag t = new Tag(x.Value, x.Count);
                DAO.groupedHashtagList.Add(t);
            }
            DAO.groupedHashtagList = DAO.groupedHashtagList.GroupBy(x => x.TagName).Select(g => g.Last()).ToList();
        }

        /*
         * Groups the mentions list by removing duplicates and increasing count
         */
        public static void GroupMentionsList()
        {
            var q = DAO.mentionsList.GroupBy(x => x)
                                   .Select(g => new { Value = g.Key, Count = g.Count() })
                                   .OrderByDescending(x => x.Count);
            foreach(var x in q)
            {
                Tag t = new Tag(x.Value, x.Count);
                DAO.groupedMentionsList.Add(t);
            }
            DAO.groupedMentionsList = DAO.groupedMentionsList.GroupBy(x => x.TagName).Select(g => g.Last()).ToList();
        }

        /*
         * Deals with the verification of SIR being (un)selected
         */
        public static void CheckSIR()
        {
            sirChecked = true;
        }

        /*
         * Deals with the verification of SIR being (un)selected
         */
        public static void uncheckSIR()
        {
            sirChecked = false;
        }

        /*
         * Deals with the verification of SIR being (un)selected
         */
        public static bool isSIR()
        {
            if (sirChecked)
            {
                return true;
            } else
            {
                return false;
            }
        }

        /*
         * Registers SIR to SIRList
         */
        public static void addSIR(string sc, string noi)
        {
            SIR sir = new SIR(sc, noi);
            DAO.SIRList.Add(sir);
        }

        /*
         * Set file path for (de)serialisation
         */
        public static void setFilePath(string path)
        {
            filePath = path;
        }

        /*
         * Get file path for (de)serialisation
         */
        public static string getFilePath()
        {
            return filePath;
        }
    }
}
