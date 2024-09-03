using PrayasModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Data.Client;
using System.Web.Mvc;

namespace PrayasDAL
{
    public class DBLayer
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

        //public static SnowflakeDbConnection getCon(string type)
        //{
        //    SnowflakeDbConnection snowflakeConnection = new SnowflakeDbConnection(GetConn.constring);

        //    return snowflakeConnection;

        //}

        public static SqlConnection GetConnecion(string conType)
        {

            SqlConnection con = new SqlConnection(GetConn.constring());


            //SqlConnection con = new SqlConnection();
            //if (conType == "Data")
            //con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
            //else if (conType == "Tranc")
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["rlmsTranc"].ToString();
            //else if (conType == "MSME")
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSMEConn"].ToString();
            //else if (conType == "DataProd")
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RLMSConnProd"].ToString();
            //else if (conType == "RDSP_Trans")
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RDSP_Trans"].ToString();
            //else if (conType == "CB")
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CB_HTML"].ToString();
            //else if (conType == "RLMS_Extended")
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RLMS_Extended"].ToString();
            //else
            //    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["rlmsReports"].ToString();

            return con;
        }

        public DataTable GetDt(string query, string connection, CommandType ct)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = GetConnecion(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.CommandType = ct;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 0;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception er)
            {
                ResultFlog = false;
                Message = er.Message;
            }
            return dt;
        }

        public DataTable GetDt(string query, string connection, CommandType ct, SqlParameter[] p)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = GetConnecion(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.CommandType = ct;
                        cmd.Connection = con;
                        cmd.Parameters.AddRange(p);
                        cmd.CommandTimeout = 0;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception er)
            {
                ResultFlog = false;
                Message = er.Message;
            }
            return dt;
        }

        public DataSet GetDataSet(string query, string connection, CommandType ct, SqlParameter[] p)
        {
            DataSet dt = new DataSet();
            try
            {
                using (SqlConnection con = GetConnecion(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.CommandType = ct;
                        cmd.Connection = con;
                        cmd.Parameters.AddRange(p);
                        cmd.CommandTimeout = 0;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception er)
            {
                ResultFlog = false;
                Message = er.Message;
            }
            return dt;
        }

        public DataSet GetDataSet(string query, string connection, CommandType ct)
        {
            DataSet dt = new DataSet();
            try
            {
                using (SqlConnection con = GetConnecion(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.CommandType = ct;
                        cmd.Connection = con;
                        cmd.CommandTimeout = 0;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception er)
            {
                ResultFlog = false;
                Message = er.Message;
            }
            return dt;
        }

        public int InsertInto(string proceure, SqlParameter[] p, string Connection)
        {
            try
            {
                using (SqlConnection con = GetConnecion(Connection))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = proceure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddRange(p);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        return i;
                    }
                }
            }
            catch (Exception ex)
            {
                ResultFlog = true;
                Message = ex.Message;
                throw new Exception(ex.Message);
            }

        }

        internal int UpdateDb(string query, SqlParameter[] p, string Connection)
        {
            try
            {
                using (SqlConnection con = GetConnecion(Connection))
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddRange(p);
                    cmd.Connection = con;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i;
                }
            }
            catch (Exception ex)
            {
                return 0;
                ResultFlog = true;
                Message = ex.Message;
                throw new Exception(ex.Message);
            }
        }

        internal int UpdateDb(string query, string Connection)
        {
            try
            {
                using (SqlConnection con = GetConnecion(Connection))
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Connection = con;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i;
                }
            }
            catch (Exception ex)
            {
                return 0;
                ResultFlog = true;
                Message = ex.Message;
                throw new Exception(ex.Message);
            }
        }

        public DataTable MyMethod(string Query)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, DAL.connect());

            da.Fill(dt);
            List<SelectListItem> list = new List<SelectListItem>();
            return dt;

        }

    }
}
