using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrayasDAL;
using PrayasModels;
using System.Data;

namespace PrayasBAL
{
    public class DDLRptBAL
    {
        DDLReprtListDAL listDAL = new DDLReprtListDAL();
        DataSet ds = new DataSet();
        List<BIReportsListModel>  bIReportsListModels = new List<BIReportsListModel>();

        public List<BIReportsListModel> bindrpt()
        {
            bIReportsListModels = listDAL.BindReport();

            

            return bIReportsListModels;
        }

    }
}
