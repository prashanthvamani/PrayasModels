using Amazon.S3;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrayasDAL;
using PrayasModels;
using Snowflake.Data.Client;
using System.Data;
using System.Data.SqlClient;

namespace PrayasWeb.Controllers
{
    public class BIreportsdownloadController : Controller
    {
       
        List<BIReportsListModel> BIlist = new List<BIReportsListModel>();
        

        List<TwoWheelerModel> twlist = new List<TwoWheelerModel>();
        //Prod
        string constring = "account=indusind-enterprisedataplatform.privatelink;user=test;password=Snow_123;role=BFIL-HT-DEVELOPER;WAREHOUSE=ETL_MONITORING_WH;db=BFIL;schema=BFIL_MART";

        SnowflakeDbConnection conn = new SnowflakeDbConnection();

        //snowflakeConnection.ConnectionString = "account=fc42949.central-india.azure;user=DEV_PRAYAS_USER;password=Prayas@123;role=BFIL-HT-DEVELOPER;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";

        DAL dAL = new DAL();

        List<SelectListItem> list = new List<SelectListItem>();

        //SqlConnection con = new SqlConnection(GetConn.constring());
        public IActionResult Index()
        {
            //BIlist = BIReportsListDAL.BindReport();


            //string query = "Select Distinct ReportName From Tbl_BIReportsList  Where Report_Block ='Business Reports' Order By ReportName";
            //DataSet ds = new DataSet();
            //List<string> li = new List<string>();
            //DataTable dt = new DataTable();
            //dt = dAL.MyMethod(query);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    //BIlist.Add(new BIReportsListModel
            //    //{
            //    //    ReportName = Convert.ToString(dr["ReportName"]),
            //    //    ID = Convert.ToInt32(dr["ID"]),
            //    //});
            //    list.Add(new SelectListItem
            //    {
            //        //Text = Convert.ToString(dr["ReportName"]),
            //        //Value = Convert.ToString(dr["ID"]),
            //        Text = Convert.ToString(dr.ItemArray[0]),
            //        //Value = Convert.ToString(dr.ItemArray[1])

            //    });

            //}


            //TempData["List"] = BIlist;

            //using (var conn = new SnowflakeDbConnection())
            // {
            conn.ConnectionString = constring;
            conn.Open();

            SnowflakeDbDataAdapter dataAdapter = new SnowflakeDbDataAdapter("select DISTINCT ZONENAME from BFIL.BFIL_MART.TW_ITLOGIN", conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            conn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem
                {
                    Text = Convert.ToString(dr["ZONENAME"]),
                    Value = Convert.ToString(dr["ZONENAME"])
                });
            }
            //}1
            ViewBag.country = list;
            return View();
        }

        [HttpPost]
        public  IActionResult Index(IFormCollection form,TwoWheelerModel rpt)
        {
            //ddlZone = model.ReportName;

           

            string ddlZone = form["country"].ToString();
            string ddlregion = form["State"].ToString();


            conn.ConnectionString = constring;
            conn.Open();

            SnowflakeDbDataAdapter dataAdapter = new SnowflakeDbDataAdapter("select top 400 ZONENAME,REGION,LOANPROPOSALCODE,CLIENTID,CLIENTCODE,CLIENTNAME,PRODUCTCODE,CENTERCODE,CENTERMEETINGDAY,proposaldate,VENDOR_NAME,MODEL,VECHICLE_BRAND,REGISTRATIONNUMBER from BFIL.BFIL_MART.TW_ITLOGIN where ZONENAME ='" + ddlZone + "' and REGION ='" + ddlregion + "'", conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            int rowscount = dt.Rows.Count;

             TempData["Count"] = "Total rows Count : " + rowscount;

            
            conn.Close();

            //foreach (DataRow dataRow in dt.Rows)
            //{
            //    twlist.Add(

            //        new TwoWheelerModel
            //        {
            //            ZONENAME = Convert.ToString(dataRow["ZONENAME"]),
            //            REGION = Convert.ToString(dataRow["REGION"]),
            //            LOANPROPOSALCODE = Convert.ToString(dataRow["LOANPROPOSALCODE"]),
            //            CLIENTID = Convert.ToString(dataRow["CLIENTID"]),
            //            CLIENTCODE = Convert.ToString(dataRow["CLIENTCODE"]),
            //            CLIENTNAME = Convert.ToString(dataRow["CLIENTNAME"]),
            //            PRODUCTCODE = Convert.ToString(dataRow["PRODUCTCODE"]),
            //            CENTERCODE = Convert.ToString(dataRow["CENTERCODE"]),
            //            CENTERMEETINGDAY = Convert.ToString(dataRow["CENTERMEETINGDAY"]),
            //            proposaldate = Convert.ToDateTime(dataRow["proposaldate"]),
            //            VENDOR_NAME = Convert.ToString(dataRow["VENDOR_NAME"]),
            //            MODEL = Convert.ToString(dataRow["MODEL"]),
            //            VECHICLE_BRAND = Convert.ToString(dataRow["VECHICLE_BRAND"]),
            //            REGISTRATIONNUMBER = Convert.ToString(dataRow["REGISTRATIONNUMBER"]),

            //        });

                
            //}
            //return rpt;
            




            return RedirectToAction("Index");
        }
        public JsonResult getstate(string value)
        {

            //string query = "Select Distinct a.SubReportName From Prayas.dbo.Tbl_BISubReportsList as a Inner Join Prayas.dbo.Tbl_BIReportsList as b on b.ID = a.ReportID where b.Report_Block = 'Business Reports' and b.ReportName = '" + value + "' order by a.SubReportName";

            conn.ConnectionString = constring;
            conn.Open();
            SnowflakeDbDataAdapter dataAdapter = new SnowflakeDbDataAdapter("select DISTINCT REGION from BFIL.BFIL_MART.TW_ITLOGIN where ZONENAME = '" + value + "'", conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            conn.Close();

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "--Select Region--", Value = "0" });
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem
                {
                    //Value = Convert.ToString(dr["ReportID"]),
                    //Text = Convert.ToString(dr["SubReportName"])

                    Text = Convert.ToString(dr["REGION"]),
                    Value = Convert.ToString(dr["REGION"])
                });
            }

            return Json(new SelectList(list, "Value", "Text", new System.Text.Json.JsonSerializerOptions()));
        }






        //public JsonResult getstate(BIReportsListModel model)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    string SubReportName = model.SubReportName;
        //    BIlist = BIReportsListDAL.BindSubReportList(SubReportName);
        //    return Json(BIlist);
        //    //return Json(BIlist.ToList());
        //}
    }
}
