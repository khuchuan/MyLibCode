using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class DataFake
    {
        public List<Partner> GetListPartner()
        {
            List<Partner> list = new List<Partner>();
            list.Add(new Partner() { Id = 1, Name = "test.topup", Description = "Doi tac ket noi topup" });
            list.Add(new Partner() { Id = 2, Name = "test.paybil", Description = "Doi tac ket noi paybill" });
            list.Add(new Partner() { Id = 3, Name = "test.softpin", Description = "Doi tac ket noi softpin" });
            return list;
        }

        public List<Pack> GetPacks(string productCode) 
        {
            List<Pack> list = new List<Pack>();
            switch (productCode)
            {
                case "Viettel":
                    list.Add(new Pack() { Code = "Viettel70", Id = 1, Name = "Viettel 70" });
                    list.Add(new Pack() { Code = "Viettel90", Id = 2, Name = "Viettel 90" });
                    list.Add(new Pack() { Code = "Viettel120", Id = 3, Name = "Viettel 120" });
                    break;
                case "Vina":
                    list.Add(new Pack() { Code = "Vina70", Id = 1, Name = "Vina 70" });
                    list.Add(new Pack() { Code = "Vina90", Id = 2, Name = "Vina 90" });
                    list.Add(new Pack() { Code = "Vina120", Id = 3, Name = "Vina 120" });
                    break;
                case "Mobi":
                    list.Add(new Pack() { Code = "Mobi70", Id = 1, Name = "Mobi 70" });
                    list.Add(new Pack() { Code = "Mobi90", Id = 2, Name = "Mobi 90" });
                    list.Add(new Pack() { Code = "Mobi120", Id = 3, Name = "Mobi 120" });
                    break;

                default:
                    break;
            }
            
            return list;
        }

        public List<Product> GetProduct()
        {
            List<Product> list = new List<Product>();
            list.Add(new Product() { Code = "Viettel", Id = 1, Name = "Viettel" });
            list.Add(new Product() { Code = "Vina", Id = 2, Name = "Vina" });
            list.Add(new Product() { Code = "Mobi", Id = 3, Name = "Mobi" });
            return list;
        }



    }
}