using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFilteringService
{
    class SIR
    {
        private string sortCode;
        private string natureOfIncident;

        public SIR(string sortCode, string natureOfIncident)
        {
            this.sortCode = sortCode;
            this.natureOfIncident = natureOfIncident;
        }

        public string SortCode
        {
            get { return sortCode; }
            set { sortCode = value; }
        }

        public string NatureOfIncident
        {
            get { return natureOfIncident; }
            set { natureOfIncident = value; }
        }
    }
}
