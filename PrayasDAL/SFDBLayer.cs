using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrayasModels;

namespace PrayasDAL
{
    public class SFDBLayer
    {
        enum ConType
        {
            Data,
            Report,
            Trancs,
            DataProd,
        }

        public bool ResultFlog = true;
        public string Message { get; set; }

        //string constring = "account=indusind-enterprisedataplatform.privatelink;user=test;password=Snow_123;role=BFIL-HT-DEVELOPER;WAREHOUSE=ETL_MONITORING_WH;db=BFIL;schema=BFIL_MART";

        public static SnowflakeDbConnection GetConnecion(string conType)
        {
            SnowflakeDbConnection con = new SnowflakeDbConnection();
            //GetConn.constring
            return con;
        }

    }
}
