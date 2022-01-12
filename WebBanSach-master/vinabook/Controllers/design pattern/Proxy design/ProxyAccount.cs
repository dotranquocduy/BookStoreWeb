using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Vinabook.Models;
using static Vinabook.Models.KhachHang;

namespace Vinabook.Controllers.design_pattern.Proxy_design
{
    public enum CheckAccount
    {
        ErrorTaiKhoan,
        ErrorPassword,
        ErrorBirthDay,
        Success
    }
    public interface IAccount
    {
        CheckAccount UpdateAccount(QuanLyBanSachEntities db, UserAccount kh);
    }
   
    public class UserAccount : KhachHang, IAccount
    {
       
        public CheckAccount UpdateAccount(QuanLyBanSachEntities db,UserAccount kh)
        {
            KhachHang cus = new KhachHang();
            cus.TaiKhoan = kh.TaiKhoan;
            cus.NgaySinh = kh.NgaySinh;
            cus.MatKhau = kh.MatKhau;
            cus.MaKH = kh.MaKH;
            cus.GioiTinh = kh.GioiTinh;
            cus.HoTen = kh.HoTen;
            cus.Email = kh.Email;
            cus.DienThoai = kh.DienThoai;
            cus.DiaChi = kh.DiaChi;
            db.KhachHangs.Add(cus);
            db.SaveChanges();
            return CheckAccount.Success;
        }
    }

    public class ProxyAccount : IAccount
    {
        // Use 'lazy initialization'
        private UserAccount user;
        public ProxyAccount(UserAccount user)
        {
            this.user = user;
        }
         public CheckAccount UpdateAccount(QuanLyBanSachEntities db, UserAccount kh)
        {
            //StringComparison comp = StringComparison.OrdinalIgnoreCase;

            //read word not allowed login
            string[] linesNotAllowed;
            var list = new List<string>();
            var fileStream = new FileStream(@"F:\cong nghe thong tin\MauThietKe_DoAnCuoiKy\NotAllowedListContext.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            linesNotAllowed = list.ToArray();

            var age = DateTime.Now.Year - kh.NgaySinh.Value.Year;
            foreach(var item in linesNotAllowed)
            {
                if (user.TaiKhoan.Contains(item))
                {                   
                    return CheckAccount.ErrorTaiKhoan;
                }
                else if(user.MatKhau.Length < 6)
                {
                    return CheckAccount.ErrorPassword;
                }
                else if(age< 12)
                {
                    return CheckAccount.ErrorBirthDay;
                }
            }

            return user.UpdateAccount(db, kh);
            
        }
    }

}