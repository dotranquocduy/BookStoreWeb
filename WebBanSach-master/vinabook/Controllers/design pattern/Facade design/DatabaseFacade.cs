using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vinabook.Models;



namespace Vinabook.Controllers.design_pattern.Facade_design
{
    public class DatabaseFacade
    {
       private QuanLyBanSachEntities db = new QuanLyBanSachEntities();


        //tìm id khuyến mãi; lấy từ database
        public KhuyenMai Find_Id_KhuyenMai(string id)
        {
            KhuyenMai km = db.KhuyenMais.Find(id);
            return km;
        }

        //Kiểm tra mã khuyến mãi
        public void check_KhuyenMai(string MaKM)
        {
            if (MaKM != "" && MaKM != "NULL")
                foreach (var item in db.KhuyenMais)
                {
                    if (item.MaKM == MaKM.ToUpper())
                    {
                        item.DaSuDung = true;
                        break;
                    }
                }
            db.SaveChanges();
        }

        //Thêm đơn hàng vào database
        public void Add_donhang(DonHang dh)
        {
            db.DonHangs.Add(dh);
            db.SaveChanges();
        }

        //kiểm tra số lượng tồn sách 
        public void Check_SoLuongTonSach(CartItem item)
        {
            Sach sach = db.Saches.Find(item.productOrder.MaSach);
            sach.SoLuongTon -= item.Quality;
            db.SaveChanges();
        }

        //Thêm chi tiết đơn hàng
        public void Add_ChiTietDonHang(ChiTietDonHang CTDH)
        {
            db.ChiTietDonHangs.Add(CTDH);
        }


        //tìm id sách; lấy từ database
        public Sach Find_Id_Sach(int? id)
        {
            Sach sach = db.Saches.Find(id);
            return sach;
        }


        //lấy số lượng sách theo sách mới  
        public List<Sach> DB_NewBook()
        {
           return db.Saches.Where(n => n.Moi == 1).ToList();
        }

        // lấy số lượng sách theo ngày cập nhật  
        public List<Sach> DB_NCN_Book()
        {
            return db.Saches.OrderBy(n => n.NgayCapNhat).ToList();
        }

        //lấy số lượng sách theo giá bán từ nhỏ đến lớn  
        public List<Sach> DB_DesPrice_Book()
        {
            return db.Saches.OrderByDescending(n => n.GiaBan).ToList();
        }

        // sắp sách theo từ nhỏ đến lớn  
        public List<Sach> DB_Hint_Book()
        {
            return db.Saches.OrderBy(n => Guid.NewGuid()).ToList();
        }

        //Tìm sách theo mã chủ đề
        public List<Sach> ChuDeSach(int id)
        {
            return db.Saches.Where(n => n.MaChuDe == id).ToList();
        }
        //Tìm sách theo nhà xuất bản
        public List<Sach> NXBSach(int id)
        {
            return db.Saches.Where(n => n.MaNXB == id).ToList();
        }

       

        //Tên nhà xuất bản
        public string Ten_NXB(int? id)
        {
            return db.NhaXuatBans.Single(n => n.MaNXB == id).TenNXB;
        }
        //Tên chủ đề
        public string Ten_ChuDe(int? id)
        {
            return db.ChuDes.Single(n => n.MaChuDe == id).TenChuDe;
        }
        //Tên tác giả
        public string Ten_TacGia(int? id)
        {
            return db.TacGias.Single(n => n.MaTacGia == id).TenTacGia;
        }

        //Sắp sách theo mã tác giả 
        public List<Sach> SapXep_MaTacGia(int id)
        {

            var list = (from s in db.ThamGias
                        where s.MaTacGia == id
                        select s).ToList();
            var listSach = new List<Sach>();
            foreach (var item in list)
            {
                var Sachs = (from s in db.Saches
                             where s.MaSach == item.MaSach
                             select s).ToList();
                foreach (var iteamsach in Sachs)
                {
                    listSach.Add(iteamsach);
                }
            }

            return listSach.OrderBy(n => n.TenSach).ToList();
        }

    }
}