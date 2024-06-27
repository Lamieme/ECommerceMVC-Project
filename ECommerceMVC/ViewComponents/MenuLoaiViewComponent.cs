using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace ECommerceMVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;

        public MenuLoaiViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            //Hien thi ra danh sach loai
            //Lay data (Ten va so luong san pham thuoc loai do)
            // Lay data tu db xong roi hien thi len tren man hinh -> no la DAO =)))
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai, 
                TenLoai = lo.TenLoai, 
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);
            return View(data);//tml nay goi len view cho m ne
            //Default.cshtml

            
        }
    }
}
