using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using helper;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Security.Cryptography;
using API_He_thong.DATA;
using System.Net.Mail;
using System.Net;
namespace BeeMatchingAPP.Controllers
{
    //tuan day
    //tuan cong
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _httpClient = httpClient;
            _hostingEnvironment = hostingEnvironment;
        }
        private DoanhNghiep thongtindoanhnghiep
        {
            get
            {
                // Truy xuất danh sách người tìm việc từ Session
                return HttpContext.Session.Get<DoanhNghiep>("ThongTindoanhnghiep") ?? new DoanhNghiep();
            }
        }
        private NguoiTimViec TrangThongTinCaNhan
        {
            get
            {
                // Truy xuất danh sách người tìm việc từ Session
                return HttpContext.Session.Get<NguoiTimViec>("ThongTin") ?? new NguoiTimViec();
            }
        }
        // Thuộc tính để truy xuất dữ liệu từ session
        private UngTuyen DuLieuTrangUngTuyen
        {
            get
            {
                // Truy xuất đối tượng UngTuyen từ session, hoặc trả về một thể hiện mới nếu không tìm thấy
                return HttpContext.Session.Get<UngTuyen>("ungtuyen") ?? new UngTuyen();
            }
        }
        private DanhGia DuLieuTrangDanhGia
        {
            get
            {
                // Truy xuất đối tượng UngTuyen từ session, hoặc trả về một thể hiện mới nếu không tìm thấy
                return HttpContext.Session.Get<DanhGia>("danhgia") ?? new DanhGia();
            }
        }
        // Phương thức xử lý trang thông tin cá nhân cho người tìm việc
        public async Task<ActionResult> TrangThongTinNguoiTimViec()
        {
            // Lấy thông tin cá nhân từ session hoặc mô hình
            var data = TrangThongTinCaNhan;
            if (data == null || string.IsNullOrWhiteSpace(data.email))
            {
                // Điều hướng đến trang thêm thông tin nếu chưa có email
                return RedirectToAction("TrangaddThongtin", "Home");
            }

            ViewData["HoSo"] = data;
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> TrangaddThongtin()
        {
            // Lấy dữ liệu hoặc khởi tạo dữ liệu mặc định
            var data = TrangThongTinCaNhan ;
            ViewData["HoSo"] = data;
            return View();
        }
        public async Task<ActionResult> TrangThongTinDoanhNghiep()
        {
            // Truy xuất dữ liệu cá nhân (được giả định là từ mô hình hoặc session)
            var data = thongtindoanhnghiep;
            ViewData["HoSodn"] = data;
            return View(data);
        }
        [HttpGet]
        public async Task<ActionResult> TrangDanhGia(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/UngTuyen/GetById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Không thể lấy UngTuyen từ API.");
            }
            var apiResponse = await response.Content.ReadAsStringAsync();
            var ungTuyen = JsonConvert.DeserializeObject<UngTuyen>(apiResponse);
            var data = DuLieuTrangDanhGia;
            ViewData["danhgia"] = data;
            // Truyền dữ liệu vào view thông qua ViewData
            ViewData["id"] = ungTuyen;

            // Trả về view với dữ liệu ứng tuyển
            return View();
        }
        // Phương thức hiển thị thông tin ứng tuyển
        [HttpGet]
        public async Task<ActionResult> TrangThongTinUngTuyen(int id)
        {
            // Truy xuất dữ liệu từ session
            var data = DuLieuTrangUngTuyen;
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/CongViec/GetById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Không thể lấy công việc từ API.");
            }
            var apiResponse = await response.Content.ReadAsStringAsync();
            var congviec = JsonConvert.DeserializeObject<CongViec>(apiResponse);
            data.CongViecId = congviec.CongViecId;
            // ViewData["id"] = congviec;
            // Truyền dữ liệu vào view thông qua ViewData
            ViewData["ungtuyen"] = data;

            // Trả về view với dữ liệu ứng tuyển
            return View(data);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TrangThongTinUngTuyen(UngTuyen ut, IFormFile hinh_anh)
        {
            // Kiểm tra nếu hinh_anh không phải null
            if (hinh_anh == null || hinh_anh.Length == 0)
            {
                ModelState.AddModelError("hinh_anh", "Vui lòng chọn hình ảnh.");
                return View(ut);
            }

            // Kiểm tra phần mở rộng của tệp
            string fileExtension = Path.GetExtension(hinh_anh.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError(string.Empty, "Chỉ chấp nhận tệp hình ảnh (.jpg, .jpeg, .png, .gif).");
                return View(ut);
            }

            // Kiểm tra MIME type
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedMimeTypes.Contains(hinh_anh.ContentType))
            {
                ModelState.AddModelError(string.Empty, "Loại tệp không được hỗ trợ.");
                return View(ut);
            }

            // Kiểm tra dung lượng tệp (giới hạn 5MB)
            if (hinh_anh.Length > 5 * 1024 * 1024) // 5MB
            {
                ModelState.AddModelError(string.Empty, "Dung lượng tệp không được vượt quá 5MB.");
                return View(ut);
            }

            // Tạo tên tệp (dùng Guid nếu UngTuyenId chưa có)
            string fileName = ut.UngTuyenId > 0 ?
                "nv" + ut.UngTuyenId.ToString() + fileExtension :
                Guid.NewGuid().ToString() + fileExtension;

            // Đường dẫn đến thư mục lưu tệp
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload", "UngTuyen", fileName);

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Lưu tệp vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await hinh_anh.CopyToAsync(stream);
            }

            // Cập nhật tên tệp vào trường hinh_anh trong model
            ut.HinhAnhCV = fileName;

            // Tiến hành lưu dữ liệu vào cơ sở dữ liệu, hoặc thực hiện các bước tiếp theo
            var content = new StringContent(JsonConvert.SerializeObject(ut), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7287/api/UngTuyen/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CongViec", "CongViec"); // Chuyển hướng khi thành công
            }

            // Nếu có lỗi với API
            var responseContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi từ API: {responseContent}");
            return View(); // Trả về view với thông báo lỗi
        }
        private async Task ThemVaoTrangThongTinDoanhNghiep(int id)
        {
           // Khai báo đối tượng người tìm việc
            DoanhNghiep userInfo = null;

            // Gọi API để lấy danh sách người tìm việc
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/Company/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + apiResponse);  // Log để kiểm tra API trả về gì
                var allUsers = JsonConvert.DeserializeObject<List<DoanhNghiep>>(apiResponse);

                // Tìm kiếm người dùng theo ID
                userInfo = allUsers.Find(p => p.NguoiDungId == id);
            }


            // Nếu không tìm thấy người dùng, throw exception
            if (userInfo == null)
            {
                var newItem = new DoanhNghiep
                {
                    DoanhNghiepId = 0,
                    NguoiDungId = id,
                    HinhAnh = "",
                    TrangThai = "",
                    TenCongTy = "",
                    email = "",
                    DistrictId = "",
                    WardId = "",
                    ProvinceId = "",
                    MoTa = "",

                };

                //Lưu thông tin người dùng vào Session
                HttpContext.Session.Set("ThongTindoanhnghiep", newItem);

            }

            // Truy xuất thông tin người dùng từ Session
            //   var data = TrangThongTinCaNhan;

            // Tạo đối tượng mới từ thông tin người dùng
            else
            {
                var newItem = new DoanhNghiep
                {
                    DoanhNghiepId = userInfo.DoanhNghiepId,
                    NguoiDungId = id,
                    HinhAnh = userInfo.HinhAnh,
                    TrangThai = userInfo.TrangThai,
                    TenCongTy = userInfo.TenCongTy,
                    email = userInfo.email,
                    DistrictId = userInfo.DistrictId,
                    WardId = userInfo.WardId,
                    ProvinceId = userInfo.ProvinceId,
                    MoTa = userInfo.MoTa,

                };

                //Lưu thông tin người dùng vào Session
                HttpContext.Session.Set("ThongTindoanhnghiep", newItem);

                var ut = DuLieuTrangUngTuyen;
               
                var newItem1 = new DanhGia
                {
                    DoanhNghiepId = userInfo.DoanhNghiepId,
                    DiemDanhGia = 0,
                    NoiDungDanhGia = "",
                    UngTuyenId = ut.UngTuyenId,
                    NgayDanhGia = DateTime.Now,
                    
                };

                // Lưu thông tin ứng tuyển vào session
                HttpContext.Session.Set("danhgia", newItem1);
            }
         
        }
        private async Task ThemVaoTrangThongTinNguoiTimViec(int id)
        {
            
            // Khai báo đối tượng người tìm việc
            NguoiTimViec userInfo = null;

            // Gọi API để lấy danh sách người tìm việc
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/NguoiTimViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + apiResponse);  // Log để kiểm tra API trả về gì
                var allUsers = JsonConvert.DeserializeObject<List<NguoiTimViec>>(apiResponse);

                // Tìm kiếm người dùng theo ID
                userInfo = allUsers.Find(p => p.NguoiDungId == id);
            }
            else
            {
                throw new Exception("Không thể lấy danh sách người tìm việc từ API.");
            }

            // Nếu không tìm thấy người dùng, throw exception
            if (userInfo == null)
            {
                var Item1 = new NguoiTimViec
                {
                    NguoiDungId = id,
                    HinhAnh = "",
                    so_dien_thoai = "",
                    gioi_tinh = "",
                    email = "",
                    DistrictId = "",
                    dia_chi_nha = "",
                    WardId = "",
                    ProvinceId = "",
                    ho_ten = "",
                    KinhNghiem = "",
                    HoatDongNgoaiKhoa = "",
                    MoTa = "",
                    NgonNgu = "",
                    trang_thai = ""
                };

                //Lưu thông tin người dùng vào Session
                HttpContext.Session.Set("ThongTin", Item1);
            }

            // Truy xuất thông tin người dùng từ Session
            //   var data = TrangThongTinCaNhan;

            // Tạo đối tượng mới từ thông tin người dùng
            else if (userInfo != null)
            {
                var newItem = new NguoiTimViec
                {
                    NguoiTimViecId = userInfo.NguoiTimViecId,
                    NguoiDungId = id,
                    HinhAnh = userInfo.HinhAnh,
                    so_dien_thoai = userInfo.so_dien_thoai,
                    gioi_tinh = userInfo.gioi_tinh,
                    email = userInfo.email,
                    DistrictId = userInfo.DistrictId,
                    dia_chi_nha = userInfo.dia_chi_nha,
                    WardId = userInfo.WardId,
                    ProvinceId = userInfo.ProvinceId,
                    ho_ten = userInfo.ho_ten,
                    KinhNghiem = userInfo.KinhNghiem,
                    ngay_sinh = userInfo.ngay_sinh,
                    HoatDongNgoaiKhoa = userInfo.HoatDongNgoaiKhoa,
                    MoTa = userInfo.MoTa,
                    ngay_tao = userInfo.ngay_tao,
                    NgonNgu = userInfo.NgonNgu,
                    trang_thai = userInfo.trang_thai
                };

                //Lưu thông tin người dùng vào Session
                HttpContext.Session.Set("ThongTin", newItem);

                // Tạo mới đối tượng ứng tuyển
                var newItem1 = new UngTuyen
                {
                    NguoiTimViecId = userInfo.NguoiTimViecId,
                    NgayUngTuyen = DateTime.Now,
                    DeXuat = "",
                    TrangThai = "Đang xem xét",
                    ChapNhanCongViec = false
                };

                // Lưu thông tin ứng tuyển vào session
                HttpContext.Session.Set("ungtuyen", newItem1);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(NguoiDung model)
        {
            string otp = HttpContext.Session.GetString("otp");
            if (ModelState.IsValid)
            {
                if (model.Otp == otp)
                {
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:7287/api/User/Create", model);

                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Login");

                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "dang ki that bại");
                }


            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(string gmail)
        {
            int num;
            Random random = new Random();
            num = random.Next(1000, 5000);
            string toStringNum = num.ToString();
            HttpContext.Session.SetString("otp", toStringNum);

            string from, to, pass, email;

            from = "tuanden090304@gmail.com";
            pass = "ltux uibp hauy rcrb";
            to = gmail;

            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.From = new MailAddress(from);
            mm.Subject = "Mã OTP";
            mm.Body = toStringNum;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(mm);
                string alert = "Đã gửi OTP qua gmail!!";
                ViewData["Otp"] = alert.ToString();
                return RedirectToAction("Register", new { message = "Đã gửi OTP thành công!" });
            }
            catch (Exception ex)
            {
                string alert = "Gui otp that bai!!";
                ViewData["Otp"] = alert.ToString();
                return RedirectToAction("Register", new { message = "Đã gửi OTP thất bại!" });

            }

        }
        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendOtpForgetPassword(string gmail)
        {
            int num;
            Random random = new Random();
            num = random.Next(1000, 5000);
            string toStringNum = num.ToString();
            HttpContext.Session.SetString("otp", toStringNum);

            string from, to, pass, email;

            from = "tuanden090304@gmail.com";
            pass = "ltux uibp hauy rcrb";
            to = gmail;

            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.From = new MailAddress(from);
            mm.Subject = "Mã OTP";
            mm.Body = toStringNum;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(mm);
                string alert = "Đã gửi OTP qua gmail!!";
                //ViewData["Otp"] = alert.ToString();
                return RedirectToAction("ForgetPassword", new { message = "Đã gửi OTP thành công!" });
            }
            catch (Exception ex)
            {
                string alert = "Gui otp that bai!!";
                //ViewData["Otp"] = alert.ToString();
                return RedirectToAction("ForgetPassword", new { message = "Đã gửi OTP thất bại!" });

            }

        }

        [HttpPost]
        public async Task<IActionResult> SendPasswordToGmail(string gmail, string otp)
        {
            string OtpSession = HttpContext.Session.GetString("otp");

            if (otp == OtpSession)
            {
                NguoiDung userInfo;
                List<NguoiDung> listUsers = new List<NguoiDung>();
                var response = await _httpClient.GetAsync("https://localhost:7287/api/User/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    listUsers = JsonConvert.DeserializeObject<List<NguoiDung>>(apiResponse);
                }
                userInfo = listUsers.Find(p => p.Email == gmail);



                string from, to, pass, email;

                from = "tuanden090304@gmail.com";
                pass = "ltux uibp hauy rcrb";
                to = gmail;

                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.From = new MailAddress(from);
                mm.Subject = "Mật khẩu đã quên của bạn!!";
                mm.Body = userInfo.mat_khau.ToString();

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);

                try
                {
                    smtp.Send(mm);
                    string alert = "Đã gửi mật khẩu qua gmail của bạn!!";
                    //ViewData["Otp"] = alert.ToString();
                    return RedirectToAction("Login", new { message = alert });
                }
                catch (Exception ex)
                {
                    string alert = "Gửi mật khẩu qua gmail thất bại!!";
                    //ViewData["Otp"] = alert.ToString();
                    return RedirectToAction("ForgetPassword", new { message = alert });

                }
            }
            else
            {
                string alert = "Bạn đã nhập sai OTP";
                return RedirectToAction("ForgetPassword", new { message = alert });
            }

        }
        // GET: Login
        // GET: Login
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        // POST: Login
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(NguoiDung user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7287/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<NguoiDung>(responseBody);

                if (userInfo != null)
                {
                    if (userInfo.TrangThai == "Đang hoạt động")
                    {
                        HttpContext.Session.SetString("UserEmail", userInfo.Email);
                        HttpContext.Session.SetString("UserRole", userInfo.Roles);

                        switch (userInfo.Roles)
                        {
                            case "Người xin việc":
                                await ThemVaoTrangThongTinNguoiTimViec(userInfo.nguoi_dung_id);
                                return RedirectToAction("Index", "Home");

                            case "ADMIN":
                                return RedirectToAction("Index", "Admin");

                            case "Doanh Nghiệp":
                                await ThemVaoTrangThongTinDoanhNghiep(userInfo.nguoi_dung_id);
                                return RedirectToAction("Index", "DoanhNghieps", new { id = thongtindoanhnghiep.DoanhNghiepId });

                            default:
                                ModelState.AddModelError(string.Empty, "Vai trò người dùng không hợp lệ.");
                                return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Tài khoản của bạn không hoạt động.");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không tìm thấy người dùng.");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin.");
                return View(user);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> Index()
        {
            List<CongViec> job = new List<CongViec>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                job = JsonConvert.DeserializeObject<List<CongViec>>(apiResponse);
            }

            List<provinces> provinces = new List<provinces>();
            var provinceResponse = await _httpClient.GetAsync("https://localhost:7287/api/Places/GetAllprovinces");
            if (provinceResponse.IsSuccessStatusCode)
            {
                var provinceApiResponse = await provinceResponse.Content.ReadAsStringAsync();
                provinces = JsonConvert.DeserializeObject<List<provinces>>(provinceApiResponse);
            }

            List<districts> districts = new List<districts>();
            var districtResponse = await _httpClient.GetAsync("https://localhost:7287/api/Places/GetAlldictricts");
            if (districtResponse.IsSuccessStatusCode)
            {
                var districtApiResponse = await districtResponse.Content.ReadAsStringAsync();
                districts = JsonConvert.DeserializeObject<List<districts>>(districtApiResponse);
            }

            List<wards> wards = new List<wards>();
            var wardResponse = await _httpClient.GetAsync("https://localhost:7287/api/Places/GetAllwards");
            if (wardResponse.IsSuccessStatusCode)
            {
                var wardApiResponse = await wardResponse.Content.ReadAsStringAsync();
                wards = JsonConvert.DeserializeObject<List<wards>>(wardApiResponse);
            }

            // Now map the related province, district, and ward names to the jobs
            foreach (var j in job)
            {
                // Map Province name based on ProvinceId
                j.ProvinceName = provinces.FirstOrDefault(p => p.code == j.ProvinceId)?.full_name ?? "Unknown Province";

                // Map District name based on DistrictId
                j.DistrictName = districts.FirstOrDefault(d => d.code == j.DistrictId)?.full_name ?? "Unknown District";

                // Map Ward name based on WardId
                j.WardName = wards.FirstOrDefault(w => w.code == j.WardId)?.full_name ?? "Unknown Ward";
            }
            ViewData["job"] = job;
            return View(job); // Pass jobs with province, district, and ward names to the view
        }
        public async Task<ActionResult> Details(int id)
        {
          
            CongViec reservation = new CongViec();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/CongViec/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiresponse = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<CongViec>(apiresponse);
            }
          

            // Lấy các dữ liệu liên quan khác như trước
            List<KyNangCongViec> kynangcongviec = new List<KyNangCongViec>();
            var responsekynang = await _httpClient.GetAsync($"https://localhost:7287/api/SkillCongViec/GetAll");
            if (responsekynang.IsSuccessStatusCode)
            {
                var apiresponse = await responsekynang.Content.ReadAsStringAsync();
                kynangcongviec = JsonConvert.DeserializeObject<List<KyNangCongViec>>(apiresponse);
            }
            // Lấy các dữ liệu liên quan khác như trước
            List<KinhNghiemCongViec> kinhnghiemcongviec = new List<KinhNghiemCongViec>();
            var responsekinhnghiem = await _httpClient.GetAsync($"https://localhost:7287/api/KinhNghiemCongViec/GetAll");
            if (responsekynang.IsSuccessStatusCode)
            {
                var apiresponse = await responsekynang.Content.ReadAsStringAsync();
                kynangcongviec = JsonConvert.DeserializeObject<List<KyNangCongViec>>(apiresponse);
            }


            List<provinces> provinces = new List<provinces>();
            var provinceResponse = await _httpClient.GetAsync("https://localhost:7287/api/Places/GetAllprovinces");
            if (provinceResponse.IsSuccessStatusCode)
            {
                var provinceApiResponse = await provinceResponse.Content.ReadAsStringAsync();
                provinces = JsonConvert.DeserializeObject<List<provinces>>(provinceApiResponse);
            }

            List<districts> districts = new List<districts>();
            var districtResponse = await _httpClient.GetAsync("https://localhost:7287/api/Places/GetAlldictricts");
            if (districtResponse.IsSuccessStatusCode)
            {
                var districtApiResponse = await districtResponse.Content.ReadAsStringAsync();
                districts = JsonConvert.DeserializeObject<List<districts>>(districtApiResponse);
            }

            List<wards> wards = new List<wards>();
            var wardResponse = await _httpClient.GetAsync("https://localhost:7287/api/Places/GetAllwards");
            if (wardResponse.IsSuccessStatusCode)
            {
                var wardApiResponse = await wardResponse.Content.ReadAsStringAsync();
                wards = JsonConvert.DeserializeObject<List<wards>>(wardApiResponse);
            }

            // Bây giờ gán tên các tỉnh, huyện, phường vào công việc
            reservation.ProvinceName = provinces.FirstOrDefault(p => p.code == reservation.ProvinceId)?.full_name ?? "Unknown Province";
            reservation.DistrictName = districts.FirstOrDefault(d => d.code == reservation.DistrictId)?.full_name ?? "Unknown District";
            reservation.WardName = wards.FirstOrDefault(w => w.code == reservation.WardId)?.full_name ?? "Unknown Ward";

            // Lấy thông tin doanh nghiệp
            DoanhNghiep doanhnghiep = await CallDoanhNghiep(reservation.DoanhNghiepId);

            // Lọc các kỹ năng phù hợp
            List<KyNangCongViec> matchingKynang = kynangcongviec.Where(k => k.CongViecId == reservation.CongViecId).ToList();
            List<KyNangCongViec> kynang = new List<KyNangCongViec>();
            foreach (var item in matchingKynang)
            {
                var fetchedKynang = await CallkynangCongViec(item.KyNangId);
                if (fetchedKynang != null)
                {
                    kynang.Add(fetchedKynang);
                }
            }

            // Lọc các kinh nghiệm phù hợp
            List<KinhNghiemCongViec> matchingKinhNghiem = kinhnghiemcongviec.Where(k => k.CongViecId == reservation.CongViecId).ToList();
            List<KinhNghiemCongViec> kinhNghiem = new List<KinhNghiemCongViec>();
            foreach (var item in matchingKinhNghiem)
            {
                var fetchedKinhnghiem = await CallkinhnghiemCongViec(item.KinhNghiemId);
                if (fetchedKinhnghiem != null)
                {
                    kinhNghiem.Add(fetchedKinhnghiem);
                }
            }

            // Đặt ViewData cho việc render view
            ViewData["kinhnghiem"] = kinhNghiem;
            ViewData["kynang"] = kynang;
            ViewData["doanhnghiep"] = doanhnghiep;
            ViewData["reservation"] = reservation;
          
            return View(reservation);
        }
        public async Task<ActionResult> beematching()
        {
            return View("beematching");
        }
        public async Task<ActionResult> beenews()
        {
            return View("beenews");
        }
        public async Task<ActionResult> Lienhe()
        {
            return View("Lienhe");
        }
        public async Task<KyNangCongViec> CallkynangCongViec(int id)
        {
            KyNangCongViec kynang = new KyNangCongViec();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/SkillCongViec/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiresponse = await response.Content.ReadAsStringAsync();
                kynang = JsonConvert.DeserializeObject<KyNangCongViec>(apiresponse);
            }
            return kynang;
        }
        public async Task<KinhNghiemCongViec> CallkinhnghiemCongViec(int id)
        {
            KinhNghiemCongViec kinhNghiem = new KinhNghiemCongViec();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/KinhNghiemCongViec/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiresponse = await response.Content.ReadAsStringAsync();
                kinhNghiem = JsonConvert.DeserializeObject<KinhNghiemCongViec>(apiresponse);
            }
            return kinhNghiem;
        }
        public async Task<DoanhNghiep> CallDoanhNghiep(int id)
        {
            DoanhNghiep doanhnghiep = new DoanhNghiep();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/Company/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiresponse = await response.Content.ReadAsStringAsync();
                doanhnghiep = JsonConvert.DeserializeObject<DoanhNghiep>(apiresponse);
            }
            return doanhnghiep;
        }

        [HttpGet]
        public async Task<ActionResult> TimKiemTheoTen(string search)
        {
            List<CongViec> job = new List<CongViec>();
            List<CongViec> hassearch = new List<CongViec>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                job = JsonConvert.DeserializeObject<List<CongViec>>(apiResponse);
            }
            foreach (CongViec congviec in job)
            {
                if (congviec.TieuDe.ToLower().Contains(search.ToLower()))
                {
                    hassearch.Add(congviec);
                }
            }
            ViewData["job"] = hassearch;
            return View("CongViec");

        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NguoiDung user, IFormFile hinh_anh)
        {
            /*// Kiểm tra nếu hinh_anh không phải null
              if (hinh_anh == null || hinh_anh.Length == 0)
              {
                  ModelState.AddModelError("hinh_anh", "Vui lòng chọn hình ảnh.");
                  return View(user);
              }

              // Kiểm tra phần mở rộng của tệp
              string fileExtension = Path.GetExtension(hinh_anh.FileName).ToLower();
              string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

              if (!allowedExtensions.Contains(fileExtension))
              {
                  ModelState.AddModelError(string.Empty, "Chỉ chấp nhận tệp hình ảnh (.jpg, .jpeg, .png, .gif).");
                  return View(user);
              }

              // Kiểm tra dung lượng tệp (giới hạn 5MB)
              if (hinh_anh.Length > 5 * 1024 * 1024) // 5MB
              {
                  ModelState.AddModelError(string.Empty, "Dung lượng tệp không được vượt quá 5MB.");
                  return View(user);
              }

              // Tạo tên tệp (ví dụ: nv123.jpg)
              string fileName = "nv" + user.nguoi_dung_id.ToString() + fileExtension;

              // Đường dẫn đến thư mục lưu tệp
              string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload", "images", fileName);

              // Kiểm tra và tạo thư mục nếu chưa tồn tại
              var directory = Path.GetDirectoryName(filePath);
              if (!Directory.Exists(directory))
              {
                  Directory.CreateDirectory(directory);
              }

              // Lưu tệp vào thư mục
              using (var stream = new FileStream(filePath, FileMode.Create))
              {
                  await hinh_anh.CopyToAsync(stream);
              }

              // Cập nhật tên tệp vào trường hinh_anh trong model
              user.hinh_anh = fileName;

              // Tiến hành lưu dữ liệu vào cơ sở dữ liệu, hoặc thực hiện các bước tiếp theo
              // Bạn có thể gọi API hoặc lưu thông tin vào cơ sở dữ liệu ở đây

              // Example API Call (Có thể sửa theo nhu cầu của bạn)*/
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7287/api/User/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));  // Chuyển hướng khi thành công
            }

            // Nếu có lỗi với API
            var responseContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Error occurred while creating user: {responseContent}");
            return View(user);  // Trả về view với thông báo lỗi
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
