using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Models;

namespace Vinabook.Controllers.design_pattern.Facade_design
{
    public class ControllerFacade
    {
        public DatabaseFacade db;
        public CartFacade giohang;
    

        public ControllerFacade()
        {
            db = new DatabaseFacade();
            giohang = new CartFacade();
          
        }

        /// <summary>
        /// CartFacade
        /// </summary>
            
        // Check giỏ hàng trống
        public List<CartItem> CheckCart()
        {
           return giohang.CheckCart();
        }

        // check số lượng giỏ hàng
        public List<CartItem> CheckCart_QualityItem(List<CartItem> listCart)
        {
            return giohang.CheckCart_QualityItem(listCart);
        }

        

        /// <summary>
        /// DatabaseFacade
        /// </summary>

        //tìm id khuyến mãi; lấy từ database
        public KhuyenMai Find_Id_KhuyenMai(string id)
        {
            return db.Find_Id_KhuyenMai(id);
        }

        //Thêm đơn hàng vào database
        public void Add_donhang(DonHang dh)
        {
            db.Add_donhang(dh);
        }

        //Kiểm tra mã khuyến mãi
        public void check_KhuyenMai(String MaKM)
        {
            db.check_KhuyenMai(MaKM);
        }

        //kiểm tra số lượng tồn sách 
        public void Check_SoLuongTonSach(CartItem item)
        {
            db.Check_SoLuongTonSach(item);
        }

        //Thêm chi tiết đơn hàng
        public void Add_ChiTietDonHang(ChiTietDonHang CTDH)
        {
            db.Add_ChiTietDonHang(CTDH);
        }

        //tìm id sách; lấy từ database
        public Sach Find_Id_Sach(int? id)
        {
           return db.Find_Id_Sach(id);
        }

        // database sách  
        public List<Sach> DB_NewBook()
        {
           return db.DB_NewBook();
        }

        // sắp sách theo ngày cập nhật  
        public List<Sach> DB_NCN_Book()
        {
            return db.DB_NCN_Book();
        }

        // sắp sách theo từ nhỏ đến lớn  
        public List<Sach> DB_DesPrice_Book()
        {
            return db.DB_DesPrice_Book();
        }

        // sắp sách theo từ nhỏ đến lớn  
        public List<Sach> DB_Hint_Book()
        {
            return db.DB_Hint_Book();
        }

        //Tìm sách theo mã chủ đề
        public List<Sach> ChuDeSach(int id)
        {
            return db.ChuDeSach(id);
        }

        //Tìm sách theo nhà xuất bản
        public List<Sach> NXBSach(int id)
        {
            return db.NXBSach(id);
        }

        //Tên nhà xuất bản
        public string Ten_NXB(int? id)
        {
            return db.Ten_NXB(id);
        }
        //Tên chủ đề
        public string Ten_ChuDe(int? id)
        {
            return db.Ten_ChuDe(id);
        }
        //Tên tác giả
        public string Ten_TacGia(int? id)
        {
            return db.Ten_TacGia(id);
        }

        //Sắp sách theo mã tác giả 
        public List<Sach> SapXep_MaTacGia(int id)
        {

            return db.SapXep_MaTacGia(id);
        }


    }
}