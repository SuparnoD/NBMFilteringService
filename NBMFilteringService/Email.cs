/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 15/11/2021
 * FILE NAME: Email.cs
 * PURPOSE: Email object class
 * LAYER: Business
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    public class Email : Message
    {
        private string subject;

        public Email(string id, string subject, string sender, string text) : base(id, sender, text)
        {
            this.subject = subject;
        }

        public Email()
        {

        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
