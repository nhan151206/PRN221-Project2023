using System;
using System.Collections.Generic;

namespace Project2023PRN221.Models
{
    public partial class TblHoadon
    {
        public TblHoadon()
        {
            TblChiTietHds = new HashSet<TblChiTietHd>();
        }

        public decimal MaHd { get; set; }
        public string? MaKh { get; set; }
        public DateTime? NgayHd { get; set; }

        public virtual TblKhachHang? MaKhNavigation { get; set; }
        public virtual ICollection<TblChiTietHd> TblChiTietHds { get; set; }
    }
}
