using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vinabook.Controllers.design_pattern.Prototype_design;
namespace Vinabook.Models
{
    [MetadataTypeAttribute(typeof(SachMetadata))]
    public partial class Sach :SachPrototype
    {
        public SachPrototype Clone()
        {
            Sach sach = new Sach();
            sach.MaSach = MaSach;
            sach.TenSach = TenSach;
            sach.GiaBan = GiaBan;
            sach.MoTa = MoTa;
            sach.NgayCapNhat = NgayCapNhat;
            sach.AnhBia = AnhBia;
            sach.SoLuongTon = SoLuongTon;
            sach.MaChuDe = MaChuDe;
            sach.MaNXB = MaNXB;
            sach.Moi = Moi;
            return sach;
        }

        internal sealed class SachMetadata
        {
            [Display(Name = "Mã sách")]
            public int MaSach { get; set; }

            [Display(Name = "Tên sách")]
            public string TenSach { get; set; }

            [Display(Name = "Giá bán")]
            public decimal GiaBan { get; set; }

            [Display(Name = "Mô tả")]
            public string MoTa { get; set; }

            [Display(Name = "Ngày cập nhật")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime NgayCapNhat { get; set; }

            [Display(Name = "Ảnh bìa")]
            public string AnhBia { get; set; }

            [Display(Name = "Số lượng tồn")]
            public int SoLuongTon { get; set; }

            [Display(Name = "Mã chủ đề")]
            public int MaChuDe { get; set; }

            [Display(Name = "Mã nhà xuất bản")]
            public int MaNXB { get; set; }

            [Display(Name = "Mới")]
            public int Moi { get; set; }

        }
    }
}