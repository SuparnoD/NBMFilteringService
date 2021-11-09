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
        private static bool sms = false;
        private static bool email = false;
        private static bool tweet = false;

        public static void CategoriseMessage(string id)
        {
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
            }

            /*
             * Sanitising Email Message
             */
            if (email)
            {
                Email email = new Email();
                email.ID = id;
                email.Sender = sender;
                email.Subject = subject;
                email.Text = body;
                DAO.emailList.Add(email);
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
