using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Controllers;
using System.Web.ModelBinding;
using Vinabook.Models;
using System.Web.Mvc;

namespace Vinabook.Controllers.design_pattern
{
    public sealed class BookSingletonPattern
    {
        
        public static BookSingletonPattern Instance { get; } = new BookSingletonPattern();       
            private BookSingletonPattern() { }

            public Sach Init(int id,QuanLyBanSachEntities db,HttpResponseBase response)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaChuDe == id);
            if (sach == null)
            {              
                 response.StatusCode= 404;
                return null;
            }

            return sach;
        }
       

        // http post
        public void Edit(QuanLyBanSachEntities db, Sach sach, System.Web.Mvc.ModelStateDictionary modelState)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //http post
        public void Create(QuanLyBanSachEntities db, Sach sach, System.Web.Mvc.ModelStateDictionary modelState, dynamic ViewBag)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                ViewBag.ThongBao = "Thêm mới thành công";
            }
            else
            {
                ViewBag.ThongBao = "Thêm mới thất bại";
            }
        }

        
    }
}