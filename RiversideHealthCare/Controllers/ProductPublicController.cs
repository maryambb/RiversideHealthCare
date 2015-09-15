using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class ProductPublicController : Controller
    {
        ProductClass objProd = new ProductClass();
        OrderClass objOrd = new OrderClass();

        //displaying index content only
        public ActionResult GiftshopIndex()
        {
            ViewBag.Message = "Gift Shop";
            var prod = objProd.getProducts();
            return View(prod);
        }

        //GET: Product from list
        public ActionResult PurchaseForm(int ProductId)
        {
            ViewBag.amount = new ProductClass().getProductsAmountById(ProductId);
            ViewBag.P_Id = ProductId;
            return View();
        }

        //purchase form post
        [HttpPost]
        public ActionResult PurchaseForm(Order ord)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //these part of code is for creating the url which redirect to paypal (connect us to paypal)
                    string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                    string cmd = "?cmd=";
                    string value = "_products";
                    string business = "&business=info@riverside.webelementz.com";
                    string itm = "&item_name=Buy this product";
                    string currency = "&currency_code=CAD";
                    string amt = "&amount=";

                    string path = url + cmd + value + business + itm + currency + amt;


                    // Inserting the order information into db
                    objOrd.commitInsert(ord);
                    //After inserting, redirect to the paypal 
                    return Redirect(path);
                }
                catch
                {
                    //Error handling, return to Error view if something goes wrong
                    return View("Error");
                }
            }
            return View();

        }


        public ActionResult Error()
        {
            //shows the Error view
            return View();
        }

    }
}
