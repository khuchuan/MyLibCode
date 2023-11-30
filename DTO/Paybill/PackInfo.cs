using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paybill
{
    public class PackInfo
    {
        public string? ProductId { get; set; }
        public string? PackId { get; set; }
        public string? Data { get; set; }
        public double? Amount { get; set; }
        public int? TimeLimit { get; set; }
        public bool IsActive { get; set; } = false;

    }
}
