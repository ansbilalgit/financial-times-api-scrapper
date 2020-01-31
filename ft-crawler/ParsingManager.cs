using ft_crawler.Enums;
using ft_crawler.Models;
using ft_crawler.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ft_crawler
{
    public class ParsingManager
    {
        private static NameValueCollection _inputFileConfig = (NameValueCollection)ConfigurationManager.GetSection("inputFileConfig");

        public static void StartParsing()
        {
            try
            {
                //Gets Data from File
                var excelDataTable = ExcelManager.GetDataTable();

                //Crawls data from FT API and Stores in sperate excel File
                crawlDataAndSaveData(excelDataTable);
            }
            catch (Exception ex)
            {
                LoggerUtility.WriteLog("Error in Starting Parser", ex.Message);
            }

        }

        private static void crawlDataAndSaveData(DataTable dt)
        {
            DataTable updatedTable = new DataTable();
            updatedTable = dt.Clone();
            updatedTable.Columns.Add(OutputColumnEnum.PUB_DATE, typeof(DateTime));
            updatedTable.Columns.Add(OutputColumnEnum.TITLE, typeof(string));
            updatedTable.Columns.Add(OutputColumnEnum.SUB_HEADING, typeof(string));
            updatedTable.Columns.Add(OutputColumnEnum.SUMMARY, typeof(string));
            updatedTable.Columns.Add(OutputColumnEnum.WEB_URL, typeof(string));
            int totalRows = dt.Rows.Count;
            LoggerUtility.WriteLog("Total Rows to Crawl :", dt.Rows.Count.ToString());
            if (totalRows > 0)
            {
                for (int i = 0; i < totalRows; i++)
                {
                    DataRow dr = dt.Rows[i];
                    updatedTable.ImportRow(dr);
                    try
                    {
                        string companyCol = _inputFileConfig[InputFileConfigKeyEnum.COMPANY];
                        string company = dr[companyCol].ToString();

                        string fromDateCol = _inputFileConfig[InputFileConfigKeyEnum.FROM_DATE];
                        string fromDate = dr[fromDateCol].ToString();

                        string toDateCol = _inputFileConfig[InputFileConfigKeyEnum.TO_DATE];
                        string toDate = dr[toDateCol].ToString();

                        try
                        {
                            List<Result> newsResult = ApiMananger.GetNewsList(company, fromDate, toDate);
                            foreach (var result in newsResult)
                            {
                                DataRow newsRow = updatedTable.NewRow();
                                newsRow[OutputColumnEnum.PUB_DATE] = result.lifecycle?.lastPublishDateTime;
                                newsRow[OutputColumnEnum.SUB_HEADING] = result.editorial?.subheading;
                                newsRow[OutputColumnEnum.SUMMARY] = result.summary?.excerpt;
                                newsRow[OutputColumnEnum.TITLE] = result.title?.title;
                                newsRow[OutputColumnEnum.WEB_URL] = result.location?.uri;
                                updatedTable.Rows.Add(newsRow);
                            }

                            ExcelManager.SaveDataTable(updatedTable);
                            LoggerUtility.WriteLog("Record Saved", i.ToString());

                        }
                        catch (Exception ex)
                        {
                            LoggerUtility.WriteLog("Error In Getting News for " + company + " - " + fromDate + " - " + toDate, ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {

                        LoggerUtility.WriteLog("Error In Reading Row " + i, ex.Message);
                    }

                }
            }


        }
    }
}
