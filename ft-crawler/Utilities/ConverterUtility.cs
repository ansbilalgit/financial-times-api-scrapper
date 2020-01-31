using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler.Utilities
{
    public class ConverterUtility
    {
        public static string ParseDate(string d)
        {
            string _date = string.Empty;
            try
            {
                _date = Convert.ToDateTime(d).ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            catch (Exception)
            {

                throw;
            }

            return _date;
        }

        internal static int GetMaxResult(string v)
        {
            int r = 5;
            int.TryParse(v, out r);
            return r;
        }

        internal static int GetDelayValue(string v)
        {
            int r = 1;
            int.TryParse(v, out r);
            return r * 1000;
        }

        internal static int GetRetryValue(string v)
        {
            int r = 5;
            int.TryParse(v, out r);
            return r;
        }

        internal static string AddDays(string d, int v)
        {
            string _date = string.Empty;
            try
            {
                _date = Convert.ToDateTime(d).AddDays(v).ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            catch (Exception)
            {

                throw;
            }

            return _date;
        }
    }
}
