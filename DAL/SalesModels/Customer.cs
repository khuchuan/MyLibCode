using System;
using System.Collections.Generic;

namespace DAL.SalesModels
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Partner { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string IdCard { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
