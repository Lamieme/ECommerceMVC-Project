using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ECommerceMVC.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommerceMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context db;

        //Can database nen add db
        public CartController(Hshop2023Context context)
        {
            db = context;
        }

        //Tao cart
        //Neu rong thi tao gio hang moi
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        //Ham hien thi index
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            //Dinh nghia gio hang = cart
            var gioHang = Cart;
            //Kiem tra coi item co trong gio hang chua theo mahh
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            //TH1: Neu item == 0, chua co trong gio hang
            //Tao moi, lay thong tin
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                //Truong hop truyen truc tiep qua url
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHh = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0, //Cho phep null
                    Hinh = hangHoa.Hinh ?? string.Empty, //Cho phep khong co hinh
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                //TH2: Da co san pham trong gio hang
                //Update so luong
                item.SoLuong += quantity;
            }
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
                return RedirectToAction("Index");
            }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }
    }
}
