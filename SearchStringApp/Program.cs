﻿using System;
using System.Collections.Generic;
using System.Linq;
using PUnit.Framework;
using PUnit.Framework.Engine;
using StringSearch;
using StringSearch.Tests;


namespace SearchStringApp
{
    /// <summary>
    /// This console application is created to demonstrate FindSearch
    /// </summary>
    class Program
    {
        /// <summary>
        /// It accepts these params
        /// --help - shows all the possible commands
        /// --default - automatically runs string search cases from the task pdf file
        /// --test - automatically runs tests from TestFindString assembly
        /// no parameter - will ask for text and substring
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("This is console application to demonstrate FindString class. For help use argument --help\n\n");
            // You can't use swtich if string isn't a constant
            // For simplicity I didn't create console command mechanism, which would allow remove these if else statements
            if (args.Contains("--help"))
                PrintHelpCommand();
            else if (args.Contains("--default"))
                ExecuteDefaultCommand();
            else if (args.Contains("--test"))
                new Runner(new ConsoleOutput()).Run(typeof(TestFindString).Assembly);
            else
                StartStringSearchComand();
            Console.WriteLine("\nPress any key to close application");
            Console.ReadKey();
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
            FindStringOutputResult(text, subtext);
        }

        private static void ExecuteDefaultCommand()
        {
            var text = "Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea";
            FindStringOutputResult(text, "Polly");
            FindStringOutputResult(text, "polly");
            FindStringOutputResult(text, "Ll");
            FindStringOutputResult(text, "ll");
            FindStringOutputResult(text, "X");
            FindStringOutputResult(text, "Polx");
            FindStringOutputResult(text, "teacups");
        }

        private static void PrintHelpCommand()
        {
            Console.WriteLine("Possible commands:");
            Console.WriteLine("--helper        to show all the commands");
            Console.WriteLine("--default       to execute default use cases by using default data");
            Console.WriteLine("--test          to run tests from TestFindString assembly");
        }

        private static void FindStringOutputResult(string text, string subtext)
        {
            List<int> indexesList = new FindString().AllIndexesOf(text, subtext);
            if (indexesList.Count > 0)
                Console.WriteLine(string.Join<int>(", ", indexesList));
            else
                Console.WriteLine("No matches");
        }
    }
}
