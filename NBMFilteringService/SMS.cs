using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class SMS : Message
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
