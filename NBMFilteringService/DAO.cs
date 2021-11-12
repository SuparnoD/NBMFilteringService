using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class DAO
    {
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

        public static void LoadDict()
        {
            var dict = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            DAO.textList = dict;
        }

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
    }
}
