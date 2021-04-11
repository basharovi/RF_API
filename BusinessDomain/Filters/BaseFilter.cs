using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Filter
{
    public class BaseFilter
    {
        
        public string ID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
       
        public string Users { get; set; }
        public int? PageSize { get; set; }
        public int? PageNo { get; set; }
        public int? RecordId { get; set; }
       
        // For Exprot 
        public string ModelName { get; set; }
        public string Extension { get; set; }
        //For Exprot End
    }
}
