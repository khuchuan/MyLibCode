using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class TestController : Controller
    {
        public ActionResult DataTopup()
        {
            // Lay DS khach hang, San pham de truyen du lieu vao view
            DataFake dataFake = new DataFake();
            var listPartner = dataFake.GetListPartner();

            var lisProduct = dataFake.GetProduct();
            //var lisPack = new List<Pack>();

            ViewBag.model = listPartner;
            ViewBag.listPack = lisProduct;

            return View();
        }

        public ActionResult CheckDataPack(CheckDataRequest dataRequest)
        {
            var listPack = new DataFake().GetPacks(dataRequest.ProductCode);
            ViewBag.model = listPack;

            return PartialView();
        }

    }
}
