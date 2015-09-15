using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using RiversideHealthCare.Models;

namespace RiversideHealthCare.Controllers
{
    public class ProductAdminController : Controller
    {

        //string id = Session.SessionID;
        //string timestamp = ViewState("TimeStamp").ToString(); <--checks whether this is in use
        //Session.Add("Email", username);

        ProductClass objProd = new ProductClass();
        //
        // GET: /Giftshop/

        [Authorize(Roles = "admin")]
        public ActionResult ProductIndex()
        {
            // Getting all the product and returning them into the Index view for represent
            ViewBag.Message = "Gift Shop - Product Listing";
            var prod = objProd.getProducts();
            return View(prod);
        }

        // GET: /Admin/Details
        [Authorize(Roles = "admin")]
        public ActionResult ProductDetails(int ProductId)
        {

            //Get one product by it's id and if it is available , return it to Details view for represent ,else return error view 
            var prod = objProd.getProductsById(ProductId);
            if (prod == null)
            {
                return View("Error");
            }
            else
            {
                return View(prod);

            }
        }


        // GET: /product/Insert
        [Authorize(Roles = "admin")]
        public ActionResult ProductInsert()
        {
            return View();
        }


        // POST: /product/Insert
        // To handle file upload
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ProductInsert(Product prod, HttpPostedFileBase file)
        {
            //Will check all the data regarding their validation , and if they were valid ,they are inserted into database and then redirecting to the index view .
            /*this is for image upload*/
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/giftshop/")
                                                          + file.FileName);
                    prod.Image = file.FileName;
                }
                objProd.commitInsert(prod);
                return RedirectToAction("ProductIndex");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    objProd.commitInsert(prod);
                    return RedirectToAction("ProductIndex");
                }
                catch
                {
                    return View(prod);
                }
            }
            return View(prod);
        }

        // GET: /product/updated
        [Authorize(Roles = "admin")]
        public ActionResult ProductUpdate(int ProductId)
        {
            //It will show the edit page according to the selected product
            var prod = objProd.getProductsById(ProductId);
            if (prod == null)
            {
                return View("Error");
            }
            return View(prod);
        }

        // POST: /product/Update
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ProductUpdate(int ProductId, Product prod, HttpPostedFileBase file)
        {
            //Will check all the data regarding their validation , and if they were valid ,they are updated into database and then redirecting to the index view .
            /*this is for image upload*/
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/images/giftshop/")
                                                          + file.FileName);
                    prod.Image = file.FileName;
                }
                objProd.commitUpdate(ProductId, prod.Name, prod.Description, prod.UnitPrice, prod.Image, prod.Stock);
                return RedirectToAction("ProductIndex");
            }

            return View();
        }

        // GET: /product/Delete
        [Authorize(Roles = "admin")]
        public ActionResult ProductDelete(int ProductId)
        {

            //It shows Delete view according to the selected product
            var prod = objProd.getProductsById(ProductId);
            if (prod == null)
            {
                return View("Error");
            }
            return View(prod);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDelete(int ProductId, Product prod)
        {
            //Selected product will be deleted from the database
            try
            {
                objProd.commitDelete(ProductId);
                return RedirectToAction("ProductIndex");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Error()
        {
            //shows the Error view
            return View();
        }

    }
}
