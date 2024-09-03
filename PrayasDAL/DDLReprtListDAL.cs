using PrayasModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayasDAL
{
    public class DDLReprtListDAL : DBLayer
    {
        SqlConnection con = new SqlConnection(GetConn.constring());
        public List<BIReportsListModel> BindReport()
        {
            List<BIReportsListModel> Rptlist = new List<BIReportsListModel>();

            string query = "Select Distinct REPORTNAME From Tbl_BIReportsList  Where Report_Block ='Business Reports' Order By ReportName";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Rptlist.Add(new BIReportsListModel
                        {
                            ReportName = sdr["REPORTNAME"].ToString(),
                        });
                    }
                }
                con.Close();
            }

            return Rptlist;
        }
    }
}
