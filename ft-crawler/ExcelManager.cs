using ft_crawler.Enums;
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
    public class ExcelManager
    {
        #region Private Properties
        private static string _inputFilePath = ConfigurationManager.AppSettings["InputFilePath"];
        private static string _outputFilePath = ConfigurationManager.AppSettings["OutputFilePath"];
        private static string _sheetName = ConfigurationManager.AppSettings["SheetName"];
        #endregion


        public static DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = ExcelUtility.GetDataSetFromExcelFile(_inputFilePath, _sheetName);
                dt = ds.Tables[_sheetName];
            }
            catch (Exception ex)
            {
                LoggerUtility.WriteLog("Error in Getting Data Set", ex.Message);
            }
            return dt;
        }

        public static void SaveDataTable(DataTable updatedTable)
        {
            try
            {
                ExcelUtility.ExportToExcelOleDb(updatedTable, _sheetName, _outputFilePath, true);
            }
            catch (Exception ex)
            {

                LoggerUtility.WriteLog("Error in Writing Data", ex.Message);
            }
        }
    }
}
