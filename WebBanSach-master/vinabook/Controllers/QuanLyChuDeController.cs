using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vinabook.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using Vinabook.Controllers.design_pattern;
using System.Diagnostics;

namespace Vinabook.Controllers
{
    [Authorize]
    public class QuanLyChuDeController : ControllersTemplateDesign
    {
        // GET: QuanLyChuDe
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
       
        public QuanLyChuDeController()
        {
            PrintTemplateMethod();
        }
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.ChuDes.ToList().OrderBy(n => n.TenChuDe).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult IndexPartial(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(db.ChuDes.ToList().OrderBy(n => n.TenChuDe).ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Tao moi
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ChuDe cd)
        {
            ChuDeSingletonPattern.Instance.Create(db,cd,ModelState,ViewBag);
            return View();
        }


        /// <summary>
        /// Chinh Sua
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int MaCD)
        {
            var cd = ChuDeSingletonPattern.Instance.Init(MaCD,db,Response);
            return View(cd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ChuDe cd, FormCollection f)
        {
            ChuDeSingletonPattern.Instance.Edit(db,cd,ModelState);

            return RedirectToAction("Index");

        }

        /// <summary>
        /// Hien thi
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        public ActionResult Details(int MaCD)
        {

            var cd = ChuDeSingletonPattern.Instance.Init(MaCD,db,Response);
            return View(cd);

        }

        [HttpPost]
        public JsonResult XemCTCD(int macd)
        {
            TempData["macd"] = macd;
            return Json(new { Url = Url.Action("XemCTCDPartial") });
        }
        public PartialViewResult XemCTCDPartial()
        {
            int maCD = (int)TempData["macd"];
            var lstCD = ChuDeSingletonPattern.Instance.Init(maCD,db,Response);
            return PartialView(lstCD);
        }

        /// <summary>
        /// Xoa
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int MaCD)
        {
            ChuDe cd = ChuDeSingletonPattern.Instance.Init(MaCD,db,Response);
            return View(cd);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaCD)
        {
            ChuDe cd = ChuDeSingletonPattern.Instance.Init(MaCD,db,Response);
            db.ChuDes.Remove(cd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /////////////////////
        [HttpPost]
        public JsonResult Remove(int id)
        {
            ChuDe cd = ChuDeSingletonPattern.Instance.Init(id,db,Response);
            db.ChuDes.Remove(cd);
            db.SaveChanges();
            return Json(new { Url = Url.Action("IndexPartial") });
        }

        protected override void PrintRoutes()
        {
            
            Debug.WriteLine("======== ALL ROUTE INFORMATION ========");
            Debug.WriteLine($@"{GetType().Name}
            Routes: 
            GET: Admin/QuanLyChuDe
            GET: Admin/QuanLyChuDe/Edit/:MaCD
            GET: Admin/QuanLyChuDe/Delete/:MaCD
            POST: Admin/QuanLyChuDe/Create/:cd
            POST: Admin/QuanLyChuDe/Edit/:MaCD
            POST: Admin/QuanLyChuDe/XemCTCD/:macd
            POST: Admin/QuanLyChuDe/XacNhanXoa/:MaCD
            POST: Admin/QuanLyChuDe/Remove/:id
            ");
        }

        
    }


}