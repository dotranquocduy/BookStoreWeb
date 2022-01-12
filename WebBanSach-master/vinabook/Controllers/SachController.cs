using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;
using PagedList.Mvc;
using PagedList;
using Vinabook.Controllers.design_pattern.Facade_design;
using Vinabook.Controllers.design_pattern;

namespace Vinabook.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
      

        //Facade
        ControllerFacade Facade;

        public SachController()
        {
            Facade = new ControllerFacade();
        }


        public PartialViewResult SachMoiPartial()
        {
            return PartialView(Facade.DB_NewBook().Take(10).ToList());
        }
        public ActionResult SachMoiNhapVe_Partial()
        {
            ViewBag.skin = "primary";
            ViewBag.Title = "Sách Mới Nhập Về";
            return PartialView(Facade.DB_NCN_Book().Take(5).ToList());
        }
        public ActionResult SachGiamGia_Partial()
        {
            ViewBag.skin = "warning";
            ViewBag.Title = "Sách Giảm Giá";
            return PartialView(Facade.DB_DesPrice_Book().Take(5).ToList());
        }
        public ActionResult SachCoTheBanQuanTam_Partial()
        {
            ViewBag.skin = "warning";
            ViewBag.Title = "Có Thể Bạn Quan Tâm";
            return PartialView(Facade.DB_Hint_Book().Take(10).ToList());
        }
        /// <summary>
        /// Sach moi tren menu
        /// </summary>
        /// <returns></returns>
   
        public ActionResult Sach(int? page)
        {  
            int pageNumber = (page ?? 1);
            int pageSize = 12;
  
            return View(Facade.DB_NewBook().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost, ActionName("Sach")]
        public PartialViewResult Sach(int? page, string x)
        {
            string temp = "";
            ISach sach1 = new SachKhuyenMaiDecorator();
            ISach sach2 = new SachMoiNhapDecorator();
            ISach sach3 = new SachYeuThichDecorator();
            int pageNumber = (page ?? 1);
            int pageSize = 12;     
               
                if (x == "0")
                {                 
                        return PartialView(sach1.usesach().ToPagedList(pageNumber, pageSize));
                }
                else if (x == "1")
                {
                    temp = x[0] + temp;
                    return PartialView(sach2.usesach().ToPagedList(pageNumber, pageSize));
                }
                else if (x == "2")
                {
                    return PartialView(sach3.usesach().ToPagedList(pageNumber, pageSize));
                }

            return PartialView(Facade.DB_NewBook().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
       
        }

        public ActionResult SachTheoTacGia(int matacgia = 1, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            
            ViewBag.TenTacGia = Facade.Ten_TacGia(matacgia);
            return View(Facade.SapXep_MaTacGia(matacgia).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SachTheoChuDe(int machude = 1, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            ViewBag.TenChuDe = Facade.Ten_ChuDe(machude);
            return View(Facade.ChuDeSach(machude).OrderBy(n => n.MaChuDe).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SachTheoNhaXuatBan(int manxb = 1, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            ViewBag.NhaXuatBan = Facade.Ten_NXB(manxb);
            return View(Facade.NXBSach(manxb).OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult SachTiengAnhPartial()
        {
            int MaChuDe = 2;
            ViewBag.MaChuDe = MaChuDe;
            return PartialView(Facade.ChuDeSach(MaChuDe).Take(3).ToList());
        }
        public PartialViewResult SachITPartial()
        {
            int MaChuDe = 1;
            ViewBag.MaChuDe = MaChuDe;
            return PartialView(Facade.ChuDeSach(MaChuDe).Take(3).ToList());
        }
        public PartialViewResult SachPhatGiaoPartial()
        {
            int MaChuDe = 3;
            ViewBag.MaChuDe = MaChuDe;
            return PartialView(Facade.ChuDeSach(MaChuDe).Take(3).ToList());
        }
        public ActionResult sachCungChuDePartial(int machude)
        {
            int id = Convert.ToInt16(TempData["MaChuDe"]);

            return PartialView(Facade.ChuDeSach(machude).Take(10).ToList());
        }
        
        public PartialViewResult SachGanDayPartial()
        {
            return PartialView(Facade.DB_NCN_Book());
        }
        
        public ViewResult XemChiTiet(int MaSach = 0)
        {
            Sach sach = Facade.Find_Id_Sach(MaSach);
            if (sach == null)
            {
                //Trả về trang báo lỗi 
                Response.StatusCode = 404;
                return null;
            }
            //ChuDe cd = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe);
            //ViewBag.TenCD = cd.TenChuDe;

            ViewBag.TenChuDe = Facade.Ten_ChuDe(sach.MaChuDe);
            ViewBag.NhaXuatBan = Facade.Ten_NXB(sach.MaNXB);
            return View(sach);
        }
    
      
        [HttpPost]
        public JsonResult AddToCart(int? id, int chiTietSl)
        {
            List<CartItem> listCart=null;
            //Process Add To Cart
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session
                listCart = new List<CartItem>();
                listCart.Add(new CartItem { Quality = chiTietSl, productOrder = Facade.Find_Id_Sach(id) });
                Session["ShoppingCart"] = listCart;

            }
            else
            {
                bool flag = false;
                listCart = (List<CartItem>)Session["ShoppingCart"];

                Iterator iterator1 = new CartItem_IteratorPattern(listCart);
                var item1 = iterator1.First();
                while (!iterator1.IsCollectionEnds)
                {
                    if (item1.productOrder.MaSach == id)
                    {
                        item1.Quality += chiTietSl;
                        flag = true;
                        break;
                    }
                    item1 = iterator1.Next();
                }
                if (!flag)
                    listCart.Add(new CartItem { Quality = chiTietSl, productOrder = Facade.Find_Id_Sach(id)});
                Session["ShoppingCart"] = listCart;
            }

            //Count item in shopping cart
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            Iterator iterator2 = new CartItem_IteratorPattern(ls);
            var item2 = iterator2.First();
            while (!iterator2.IsCollectionEnds)
            {
                
                cartcount += item2.Quality;
                item2 = iterator2.Next();
            }
            return Json(new { ItemAmount = cartcount });
        }

        //public ViewResult SachKhuyenMai()
        //{
        //    return View("Views/Shared/_Partial_1_Sach.cshtml");
        //}
    }
}