using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler.Utilities
{
    public static class LoggerUtility
    {
        public static void WriteLog(string info, string m)
        {
            Console.WriteLine("___________________________________________");
            Console.WriteLine("Info : " + info);
            Console.WriteLine("Log Message : " + m);
            Console.WriteLine("____________________________________________");
        }
    }
}
