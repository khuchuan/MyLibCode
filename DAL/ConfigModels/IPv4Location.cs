using System;
using System.Collections.Generic;

namespace DAL.ConfigModels
{
    public partial class IPv4Location
    {
        public long IPFrom { get; set; }
        public long IPTo { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
