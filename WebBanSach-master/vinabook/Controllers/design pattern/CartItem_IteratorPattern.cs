using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Models;
namespace Vinabook.Controllers.design_pattern
{
    public interface Iterator
    {
        CartItem First(); //phần tử đầu
        CartItem Next();  // phần tử kế tiếp
        bool IsCollectionEnds { get; } // kiểm tra duyệt ds phần tử 
        CartItem CurrentItem { get; } // vị trí phần tử
    }


    public class CartItem_IteratorPattern : Iterator
    {
        List<CartItem> CartList;
        int position = 0; //vị trí phần tử đầu
        int step = 1; // vị trí phần tử kế tiếp 

        public CartItem_IteratorPattern(List<CartItem>CartList)
        {
            this.CartList = CartList;
        }

        public bool IsCollectionEnds
        {
            get
            {
                if (position >= CartList.Count)
                    return true;
                else
                    return false;
            }
        }

        public CartItem CurrentItem => CartList[position];

        public CartItem First()
        {
            position = 0;

            if (CartList.Count > 0)
                return CartList[position] as CartItem;

            return null;
        }

        public CartItem Next()
        {
            position += step;
            if (!IsCollectionEnds)
                return CartList[position] as CartItem;
            else
                return null;
        }
    }
}