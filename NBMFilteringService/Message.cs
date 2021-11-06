using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class Message
    {
        private string header;
        private string sender;
        private string text;

        public Message(string header, string sender, string text)
        {
            this.header = header;
            this.sender = sender;
            this.text = text;
        }

        public Message()
        {

        }

        public string Header
        {
            get { return header; }
            set { header = value; }
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
