using PrayasModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Data.Client;
using System.Collections;
using DocumentFormat.OpenXml.Spreadsheet;
using Mono.Unix.Native;


namespace PrayasDAL
{
    public class SnflakeRpt
    {
        //SqlConnection con = new SqlConnection(GetConn.constring());

        //SnowflakeDbConnection con = new SnowflakeDbConnection(GetConn.constring);
        SnowflakeDbConnection snowflakeConnection = new SnowflakeDbConnection();
        DataTable dt = new DataTable();


        public List<SnowFlakeRpt> SFRpt(string Zone)
        {
            List<SnowFlakeRpt> Rptlist = new List<SnowFlakeRpt>();
            //snowflakeConnection.ConnectionString = "account=fc42949.central-india.azure;user=DEV_PRAYAS_USER;password=Welcome@123;role=BFIL-PRAYAS-DEVLOPER_ROLE;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";


            snowflakeConnection.ConnectionString ="account=indusind-edp_nonprod.privatelink;user=DEV_PERVIEW_USER;password=Welcome@1234;role=BFIL-HT-DEVELOPER;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";
            snowflakeConnection.Open();
            //SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("SELECT top 10 BRID,BRANCHNAME,BRANCHINCEPTION,BRANCHSTATUS,ZONE,REGION,AREA,STATE,DISTRICT,PINCODE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA", snowflakeConnection);
            SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("SELECT top 10 BRID,BRANCHNAME,BRANCHINCEPTION,BRANCHSTATUS,ZONE,REGION,AREA,STATE,DISTRICT,PINCODE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA where ZONE='" + Zone + "'", snowflakeConnection);
            adapter.Fill(dt);
            snowflakeConnection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Rptlist.Add(
                    new SnowFlakeRpt
                    {
                        BranchID = Convert.ToString(dr["BRID"]),
                        BRANCHNAME = Convert.ToString(dr["BRANCHNAME"]),
                        BRANCHINCEPTION = Convert.ToDateTime(dr["BRANCHINCEPTION"]),
                        BRANCHSTATUS = Convert.ToString(dr["BRANCHSTATUS"]),
                        ZONE = Convert.ToString(dr["ZONE"]),
                        REGION = Convert.ToString(dr["REGION"]),
                        AREA = Convert.ToString(dr["AREA"]),
                        STATE = Convert.ToString(dr["STATE"]),
                        DISTRICT = Convert.ToString(dr["DISTRICT"]),
                        //UMBASELOCATIONBRANCHID = Convert.ToString(dr["UMBASELOCATIONBRANCHID"]),
                        //UMBASELOCATIONBRANCHName = Convert.ToString(dr["UMBASELOCATIONBRANCHNAME"]),
                        PINCODE = Convert.ToString(dr["PINCODE"]),
                    });
            }
            return Rptlist;
        }

        public List<SnowFlakeRpt> BindZone()
        {
            List<SnowFlakeRpt> zonelist = new List<SnowFlakeRpt>();

            snowflakeConnection.ConnectionString ="account=indusind-edp_nonprod.privatelink;user=DEV_PERVIEW_USER;password=Welcome@1234;role=BFIL-HT-DEVELOPER;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";
            snowflakeConnection.Open();
            //SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("SELECT Distinct ZONE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA", snowflakeConnection);

            using (IDbCommand cmd = snowflakeConnection.CreateCommand())
            {
                //cmd.CommandText = "USE WAREHOUSE TEST";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "SELECT * FROM MASTERSOPDATA";   // sql opertion fetching 
                cmd.CommandText = "SELECT Distinct ZONE FROM BFIL_TEST_DB.BFIL_TEST_SCH.MASTERSOPDATA";   // sql opertion fetching 

                snowflakeConnection.Open();


                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        zonelist.Add(new SnowFlakeRpt
                        {
                            ZONE = reader["ZONE"].ToString(),
                        });
                    }

                }
                snowflakeConnection.Close();
            }
            return zonelist;
        }




    }
}
