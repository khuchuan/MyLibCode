using System;
using System.Collections.Generic;

namespace DTO
{
    public class RequestSupportInfo
    {
        public int Id { get; set; }
        public string? TransactionId { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public string? Attachment { get; set; }
        public bool SendMailAdmin { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateBy { get; set; }
        public string? PhoneNumber { get; set; }
        public int TotalRows { get; set; }
    }
}
