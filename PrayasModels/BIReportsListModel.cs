using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace PrayasModels
{
    public class BIReportsListModel
    {

        //public BIReportsListModel()
        //{
        //    this.ReportName = new List<SelectListItem>();
        //    this.SubReportName = new List<SelectListItem>();
        //}
     
        //public string SubReportName { get; set; }

        public string ReportName { get; set; }

        public int ID { get; set; }

        public string SubReportName { get; set; }

        public int SubID { get; set; }

        //public int SubReprotID { get; set; }


    }

    public class ReportList
    {
        public string CREATEDBY { get; set; }

        public DateTime CREATEDDATE { get; set; }

        public string REPORTNAME { get; set; }

        public string STATUS { get; set; }

        public string REPORT_BLOCK { get; set; }
    }
}
