using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringSearch;

namespace SearchStringApp
{
    /// <summary>
    /// This console application is created  to demonstrate FindSearch
    /// 
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        /// It accepts these params
        /// --help - shows all the possible commands
        /// --default - automatically runs string search cases from the task pdf file
        /// no parameter - will ask for text and substring
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("This is console application to demonstrate FindString class. For help use argument --help");
            if (args.Contains("--help"))
                PrintHelpCommand();
            if (args.Contains("--default"))
                ExecuteDefaultCommand();
            StartStringSearchComand();
            //Console.ReadKey();

        }

        private static void StartStringSearchComand()
        {
            Console.WriteLine("You will be asked to input two strings");

            // Reading input
            Console.WriteLine("Firstly, input the text and press enter.");
            var text = Console.ReadLine();
            Console.WriteLine("Now, input the subtext");
            var subtext = Console.ReadLine();

            // Printing out the result
            List<int> indexesList = new FindString().AllIndexesOf(text, subtext);
            if (indexesList.Count > 0)
                Console.WriteLine(string.Join<int>(", ", indexesList)); 
            else
                Console.WriteLine("No matches");
        }

        private static void ExecuteDefaultCommand()
        {
            throw new NotImplementedException();
        }

        private static void PrintHelpCommand()
        {
            Console.WriteLine("Possible commands:");
            Console.WriteLine("--helper        to show all the commands");
            Console.WriteLine("--default       to execute default use cases by using default data");
        }
    }
}
