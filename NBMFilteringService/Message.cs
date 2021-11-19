/*
 * AUTHOR: Suparna Deb
 * DATE LAST MODIFIED: 15/11/2021
 * FILE NAME: Message.cs
 * PURPOSE: Message object class - acts as a base class for SMS, Email and Tweet
 * LAYER: Business
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    public class Message
    {
        private string id;
        private string sender;
        private string text;

        public Message(string id, string sender, string text)
        {
            this.id = id;
            this.sender = sender;
            this.text = text;
        }

        public Message()
        {

        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
