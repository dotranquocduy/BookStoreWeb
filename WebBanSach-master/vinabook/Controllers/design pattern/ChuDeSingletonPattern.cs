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
    public sealed class ChuDeSingletonPattern
    {
        
        public static ChuDeSingletonPattern Instance { get; } = new ChuDeSingletonPattern();       
            private ChuDeSingletonPattern() { }

            public ChuDe Init(int id,QuanLyBanSachEntities db,HttpResponseBase response)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id);
            if (cd == null)
            {              
                 response.StatusCode= 404;
                return null;
            }

            return cd;
        }
       

        // http post
        public void Edit(QuanLyBanSachEntities db, ChuDe cd, System.Web.Mvc.ModelStateDictionary modelState)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //http post
        public void Create(QuanLyBanSachEntities db, ChuDe cd, System.Web.Mvc.ModelStateDictionary modelState, dynamic ViewBag)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                db.ChuDes.Add(cd);
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