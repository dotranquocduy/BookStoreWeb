using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;
using Vinabook.Controllers.design_pattern;
using Vinabook.Controllers.design_pattern.Facade_design;
namespace Vinabook.Controllers
{
    public class GioHangController : Controller
    {

        //Facade
        ControllerFacade Facade;

        public GioHangController()
        {
            Facade = new ControllerFacade();
        }

        

        public List<CartItem> LayGioHang()
        {

            return Facade.CheckCart();
        }

        public ActionResult Index()
        {

            if (Session["ShoppingCart"] != null)
            {
                if (((List<CartItem>)Session["ShoppingCart"]).Count > 0)
                {
                    //List<CartItem> listCart = LayGioHang();
                    //return View(listCart);
                    return View(Facade.CheckCart_QualityItem(LayGioHang()));
                }
                
            }
            return RedirectToAction("Index", "Home");
        }
        //dem so luong san pham
        public ActionResult GioHangPartial()
        {
            int cartcount = 0;
            //if (Session["ShoppingCart"] != null)
            //{
            //    List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            //    foreach (CartItem item in ls)
            //    {
            //        cartcount += item.Quality;
            //    }
            //}
            if (Session["ShoppingCart"] != null)
            {
                List<CartItem> listCart = (List<CartItem>)Session["ShoppingCart"];
                Iterator iterator = new CartItem_IteratorPattern(listCart);
                CartItem item = iterator.First();
                while (!iterator.IsCollectionEnds)
                {
                    cartcount += item.Quality;

                    item = iterator.Next();
                }
            }
            ViewBag.count = cartcount;
 
            return PartialView();
        }
        [HttpPost]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "NguoiDung");
            }

            return Json(new { Url = Url.Action("DatHangPartial") });
        }

        [HttpPost]

        public ActionResult DatHangPartial()
        {
            return PartialView("DatHangPartial");
        }
        [HttpPost]
        public ActionResult DatHangSubmit(string NgayGiao,string MaKM)
        {
            DonHang dh = new DonHang();
            if (Session["TaiKhoan"] != null || Session["TaiKhoan"].ToString() == "")
            {
                KhachHang customer = (KhachHang)Session["TaiKhoan"];
                if (ModelState.IsValid)
                {
                    dh.MaKH = customer.MaKH;
                    dh.NgayDat = DateTime.Now;
                    if (NgayGiao.Trim() != "")
                        dh.NgayGiao = Convert.ToDateTime(NgayGiao);

                    dh.TinhTrangGiaoHang = 0;
                    dh.DaThanhToan = "Chưa thanh toán";
                    if (MaKM != "" &&MaKM!="NULL")
                        dh.MaKM = MaKM;
                    Facade.Add_donhang(dh); //thêm đơn hàng
                    Facade.check_KhuyenMai(MaKM); // kiểm tra khuyến mãi
                }
            }
            if (Session["ShoppingCart"] != null)
            {
                List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            

                Iterator iterator = new CartItem_IteratorPattern(ls);
                var item = iterator.First();
                while (!iterator.IsCollectionEnds)
                {
                    ChiTietDonHang CTDH = new ChiTietDonHang();
                    CTDH.MaDonHang = dh.MaDonHang;
                    CTDH.MaSach = item.productOrder.MaSach;
                    CTDH.SoLuong = item.Quality;
                    CTDH.DonGia = item.productOrder.GiaBan;
                    //Facade chi tiết đơn vs so luong tồn sách
                    Facade.Add_ChiTietDonHang(CTDH);
                    Facade.Check_SoLuongTonSach(item);

                    item = iterator.Next();
                }

            }
            Session["ShoppingCart"] = null;
            return Json(new { success = "Đặt hàng thành công!!!" });
        }
        [HttpPost]
        public ActionResult BackToCart()
        {
            return Json(new { Url = Url.Action("Success") });
        }
        //cap nhat gio hang
        [HttpPost]
        public ActionResult CapNhat(int id, int sl)
        {
            List<CartItem> listCart = (List<CartItem>)Session["ShoppingCart"];
            //nếu người dùng thêm hàng vào giỏ và lại trở về trang chủ thêm hàng tiếp
            //thì session shoppingcart này có đang giữ tất cả sách trong giỏ hàng hiện tại hay không?
            //có.cập nhật vào Session["ShoppingCart"] mà
           

            int cartcount = 0;
            Iterator iterator = new CartItem_IteratorPattern(listCart);
            var item = iterator.First();
            while (!iterator.IsCollectionEnds)
            {
                if (item.productOrder.MaSach == id)
                    item.Quality = sl;

                cartcount += item.Quality;
                item = iterator.Next();
            }

            Session["ShoppingCart"] = listCart;

            return Json(new { Url = Url.Action("Success"), sl = cartcount });
        }
        [HttpPost]
        public ActionResult Success()
        {
            List<CartItem> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }
        //xoa gio hang
        [HttpPost]
        public ActionResult Remove(int id)
        {
            int cartcount = 0;
            List<CartItem> listCart = (List<CartItem>)Session["ShoppingCart"];
            
            Iterator iterator = new CartItem_IteratorPattern(listCart);
            var item = iterator.First();
            while (!iterator.IsCollectionEnds)
            {
                if (item.productOrder.MaSach == id)
                {
                    listCart.Remove(item);
                    //break;
                }

                cartcount += item.Quality;
                item = iterator.Next();
            }
            Session["ShoppingCart"] = listCart;
            return Json(new { Url = Url.Action("Success"), sl = cartcount });
        }
        [HttpPost]
        public ActionResult KhuyenMai(string id)
        {
            KhuyenMai km = Facade.Find_Id_KhuyenMai(id);
            if (km == null || km.NgayBDKM > DateTime.Now || km.NgayKTKM < DateTime.Now || km.DaSuDung==true)
            {
                return Json(new { tb="Lỗi!",id="",gt="" });
            }
            return Json(new { tb="Khuyến Mãi: ",id=id,gt=km.GiaTriKM+"%" });
        }
    }
}