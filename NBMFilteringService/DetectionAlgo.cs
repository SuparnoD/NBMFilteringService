using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class DetectionAlgo
    {
        public static Dictionary<string, string> textList = new Dictionary<string, string>();
        private static bool sms = false;
        private static bool email = false;
        private static bool tweet = false;

        public static void LoadDict()
        {
            var dict = File.ReadLines("textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            DetectionAlgo.textList = dict;
        }

        public static void CategoriseMessage(String body)
        {
            /*
             * Detecting SMS Message
             */
            foreach (var dict in DetectionAlgo.textList)
            {
                if (body.Contains(dict.Key))
                {
                    if(body.Length <= 140)
                    {
                        sms = true;
                    }
                } else if(body.Length <= 140)
                {
                    sms = true;
                }
            }
        }

        public static string SanitiseMessage(String body)
        {
            /*
             * Sanitising SMS Message
             */
            if (sms)
            {
                foreach(var dict in DetectionAlgo.textList)
                {
                    if (body.Contains(dict.Key))
                    {
                        string replacement = dict.Key + " <" + dict.Value + ">";
                        body = body.Replace(dict.Key, replacement);
                    }
                }
                body = "SMS: " + body;
            }

            /*
             * In the case algorithm was unable to detect either SMS, Email or Tweet
             */
            if(!sms && !email && !tweet)
            {
                body = "Unable to categorise message into a type";
            }
            return body;
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
