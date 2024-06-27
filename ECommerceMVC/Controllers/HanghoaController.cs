using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;

namespace ECommerceMVC.Controllers
{
    public class HanghoaController : Controller
    {
        private readonly Hshop2023Context db;
        public HanghoaController(Hshop2023Context context)
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var hanghoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hanghoas = hanghoas.Where(p => p.MaLoai == loai.Value);
            }
            // Show item
            var result = hanghoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0, //Cho phep null
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTa ?? "", //Cho phep rong
                TenLoai = p.MaLoaiNavigation.TenLoai // Nhay qua ma loai navigation de lay ten loai
            });
            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var hanghoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hanghoas = hanghoas.Where(p => p.TenHh.Contains(query));
            }
            // Show item
            var result = hanghoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0, //Cho phep null
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTa ?? "", //Cho phep rong
                TenLoai = p.MaLoaiNavigation.TenLoai // Nhay qua ma loai navigation de lay ten loai
            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            //Lay tu danh sach cac hang hoa
            //Dieu kien
            //p.MaHh bang id hay ko
            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id); //Lay toan bo thong tin roi
            if (data == null)
            {
                //TempData tai vi day qua controller khac
                //Da chuyen controller nen bat buoc phai co
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            // Khong chuyen entity model qua
            // Chuyen noi dung
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty, //Cho phep null
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10, //Them vao db sau
                DiemDanhGia = 5 // Check sau
            };
            return View("Detail", result);

        }
    }

}
