using AutoMapper;
using ECommerceMVC.Data;
using ECommerceMVC.Helpers;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceMVC.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Hshop2023Context db;
        private readonly IMapper _mapper;

        public KhachHangController(Hshop2023Context context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region Register
        //Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        //Upload len db
        public IActionResult DangKy(RegisterVM model)
        {
            var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();
            if (ModelState.IsValid)
            {
                try
                {
                    //Them vao db phai dung automapper
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRandomKey();
                    //System tự sinh khi đăng ký, đổi mật khẩu
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true; //Se xu li khi dung mail de active
                    khachHang.VaiTro = 0;

                    // Upload hinh
                    //if (Hinh != null)
                    //{
                    //   khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    //}
                    //Luu vao db
                    db.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "HangHoa");
                }
                catch (Exception ex)
                {

                }
            }
            return View();
        }
        #endregion


        #region Login
        [HttpGet]
        // Chay chinh xac trang dang nhap thi vao luon
        // Khong thi phai luu dia chi trang
        public IActionResult DangNhap(string? ReturnUrl)
        {
            //Chuyen qua request page de xai
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            //Model da hop le thi moi kiem tra
            //Chua thi van tro ve form dang nhap
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("Loi", "Không có khách hàng");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("Loi", "Tài khoản đã bị khóa");
                    }
                    else
                    {
                        if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("Loi", "Sai mật khẩu"); //Đừng ghi như vậy =)) ghi sai thông tin login là đc
                        }
                        else
                        {
                            //Ghi nhan, chua thong tin user
                            var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Email, khachHang.Email),
                            new Claim(ClaimTypes.Name, khachHang.HoTen),
                            new Claim("CustomerID", khachHang.MaKh),

							//Claim dong - role dong
							new Claim(ClaimTypes.Role, "Customer"),
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
