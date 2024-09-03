using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrayasModels
{
    public class InstanceCBRptModel
    {
        public string ZoneName {  get; set; }
        public string State { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string UnitName { get; set; }
        public string Branch_ID { get; set; }
        public string BRANCHNAME { get; set; }
        public string ClientID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get;set; }
        //public DateTime JoinDate { get; set; }
        public string TargetID { get; set; }
        public string Status { get; set; }
        public string Final_Ineligible { get; set; }
        public DateTime DateCreated { get; set; }                      

    }
}
