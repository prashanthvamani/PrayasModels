using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using Snowflake.Data.Client;

namespace PrayasModels
{
    public class DAL
    {
        DataSet ds;
        SqlDataAdapter da;
        //SnowflakeDbDataAdapter da;
        public static SqlConnection connect()
        {
            //Reading the connection string from web.config    
            //string Name = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            //Passing the string in sqlconnection.    
            //SqlConnection con = new SqlConnection(Name);

            SqlConnection con = new SqlConnection(GetConn.constring());
            //Check wheather the connection is close or not if open close it else open it    
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            else
            {

                con.Open();
            }
            return con;

        }
        //Creating a method which accept any type of query from controller to execute and give result.
        //result kept in datatable and send back to the controller.
        public DataTable MyMethod(string Query)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(Query, DAL.connect());

            da.Fill(dt);
            List<SelectListItem> list = new List<SelectListItem>();
            return dt;

        }



        //public static SnowflakeDbConnection connectSF()
        //{

        //    SnowflakeDbConnection consf = new SnowflakeDbConnection("account=indusind-enterprisedataplatform.privatelink;user=test;password=Snow_123;role=BFIL-HT-DEVELOPER;WAREHOUSE=ETL_MONITORING_WH;db=BFIL;schema=BFIL_MART");
        //    //con.Open();

        //    if (consf.State = ConnectionState.Open)
        //    {
        //        consf.Close();
        //    }
        //    else
        //    {
        //        consf.Open();
        //    }

        //    return consf;

        //}

        //public DataTable MyMethodSF(string Query)
        //{
        //    ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    da = SnowflakeDbDataAdapter (Query, DAL.connectSF());
        //    da.Fill(dt);
        //    List<SelectListItem> lists = new List<SelectListItem>();
        //    return dt;
        //}
    }
}