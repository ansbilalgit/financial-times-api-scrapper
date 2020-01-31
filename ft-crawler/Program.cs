using ft_crawler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            ParsingManager.StartParsing();

            Console.WriteLine("Finished \n Press Any key to stop");
            Console.ReadKey();
        }
    }
}
