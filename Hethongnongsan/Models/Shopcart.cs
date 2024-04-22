using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Hethongnongsan.Models
{
    public partial class Shopcart
    {
        public int Idshopcart { get; set; }
        public int? Idnguoidung { get; set; }
        public int? Idsanpham { get; set; }
        public int? Soluong { get; set; }
        public int? Status { get; set; }
        public int? Idshop { get; set; }
        public DateTime? Ngaymua { get; set; }
        public string Diemden { get; set; }
        public double? Tongtien { get; set; }
    }
}
