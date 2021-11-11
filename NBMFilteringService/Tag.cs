using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class Tag
    {
        private string tagName;
        private int count = 1;

        public Tag(string tagName, int count)
        {
            this.tagName = tagName;
            this.count = count;
        }

        public Tag()
        {

        }

        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}
