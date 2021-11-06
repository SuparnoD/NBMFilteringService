using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class SMS : Message
    {
        public SMS(string header, string sender, string text) : base(header, sender, text)
        {

        }

        public SMS()
        {

        }
    }
}
