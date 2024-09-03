using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PrayasModels;
using System.Web.Mvc;
using System.Data;
using PrayasModels;
using Amazon.S3.Model.Internal.MarshallTransformations;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Web.Helpers;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Bibliography;

namespace PrayasDAL
{
    public class BIReportsListDAL
    {
        SnowflakeDbConnection snowflakeConnection = new SnowflakeDbConnection();
        BIReportsListModel listModel = new BIReportsListModel();
        //DataTable dt = new DataTable();

        //SqlConnection con = new SqlConnection(GetConn.constring());



        public List<BIReportsListModel> BindReport()
        {
            List<BIReportsListModel> Rptlist = new List<BIReportsListModel>();

            //DEV
            //snowflakeConnection.ConnectionString = "account=indusind-edp_nonprod.privatelink;user=DEV_PRAYAS_USER;password=Welcome@123;role=BFIL-PRAYAS-DEVLOPER_ROLE;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";





            //DEV1
            //snowflakeConnection.ConnectionString = "account=app-indusind-edp_nonprod.privatelink;user=DEV_PERVIEW_USER;password=Welcome@1234;role=BFIL-HT-DEVELOPER;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";



            //Prod
            snowflakeConnection.ConnectionString = "account=indusind-enterprisedataplatform.privatelink;user=test;password=Snow_123;role=BFIL-HT-DEVELOPER;WAREHOUSE=ETL_MONITORING_WH;db=BFIL;schema=BFIL_MART";
            snowflakeConnection.Open();

            //SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("SELECT Distinct ZONE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA", snowflakeConnection);


            using (IDbCommand cmd = snowflakeConnection.CreateCommand())
            {
                //cmd.CommandText = "Select Distinct REPORTNAME From BFIL_TEST_DB.BFIL_TEST_SCH.Tbl_BIReportsList  Where Report_Block ='Business Reports' Order By ReportName";   // sql opertion fetching 


                //cmd.CommandText = "SELECT Distinct ZONE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA";       

                cmd.CommandText = "select DISTINCT ZONENAME from BFIL.BFIL_MART.TW_ITLOGIN";


                snowflakeConnection.Open();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rptlist.Add(new BIReportsListModel
                        {
                            //ID = Convert.ToInt32(reader["REPORTID"]),
                            ReportName = reader["ZONENAME"].ToString(),
                            //ReportName = reader["ZONE"].ToString(),
                            //SubReportName = reader["SUBREPORTNAME"].ToString(),
                        });
                    }

                }
                snowflakeConnection.Close();
            }
            return Rptlist;
        }

        //public List<BIReportsListModel> BindReport()
        //{
        //    List<BIReportsListModel> Rptlist = new List<BIReportsListModel>();

        //    string query = "Select Distinct REPORTNAME From Tbl_BIReportsList  Where Report_Block ='Business Reports' Order By ReportName";
        //    using (SqlCommand cmd = new SqlCommand(query))
        //    {
        //        cmd.Connection = con;
        //        con.Open();
        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while (sdr.Read())
        //            {
        //                Rptlist.Add(new BIReportsListModel
        //                {
        //                    ReportName = sdr["REPORTNAME"].ToString(),
        //                });
        //            }
        //        }
        //        con.Close();
        //    }

        //    return Rptlist;
        //}

        public List<BIReportsListModel> BindSubReportList(string Reportname)
        {
            List<BIReportsListModel> subrptlist = new List<BIReportsListModel>();

            //snowflakeConnection.ConnectionString = "account=fc42949.central-india.azure;user=DEV_PRAYAS_USER;password=Welcome@123;role=BFIL-PRAYAS-DEVLOPER_ROLE;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";

            snowflakeConnection.ConnectionString = "account=indusind-enterprisedataplatform.privatelink;user=test;password=Snow_123;role=BFIL-HT-DEVELOPER;WAREHOUSE=ETL_MONITORING_WH;db=BFIL;schema=BFIL_MART";

            snowflakeConnection.Open();

            using (IDbCommand cmd = snowflakeConnection.CreateCommand())
            {
                //cmd.CommandText = "Select Distinct a.SubReportName From Tbl_BISubReportsList as a Inner Join Tbl_BIReportsList as b on b.ID = a.ReportID where b.Report_Block = 'Business Reports' and b.ReportName = 'Pragati Live' order by a.SubReportName";   // sql opertion fetching 


                cmd.CommandText = "select DISTINCT REGION from BFIL.BFIL_MART.TW_ITLOGIN where ZONENAME='" + Reportname + "'";

                snowflakeConnection.Open();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subrptlist.Add(new BIReportsListModel
                        {
                            //SubID = Convert.ToInt32(reader["REPORTID"]),
                            SubReportName = reader["REGION"].ToString(),

                            //SubReportName = reader["SUBREPORTNAME"].ToString(),
                        });
                    }
                }
                snowflakeConnection.Close();
            }
            return subrptlist;
        }



    



        public List<ReportList> Menulist()
        {
            List<ReportList> rpt = new List<ReportList>();

            snowflakeConnection.ConnectionString = "account=fc42949.central-india.azure;user=DEV_PRAYAS_USER;password=Welcome@123;role=BFIL-PRAYAS-DEVLOPER_ROLE;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";
            snowflakeConnection.Open();
            //SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("SELECT top 10 BRID,BRANCHNAME,BRANCHINCEPTION,BRANCHSTATUS,ZONE,REGION,AREA,STATE,DISTRICT,PINCODE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA", snowflakeConnection);
            SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("select ID,CREATEDBY,CREATEDDATE,REPORTNAME,STATUS,REPORT_BLOCK from BFIL_TEST_DB.BFIL_TEST_SCH.Tbl_BIReportsList", snowflakeConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            snowflakeConnection.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                rpt.Add(
                    new ReportList
                    {
                        CREATEDBY = Convert.ToString(dr["CREATEDBY"]),
                        CREATEDDATE = Convert.ToDateTime(dr["CREATEDDATE"]),
                        REPORTNAME = Convert.ToString(dr["REPORTNAME"]),
                        STATUS = Convert.ToString(dr["STATUS"]),
                        REPORT_BLOCK = Convert.ToString(dr["REPORT_BLOCK"])
                    });
            }

            return rpt;
            
        }
    }
}
