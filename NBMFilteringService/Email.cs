using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class Email : Message
    {
        private string subject;

        public Email(string header, string subject, string sender, string text) : base(header, sender, text)
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
