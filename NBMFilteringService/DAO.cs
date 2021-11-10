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
        public static List<string> SIRList = new List<string>();
        public static List<string> hashtagList = new List<string>();
        public static List<string> mentionsList = new List<string>();

        public static void LoadDict()
        {
            var dict = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            DAO.textList = dict;
        }
    }
}
