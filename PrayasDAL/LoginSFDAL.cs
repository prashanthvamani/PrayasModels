using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayasDAL
{
    public class LoginSFDAL
    {
        SnowflakeDbConnection snowflakeConnection = new SnowflakeDbConnection("account=fc42949.central-india.azure;user=DEV_PRAYAS_USER;password=Welcome@123;role=BFIL-PRAYAS-DEVLOPER_ROLE;WAREHOUSE=TEST;db=BFIL_TEST_DB;schema=BFIL_TEST_SCH");
        


        public DataSet LoginBranches(string userid)
        {
            DataSet dt = new DataSet();

            snowflakeConnection.Open();
            SnowflakeDbDataAdapter adapter = new SnowflakeDbDataAdapter("SELECT * FROM BFIL_TEST_DB.BFIL_TEST_SCH.AR_ACTIVEBRANCHLISTCUSTOMLOGIN where EMPID='"+ userid + "'", snowflakeConnection);
            adapter.Fill(dt);
            snowflakeConnection.Close();



            return dt;
        }

    }
}
