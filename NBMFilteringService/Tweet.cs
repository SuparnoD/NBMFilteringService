using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class Tweet : Message
    {
        public Tweet(string header, string sender, string text) : base(header, sender, text)
        {

        }

        public Tweet()
        {

        }
    }
}
