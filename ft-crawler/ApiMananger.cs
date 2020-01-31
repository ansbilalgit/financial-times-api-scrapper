using ft_crawler.Enums;
using ft_crawler.Models;
using ft_crawler.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace ft_crawler
{
    public class ApiMananger
    {
        private static int _retry = ConverterUtility.GetRetryValue(ConfigurationManager.AppSettings["Retry"]);
        private static int _maxResult = ConverterUtility.GetMaxResult(ConfigurationManager.AppSettings["MaxResult"]);
        private static int _delay = ConverterUtility.GetDelayValue(ConfigurationManager.AppSettings["Delay"]);
        private static NameValueCollection _facetCollection = new NameValueCollection();

        public static List<Result> GetNewsList(string company, string fromDate, string toDate)
        {
            List<Result> newsResult = new List<Result>();
            try
            {

                int retry = _retry;
                string _fDate = ConverterUtility.ParseDate(fromDate);
                string _tDate = ConverterUtility.ParseDate(toDate);
                string facet = getCompanyFacet(company);
                while (retry > 0)
                {
                    LoggerUtility.WriteLog("Searching Data: ", "Params : \n Company :" + company + "\n Facet : " + facet + "\n From Date : " + _fDate + "\n To Date : " + _tDate + "\n Retry : " + retry);
                    var _result = getNewsList(company, facet, _fDate, _tDate);
                    Thread.Sleep(_delay);
                    if (_result != null && _result.Count > 0)
                    {
                        newsResult = _result;
                        retry = 0;
                    }
                    else
                    {
                        retry--;
                        _fDate = ConverterUtility.AddDays(_fDate, -2);
                        _tDate = ConverterUtility.AddDays(_tDate, 2);
                    }
                }


            }
            catch (Exception ex)
            {
                LoggerUtility.WriteLog("Error in Getting News List", ex.Message);
            }
            return newsResult;
        }

        private static List<Result> getNewsList(string company, string facet, string fDate, string tDate)
        {
            List<Result> results = new List<Result>();
            try
            {
                RequestModel newsRequest = getNewsRequestModel(company, facet, fDate, tDate);

                var json = JsonConvert.SerializeObject(newsRequest);
                string response = HttpUtility.GetApiResponseAsync(json);
                ResponseModel apiResponse = JsonConvert.DeserializeObject<ResponseModel>(response);

                if (apiResponse != null && apiResponse.results != null && apiResponse.results.Count > 0 && apiResponse.results[0].indexCount > 0)
                {
                    results = apiResponse.results[0].results;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }

        private static string getCompanyFacet(string company)
        {
            string val = company;
            try
            {
                bool isFacetFound = false;
                val = _facetCollection[company];
                if (string.IsNullOrEmpty(val))
                {
                    RequestModel request = getFacetRequestModel(company);
                    var json = JsonConvert.SerializeObject(request);

                    string response = HttpUtility.GetApiResponseAsync(json);
                    ResponseModel facetResponse = JsonConvert.DeserializeObject<ResponseModel>(response);
                    if (facetResponse != null && facetResponse.results != null && facetResponse.results.Count > 0)
                    {
                        var responseFacets = facetResponse.results[0].facets;
                        if (responseFacets != null && responseFacets.Count > 0)
                        {
                            Facet f = responseFacets[0];
                            if (f != null && f.facetElements != null && f.facetElements.Count > 0)
                            {
                                var fe = f.facetElements[0];
                                if (fe != null)
                                {
                                    val = fe.name;
                                    isFacetFound = true;
                                    _facetCollection.Add(company, fe.name);
                                    LoggerUtility.WriteLog("Facet Found : ", "Company : " + company + "\nFacet : " + val);
                                }
                            }

                        }

                    }
                }
                else
                {
                    isFacetFound = true;
                }
                if (!isFacetFound)
                {
                    LoggerUtility.WriteLog("Facet Not Found : ", "Company : " + company + "\nFacet : " + val);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return val;
        }

        private static RequestModel getFacetRequestModel(string company)
        {
            RequestModel m = new RequestModel();
            m.queryString = company;
            m.queryContext.curations.Add(ApiKeyEnum.ARTICLES);
            m.resultContext.maxResults = 1;
            m.resultContext.facets.names.Add(ApiKeyEnum.ORGANISATIONS);
            m.resultContext.facets.minThreshold = 1;
            m.resultContext.facets.maxElements = 1;

            return m;

        }

        private static RequestModel getNewsRequestModel(string company, string facet, string fDate, string tDate)
        {
            RequestModel m = new RequestModel();
            m.queryString = company + " OR " + ApiKeyEnum.ORGANISATIONS + ":=\"" + facet + "\" AND " + ApiKeyEnum.LAST_PUB_DATE + ":>" + fDate + " AND " + ApiKeyEnum.LAST_PUB_DATE + ":<" + tDate + "";
            m.queryContext.curations.Add(ApiKeyEnum.ARTICLES);
            m.resultContext.maxResults = _maxResult;
            m.resultContext.aspects.Add(AspectEnums.TITLE);
            m.resultContext.aspects.Add(AspectEnums.LIFE_CYCLE);
            m.resultContext.aspects.Add(AspectEnums.LOCATION);
            m.resultContext.aspects.Add(AspectEnums.SUMMARY);
            m.resultContext.aspects.Add(AspectEnums.EDITORIAL);

            return m;
        }
    }
}
