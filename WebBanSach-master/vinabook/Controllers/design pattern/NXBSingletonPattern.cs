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
    public sealed class NXBSingletonPattern
    {
        
        public static NXBSingletonPattern Instance { get; } = new NXBSingletonPattern();       
            private NXBSingletonPattern() { }

            public NhaXuatBan Init(int id,QuanLyBanSachEntities db,HttpResponseBase response)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {              
                 response.StatusCode= 404;
                return null;
            }

            return nxb;
        }
       

        // http post
        public void Edit(QuanLyBanSachEntities db, NhaXuatBan nxb, System.Web.Mvc.ModelStateDictionary modelState)
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
        public void Create(QuanLyBanSachEntities db, NhaXuatBan nxb, System.Web.Mvc.ModelStateDictionary modelState, dynamic ViewBag)
        {
            //Thêm vào cơ sở dữ liệu
            if (modelState.IsValid)
            {
                db.NhaXuatBans.Add(nxb);
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