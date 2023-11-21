using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class CheckDataRequest
    {
        public string PartnerId { get; set; }
        public string CustomerMobile { get; set; }
        public string ProductCode { get; set; }
    }
}