using PrayasModels;
using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Data.Client;

namespace PrayasDAL
{

    public class BindZoneDal
    {


        SnowflakeDbConnection snowflakeConnection = new SnowflakeDbConnection();
        DataTable dt = new DataTable();

        public List<BindDropdown> BindZone()
        {
            List<BindDropdown> zonelist = new List<BindDropdown>();

            snowflakeConnection.ConnectionString = "account=fc42949.central-india.azure;user=DEV_PRAYAS_USER;password=Welcome@123;role=BFIL-HT-DEVELOPER;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH";
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
                        zonelist.Add(new BindDropdown
                        {
                            Zone = reader["ZONE"].ToString(),
                        });
                    }

                }
                snowflakeConnection.Close();
            }
            return zonelist;
        }
    }
}
