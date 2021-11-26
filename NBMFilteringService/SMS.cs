/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 15/11/2021
 * FILE NAME: SMS.cs
 * PURPOSE: SMS object class
 * LAYER: Business
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NBMFilteringService
{
    public class SMS : Message
    {
        private string subject;

        public SMS(string id, string subject, string sender, string text) : base(id, sender, text)
        {
            this.subject = subject;
        }

        public SMS()
        {

        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
