using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PrayasModels;

namespace PrayasDAL
{
    public class LoginDal : DBLayer
    {
        SqlConnection con = new SqlConnection(GetConn.constring());
        
        DBLayer db = new DBLayer();
        DataSet ds = new DataSet();
        public DataSet LoginBranches(string userid)
        {
            

            SqlParameter[] param =
            {
                new SqlParameter("@staffid", userid),
            };
            ds = db.GetDataSet("AR_UserBranchlist", "Data", CommandType.StoredProcedure, param);

            //SqlCommand cmd = new SqlCommand("AR_UserBranchlist ", con)
            //{
            //    CommandType = CommandType.StoredProcedure
            //};
            //cmd.Parameters.AddWithValue("@staffid", userid).DbType = DbType.String;

            ////cmd.Parameters.AddWithValue("@staffid", userid).DbType = DbType.String;

            //cmd.CommandTimeout = 0;
            //SqlDataAdapter daCUData = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //daCUData.Fill(ds);



            return ds;
        }

        //internal DataTable GetSuperSena()
        //{
        //    DBTrap d = new Models.DBTrap();
        //    DataTable dt = d.GetDt("BFIL_SuperSena", "Report", CommandType.Text);
        //    return dt;
        //}

        //internal DataSet GetOfficerWise()
        //{
        //    DBTrap d = new Models.DBTrap();
        //    DataSet ds = d.GetDataSet("BFIL_YadhoCumulativeOfficerwise", "Reports", CommandType.StoredProcedure);
        //    return ds;

        //}

        //internal int EditList()
        //{
        //    //List<Menu> MenuList = new List<Models.Menu>();
        //    //Menu m = new Menu();
        //    DBTrap db = new DBTrap();
        //    SqlParameter[] param = {
        //        new SqlParameter("@Qtype","Update"),
        //        new SqlParameter("@ID",Id),
        //        new SqlParameter("@Name",Name),
        //        new SqlParameter("@Order",Order),
        //        new SqlParameter("@Controller",Controller),
        //        new SqlParameter("@Action",Action),
        //        new SqlParameter("@Icon",Icon),
        //        new SqlParameter("@Updatedby",Updatedby)
        //       // new SqlParameter("@IsActive",IsActive)

        //    };
        //    int i = db.InsertInto("BFIL_NewScreens", param, "Data");
        //    return i;
        //}


        //internal List<Menu> UpdateList()
        //{
        //    List<Menu> MenuList = new List<Models.Menu>();
        //    DBTrap db = new DBTrap();
        //    SqlParameter[] param = {
        //        new SqlParameter("@Qtype","fetch"),
        //        new SqlParameter("@ID",Id),
        //    };
        //    DataTable dt = db.GetDt("BFIL_NewScreens", "Data", CommandType.StoredProcedure, param);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        MenuList.Add(
        //           new Menu
        //           {
        //               Id = Convert.ToInt32(dr["ID"]),
        //               Name = dr["MenuName"].ToString(),
        //               // Parent = dr["Parent"] != System.DBNull.Value ? Convert.ToInt32(dr["Parent"]) : 0,
        //               Order = Convert.ToInt32(dr["menuOrder"]),
        //               Controller = dr["Controller"].ToString(),
        //               Action = dr["Action"].ToString(),
        //               Icon = dr["Icon"].ToString(),
        //               IsActive = Convert.ToInt32(dr["IsActive"]),
        //           });

        //    }
        //    return MenuList;
        //}
    }
}
