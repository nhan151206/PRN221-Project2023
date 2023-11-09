using System;
using System.Collections.Generic;

namespace Project2023PRN221.Models
{
    public partial class TblMatHang
    {
        public TblMatHang()
        {
            TblChiTietHds = new HashSet<TblChiTietHd>();
        }

        public string MaHang { get; set; } = null!;
        public string? TenHang { get; set; }
        public string? Dvt { get; set; }
        public float? Gia { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TblChiTietHd> TblChiTietHds { get; set; }
    }
}
