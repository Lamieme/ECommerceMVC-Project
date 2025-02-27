﻿namespace ECommerceMVC.ViewModels
{
    //Chua du lieu day ra tren view
    public class MenuLoaiVM
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public int SoLuong { get; set; }
    }
    public class ChiTietHangHoaVM
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MoTaNgan { get; set; }
        public string ChiTiet { get; set; }
        public string TenLoai { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }
    }
}
