using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayasModels
{
    public class SnowFlakeRpt 
    {
        
        public string BranchID { get; set; }

        public string BRANCHNAME { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BRANCHINCEPTION { get; set; }

        public string BRANCHSTATUS { get; set; }

        public string ZONE { get; set; }

        public string REGION { get; set; }

        public string AREA { get; set; }

        public string STATE { get; set; }

        public string DISTRICT {  get; set; }

        //public string UMBASELOCATIONBRANCHID { get; set; }

        //public string UMBASELOCATIONBRANCHName { get; set; }

        public string PINCODE { get; set; }

    }
}
