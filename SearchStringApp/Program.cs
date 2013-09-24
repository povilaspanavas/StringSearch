using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringSearch;

namespace SearchStringApp
{
    class Program
    {

        static void Main(string[] args)
        {
        
            string text = "Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea";
            var indexesList = new FindString(false).AllIndexesOf(text, "Polly");
            indexesList.ToString();
        }
    }
}
