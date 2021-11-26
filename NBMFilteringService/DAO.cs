/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 15/11/2021
 * FILE NAME: DAO.cs
 * PURPOSE: Acts as an asbtract interface to lists and files containing data
 * LAYER: Data
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NBMFilteringService
{
    public class DAO
    {
        // Lists storing relevant data
        public static Dictionary<string, string> textList = new Dictionary<string, string>();
        public static List<SMS> smsList = new List<SMS>();
        public static List<Email> emailList = new List<Email>();
        public static List<Tweet> tweetList = new List<Tweet>();
        public static List<string> quarantineList = new List<string>();
        public static List<SIR> SIRList = new List<SIR>();
        public static List<string> incidentsList = new List<string>();

        public static List<string> hashtagList = new List<string>();
        public static List<string> mentionsList = new List<string>();
        public static List<Tag> groupedHashtagList = new List<Tag>();
        public static List<Tag> groupedMentionsList = new List<Tag>();

        // Populate the text list with the textspeak abbreviations contained in the textwords.csv file
        public static void LoadDict()
        {
            var dict = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "/textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            DAO.textList = dict;
        }

        // Populate the incident lists with these different types of incidents
        public static void populateIncidentList()
        {
            incidentsList.Add("theft");
            incidentsList.Add("staff attack");
            incidentsList.Add("atm theft");
            incidentsList.Add("raid");
            incidentsList.Add("customer attack");
            incidentsList.Add("staff abuse");
            incidentsList.Add("bomb threat");
            incidentsList.Add("terrorism");
            incidentsList.Add("suspicious incident");
            incidentsList.Add("intelligence");
            incidentsList.Add("cash loss");
        }

        // Deserializer for SMS obj
        public static void SMSDeserializer(string filePath)
        {
            XmlSerializer deserialiser = new XmlSerializer(typeof(List<SMS>));
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                using (FileStream perFS = File.OpenRead(filePath))
                {
                    DAO.smsList.AddRange((List<SMS>)deserialiser.Deserialize(perFS));
                }
            }
        }

        // Deserializer for Email obj
        public static void EmailDeserializer(string filePath)
        {
            XmlSerializer deserialiser = new XmlSerializer(typeof(List<Email>));
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                using (FileStream perFS = File.OpenRead(filePath))
                {
                    DAO.emailList.AddRange((List<Email>)deserialiser.Deserialize(perFS));
                }
            }
        }

        // Deserializer for Tweet obj
        public static void TweetDeserializer(string filePath)
        {
            XmlSerializer deserialiser = new XmlSerializer(typeof(List<Tweet>));
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                using (FileStream perFS = File.OpenRead(filePath))
                {
                    DAO.tweetList.AddRange((List<Tweet>)deserialiser.Deserialize(perFS));
                }
            }
        }

        // Clear all list
        public static void ClearList()
        {
            DAO.textList.Clear();
            DAO.smsList.Clear();
            DAO.emailList.Clear();
            DAO.tweetList.Clear();
            DAO.quarantineList.Clear();
            DAO.SIRList.Clear();
            DAO.incidentsList.Clear();
            DAO.hashtagList.Clear();
            DAO.mentionsList.Clear();
            DAO.groupedHashtagList.Clear();
            DAO.groupedMentionsList.Clear();
            MessageProcess.ResetType();
        }
    }
}
