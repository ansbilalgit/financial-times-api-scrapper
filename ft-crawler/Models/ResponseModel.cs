using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler.Models
{



    public class Title
    {
        public string title { get; set; }
    }

    public class Lifecycle
    {
        public DateTime initialPublishDateTime { get; set; }
        public DateTime lastPublishDateTime { get; set; }
    }

    public class Nature
    {
        public string category { get; set; }
    }

    public class Location
    {
        public string uri { get; set; }
    }

    public class Summary
    {
        public string excerpt { get; set; }
    }

    public class Master
    {
        public string masterSource { get; set; }
        public string masterEntityId { get; set; }
    }

    public class Editorial
    {
        public string subheading { get; set; }
        public string byline { get; set; }
    }

    public class Provenance
    {
    }

    public class Term
    {
        public string name { get; set; }
        public string taxonomy { get; set; }
    }

    public class PrimaryTheme
    {
        public Term term { get; set; }
    }


    public class Author
    {
        public Term term { get; set; }
    }

    public class Brand
    {
        public Term term { get; set; }
    }

    public class Genre
    {
        public Term term { get; set; }
    }

    public class Organisation
    {
        public Term term { get; set; }
    }

    public class Region
    {
        public Term term { get; set; }
    }

    public class Section
    {
        public Term term { get; set; }
    }


    public class Topic
    {
        public Term term { get; set; }
    }

    public class PrimarySection
    {
        public Term term { get; set; }
    }


    public class Person
    {
        public Term term { get; set; }
    }

    public class Metadata
    {
        public PrimaryTheme primaryTheme { get; set; }
        public List<Author> authors { get; set; }
        public List<Brand> brand { get; set; }
        public List<Genre> genre { get; set; }
        public List<Organisation> organisations { get; set; }
        public List<Region> regions { get; set; }
        public List<Section> sections { get; set; }
        public List<Topic> topics { get; set; }
        public PrimarySection primarySection { get; set; }
        public List<Person> people { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Result
    {
        public string aspectSet { get; set; }
        public string modelVersion { get; set; }
        public string id { get; set; }
        public string apiUrl { get; set; }
        public Title title { get; set; }
        public Lifecycle lifecycle { get; set; }
        public Nature nature { get; set; }
        public Location location { get; set; }
        public Summary summary { get; set; }
        public Master master { get; set; }
        public Editorial editorial { get; set; }
        public Provenance provenance { get; set; }
        public Metadata metadata { get; set; }
        public List<Image> images { get; set; }
    }

    public class Query
    {
        public string queryString { get; set; }
        public QueryContext queryContext { get; set; }
        public ResultContext resultContext { get; set; }
    }


    public class FacetElement
    {
        public string name { get; set; }
        public int count { get; set; }
    }

    public class Facet
    {
        public string name { get; set; }
        public List<FacetElement> facetElements { get; set; }
    }

    public class ResponseResult
    {
        public int indexCount { get; set; }
        public List<string> curations { get; set; }
        public List<Result> results { get; set; }
        public List<Facet> facets { get; set; }
    }

    public class ResponseModel
    {
        public Query query { get; set; }
        public List<ResponseResult> results { get; set; }
    }
}
