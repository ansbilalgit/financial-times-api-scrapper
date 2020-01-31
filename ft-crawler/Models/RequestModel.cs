using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler.Models
{
    public class QueryContext
    {
        public List<string> curations { get; set; }

        public QueryContext()
        {
            curations = new List<string>();
        }
    }

    public class Facets
    {
        public List<string> names { get; set; }
        public int maxElements { get; set; }
        public int minThreshold { get; set; }

        public Facets()
        {
            names = new List<string>();
        }
    }

    public class ResultContext
    {
        public int maxResults { get; set; }

        public int offset { get; set; }
        public Facets facets { get; set; }
        public bool contextual { get; set; }
        public bool highlight { get; set; }
        public bool suppressDefaultSort { get; set; }
        public List<string> aspects { get; set; }

        public ResultContext()
        {
            facets = new Facets();
            aspects = new List<string>();
        }
    }

    public class RequestModel
    {
        public string queryString { get; set; }
        public QueryContext queryContext { get; set; }
        public ResultContext resultContext { get; set; }

        public RequestModel()
        {
            queryContext = new QueryContext();
            resultContext = new ResultContext();
        }
    }






}
