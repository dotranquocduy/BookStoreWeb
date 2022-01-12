using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vinabook.Models;
using PagedList;
using PagedList.Mvc;
using Vinabook.Controllers.design_pattern;
using System.Diagnostics;

namespace Vinabook.Controllers
{
    [Authorize]
    public class QuanLyNXBController : ControllersTemplateDesign
    {
        // GET: QuanLyNXB
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB).ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(NhaXuatBan nxb)
        {
            NXBSingletonPattern.Instance.Create(db, nxb, ModelState, ViewBag);
            return View();
        }
        /// <summary>
        /// Chinh Sua
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int MaNXB)
        {
            var nxb = NXBSingletonPattern.Instance.Init(MaNXB,db,Response);

            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NhaXuatBan nxb, FormCollection f)
        {
            NXBSingletonPattern.Instance.Edit(db, nxb, ModelState);

            return RedirectToAction("Index");

        }
        /// <summary>
        /// Hien thi
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        public ActionResult Details(int MaNXB)
        {

            var nxb = NXBSingletonPattern.Instance.Init(MaNXB, db, Response);

            return View(nxb);

        }
        /// <summary>
        /// Xoa
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int MaNXB)
        {
            //Lấy ra đối tượng sách theo mã 
            var nxb = NXBSingletonPattern.Instance.Init(MaNXB, db, Response);
            return View(nxb);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaNXB)
        {
            var nxb = NXBSingletonPattern.Instance.Init(MaNXB, db, Response);
            db.NhaXuatBans.Remove(nxb);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        protected override void PrintRoutes()
        {

            Debug.WriteLine("======== ALL ROUTE INFORMATION ========");
            Debug.WriteLine($@"{GetType().Name}
            Routes: 
            GET: Admin/QuanLyNXB
            GET: Admin/QuanLyNXB/Edit/:MaNXB
            GET: Admin/QuanLyNXB/Delete/:MaNXB
            POST: Admin/QuanLyNXB/Create/:MaNXB
            POST: Admin/QuanLyNXB/Edit/:MaNXB
            POST: Admin/QuanLyNXB/XemCTCD/:MaNXB
            POST: Admin/QuanLyNXB/XacNhanXoa/:MaNXB
            POST: Admin/QuanLyNXB/Remove/:MaNXB
            ");
        }
    }
}