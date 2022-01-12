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
    public class QuanLyKhuyenMaiController : ControllersTemplateDesign
    {
        // GET: QuanLyKhuyenMai
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.KhuyenMais.ToList().OrderBy(n => n.MaKM).ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(KhuyenMai nxb)
        {
            KMSingletonPattern.Instance.Create(db, nxb, ModelState, ViewBag);
            return View();
        }
        /// <summary>
        /// Chinh Sua
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string MaNXB)
        {
            //Lấy ra đối tượng sách theo mã 
            var nxb=KMSingletonPattern.Instance.Init(MaNXB,db,Response);
            ViewBag.DaSuDung = nxb.DaSuDung.ToString();
            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(KhuyenMai nxb, FormCollection f)
        {
            //Thêm vào cơ sở dữ liệu
            KMSingletonPattern.Instance.Edit(db, nxb, ModelState);
            return RedirectToAction("Index");

        }
        /// <summary>
        /// Hien thi
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        public ActionResult Details(string MaNXB)
        {

            //Lấy ra đối tượng sách theo mã 
            var nxb= KMSingletonPattern.Instance.Init(MaNXB,db,Response);

            return View(nxb);

        }
        /// <summary>
        /// Xoa
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(string MaNXB)
        {
            var nxb= KMSingletonPattern.Instance.Init(MaNXB, db, Response);

            return View(nxb);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(string MaNXB)
        {
            var nxb = KMSingletonPattern.Instance.Init(MaNXB, db, Response);
            db.KhuyenMais.Remove(nxb);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        protected override void PrintRoutes()
        {

            Debug.WriteLine("======== ALL ROUTE INFORMATION ========");
            Debug.WriteLine($@"{GetType().Name}
            Routes: 
            GET: Admin/QuanLyKhuyenMai
            GET: Admin/QuanLyKhuyenMai/Edit/:nxb
            GET: Admin/QuanLyKhuyenMai/Delete/:nxb
            POST: Admin/QuanLyKhuyenMai/Create/:nxb
            POST: Admin/QuanLyKhuyenMai/Edit/:nxb
            POST: Admin/QuanLyKhuyenMai/XemCTCD/:nxb
            POST: Admin/QuanLyKhuyenMai/XacNhanXoa/:nxb
            POST: Admin/QuanLyKhuyenMai/Remove/:nxb
            ");
        }
    }
}