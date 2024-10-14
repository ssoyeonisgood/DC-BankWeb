using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace APIClass
{
    public class SearchData
    {
        public string searchString { get; set; }

        public SearchData()
        {
            searchString = "";
        }
    }
}