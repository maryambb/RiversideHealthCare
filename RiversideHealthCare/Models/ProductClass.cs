using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class ProductClass
    {
        //creating instance of linq object
        riversideLinqDataContext objProd = new riversideLinqDataContext();

        public IQueryable<Product> getProducts()
        {
            var allProducts = objProd.Products.Select(x => x);
            return allProducts;
        }

        public Product getProductsById(int _ProductId)
        {
            var allProduct = objProd.Products.SingleOrDefault(x => x.ProductId == _ProductId);
            return allProduct;
        }

        public string getProductsAmountById(int _ProductId)
        {
            var Product_amount = objProd.Products.SingleOrDefault(x => x.ProductId == _ProductId).UnitPrice;
            return Product_amount.ToString();
        }

        public bool commitInsert(Product prod)
        {
            using (objProd)
            {
                objProd.Products.InsertOnSubmit(prod);
                //commit insert with db
                objProd.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _ProductId, string _Name, string _Description, decimal _UnitPrice, string _Image, int _Stock)
        {
            using (objProd)
            {
                var objUpdateProd = objProd.Products.Single(x => x.ProductId == _ProductId);
                //setting table cols to new values
                objUpdateProd.Name = _Name;
                objUpdateProd.Description = _Description;
                objUpdateProd.UnitPrice = _UnitPrice;
                objUpdateProd.Image = _Image;
                objUpdateProd.Stock = _Stock;
                //commit update against db
                objProd.SubmitChanges();
                return true;
            }
        }

        public bool commitDelete(int _ProductId)
        {
            using (objProd)
            {
                var objDeleteProd = objProd.Products.Single(x => x.ProductId == _ProductId);
                //deleting from table
                objProd.Products.DeleteOnSubmit(objDeleteProd);
                //committing delete with db
                objProd.SubmitChanges();
                return true;
            }
        }


    }
}