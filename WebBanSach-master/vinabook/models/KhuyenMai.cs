//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vinabook.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuyenMai()
        {
            this.DonHangs = new HashSet<DonHang>();
        }
    
        public string MaKM { get; set; }
        public string GiaTriKM { get; set; }
        public Nullable<System.DateTime> NgayBDKM { get; set; }
        public Nullable<System.DateTime> NgayKTKM { get; set; }
        public bool DaSuDung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
