using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler.Enums
{
    class Enums
    {
    }

    public static class InputFileConfigKeyEnum
    {
        public const string COMPANY = "Company";
        public const string FROM_DATE = "FromDate";
        public const string TO_DATE = "ToDate";

    }

    public static class OutputColumnEnum
    {

        public const string PUB_DATE = "Published Date";
        public const string TITLE = "Title";
        public const string SUB_HEADING = "Sub Heading";
        public const string SUMMARY = "Summary";
        public const string WEB_URL = "Web Url";
    }

    public static class ApiKeyEnum
    {
        public const string ARTICLES = "ARTICLES";
        public const string ORGANISATIONS = "organisations";
        public const string LAST_PUB_DATE = "lastPublishDateTime";
    }

    public static class AspectEnums
    {
        public const string AUDIO_VISUAL = "audioVisual";
        public const string EDITORIAL = "editorial";
        public const string IMAGES = "images";
        public const string LIFE_CYCLE = "lifecycle";
        public const string LOCATION = "location";
        public const string MASTER = "master";
        public const string METADATA = "metadata";
        public const string NATURE = "nature";
        public const string PROVENANCE = "provenance";
        public const string SUMMARY = "summary";
        public const string TITLE = "title";

    }
}
