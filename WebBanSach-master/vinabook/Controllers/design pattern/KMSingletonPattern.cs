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
    public sealed class KMSingletonPattern
    {
        
        public static KMSingletonPattern Instance { get; } = new KMSingletonPattern();       
            private KMSingletonPattern() { }

            public KhuyenMai Init(string id,QuanLyBanSachEntities db,HttpResponseBase response)
        {
            KhuyenMai km = db.KhuyenMais.SingleOrDefault(n => n.MaKM == id);
            if (km == null)
            {              
                 response.StatusCode= 404;
                return null;
            }

            return km;
        }
       

        // http post
        public void Edit(QuanLyBanSachEntities db, KhuyenMai nxb, System.Web.Mvc.ModelStateDictionary modelState)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(nxb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //http post
        public void Create(QuanLyBanSachEntities db, KhuyenMai nxb, System.Web.Mvc.ModelStateDictionary modelState, dynamic ViewBag)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                db.KhuyenMais.Add(nxb);
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