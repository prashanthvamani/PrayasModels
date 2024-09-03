using PrayasModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayasDAL
{
    public class InstanceCBrpt : DBLayer
    {
        public List<InstanceCBRptModel> UpdateList()
        {
            List<InstanceCBRptModel> InstantList = new List<PrayasModels.InstanceCBRptModel>();
            DBLayer db = new DBLayer();
            SqlParameter[] param = {
                new SqlParameter("@Zone","South Zone"),
                //new SqlParameter("@ID",Id),
            };
            DataTable dt = db.GetDt("Get_Live_InstantCB_Data", "Data", CommandType.StoredProcedure, param);

            foreach (DataRow dr in dt.Rows)
            {
                InstantList.Add(
                   new InstanceCBRptModel
                   {
                       ZoneName = dr["ZoneName"].ToString(),
                       State = dr["State"].ToString(),
                       Region = dr["Region"].ToString(),
                       Area = dr["Area"].ToString(),
                       UnitName = dr["UnitName"].ToString(),
                       Branch_ID = dr["Branch_ID"].ToString(),
                       BRANCHNAME = dr["BRANCH NAME"].ToString(),
                       ClientID = dr["ClientID"].ToString(),
                       ClientCode = dr["ClientCode"].ToString(),
                       ClientName = dr["ClientName"].ToString(),
                       //JoinDate = Convert.ToDateTime(dr["JoinDate"]),
                       TargetID = dr["TargetID"].ToString(),
                       Status = dr["Status"].ToString(),
                       Final_Ineligible = dr["Final_Ineligible"].ToString(),
                       DateCreated = Convert.ToDateTime(dr["DateCreated"]),

                   });
            }
            return InstantList;
        }
    }
}
