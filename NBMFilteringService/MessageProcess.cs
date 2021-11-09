using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class MessageProcess
    {
        public static Dictionary<string, string> textList = new Dictionary<string, string>();
        private static bool sms = false;
        private static bool email = false;
        private static bool tweet = false;

        public static void LoadDict()
        {
            var dict = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            MessageProcess.textList = dict;
        }

        public static void CategoriseMessage(string id)
        {
            if (id.StartsWith("S"))
            {
                sms = true;
            } else if (id.StartsWith("E"))
            {
                /*Email email = new Email();
                email.ID = id;
                email.Sender = sender;
                email.Subject = subject;
                email.Text = body;
                DAO.emailList.Add(email);*/
            } else if (id.StartsWith("T"))
            {
                /*Tweet tweet = new Tweet();
                tweet.ID = id;
                tweet.Sender = sender;
                tweet.Text = body;
                DAO.tweetList.Add(tweet);*/
            }
        }

        public static void SanitiseMessage(string id, string sender, string subject, string body)
        {
            /*
             * Sanitising SMS Message
             */
            if (sms)
            {
                foreach(var dict in MessageProcess.textList)
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
            }

            /*
             * In the case algorithm was unable to detect either SMS, Email or Tweet
             */
            if(!sms && !email && !tweet)
            {
                body = "Unable to categorise message into a type";
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
    }
}
