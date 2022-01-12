using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Models;
namespace Vinabook.Controllers.design_pattern.Facade_design
{
    public class CartFacade
    {
        private List<CartItem> listCart;

        public CartFacade()
        {
            listCart=new List<CartItem>();
        }

         // Check giỏ hàng trống
          public List<CartItem> CheckCart()
        {
            List<CartItem> listCart = HttpContext.Current.Session["ShoppingCart"] as List<CartItem>;
            if (listCart == null)
            {
                listCart = new List<CartItem>();
                HttpContext.Current.Session["ShoppingCart"] = listCart;
            }
            return listCart;
        }

        // check số lượng giỏ hàng
        public List<CartItem> CheckCart_QualityItem(List<CartItem> giohang)
        {          
            List<CartItem>listcart= giohang;
            return listcart;
        }
        

       
    }
}