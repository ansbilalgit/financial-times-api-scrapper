using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler.Utilities
{
    public static class HttpUtility
    {
        private static string _apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private const string _apiUrl = "http://api.ft.com/content/search/v1";

        public static string GetApiResponseAsync(string body)
        {
            string response = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    var apiResponse = client.PostAsync(_apiUrl,
                        new StringContent(body, Encoding.UTF8, "application/json")).Result;
                    if (apiResponse.IsSuccessStatusCode)
                        response = apiResponse.Content.ReadAsStringAsync().Result;
                    else
                        LoggerUtility.WriteLog("Api Call Failed ", apiResponse.StatusCode.ToString() + "\n" + apiResponse.Content.ToString() + "\n" + body);
                }
            }
            catch (Exception ex)
            {
                LoggerUtility.WriteLog("Api Call Failed", ex.Message + "\n" + body);
            }
            return response;
        }
    }
}
