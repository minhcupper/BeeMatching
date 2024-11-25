﻿using BeeMatchingAPP.Models;
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
        private NguoiTimViec TrangThongTinCaNhan
        {
            get
            {
                // Truy xuất danh sách người tìm việc từ Session
                return HttpContext.Session.Get<NguoiTimViec>("ThongTin") ?? new NguoiTimViec();
            }
        }

        public async Task<ActionResult> TrangThongTinNguoiTimViec()
        {
            // Lấy dữ liệu từ Session
            var data = TrangThongTinCaNhan;
            ViewData["HoSo"] = data;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CapNhatThongTin(NguoiTimViec nguoiTimViec)
        {
            if (ModelState.IsValid)
            {
                var user = new NguoiTimViec
                {
                    ho_ten = nguoiTimViec.ho_ten,
                    so_dien_thoai = nguoiTimViec.so_dien_thoai,
                    email = nguoiTimViec.email,
                    ngay_sinh = nguoiTimViec.ngay_sinh,
                    MoTa = nguoiTimViec.MoTa,
                    WardId = nguoiTimViec.WardId,
                    DistrictId = nguoiTimViec.DistrictId,
                    ProvinceId = nguoiTimViec.ProvinceId,
                    NgonNgu = nguoiTimViec.NgonNgu,
                    HinhAnh = nguoiTimViec.HinhAnh,
                    KinhNghiem = nguoiTimViec.KinhNghiem,
                    HoatDongNgoaiKhoa = nguoiTimViec.HoatDongNgoaiKhoa,
                    KyNangNguoiXinViecs = nguoiTimViec.KyNangNguoiXinViecs

                };
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7287/api/NguoiTimViec/Edit/{nguoiTimViec.NguoiTimViecId}", user);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("TrangThongTinNguoiTimViec");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cập nhật thông tin không thành công");
                }
            }
            return View(nguoiTimViec);
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
                throw new Exception($"Tài khoản với ID {id} không tồn tại hoặc chưa nhập thông tin.");
            }

            // Truy xuất thông tin người dùng từ Session
            var data = TrangThongTinCaNhan;

            // Tạo đối tượng mới từ thông tin người dùng
            var newItem = new NguoiTimViec
            {
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

            // Lưu thông tin người dùng vào Session
            HttpContext.Session.Set("ThongTin", newItem);
        }
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
            // Kiểm tra nếu người dùng chưa nhập thông tin
            if (user == null || string.IsNullOrEmpty(user.ten_dang_nhap) || string.IsNullOrEmpty(user.mat_khau))
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập và mật khẩu không được để trống.");
                return View(user);
            }

            // Gửi yêu cầu xác thực người dùng
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7287/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                // Lấy thông tin người dùng từ phản hồi API
                var responseBody = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<NguoiDung>(responseBody);

                if (userInfo != null)
                {
                    // Gọi phương thức ThemVaoTrangThongTinNguoiTimViec với ID người dùng
                    try
                    {
                        await ThemVaoTrangThongTinNguoiTimViec(userInfo.nguoi_dung_id);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(user);
                    }

                    // Chuyển hướng dựa trên vai trò của người dùng
                    if (userInfo.Roles == "ADMIN")
                    {
                        return RedirectToAction("Index", "ADMINController1");
                    }
                    else if (userInfo.Roles == "Người xin việc")
                    {
                        return RedirectToAction("CongViec", "CongViec");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Vai trò người dùng không hợp lệ.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể lấy thông tin người dùng.");
                }
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Lỗi đăng nhập: {errorDetails}");
            }

            return View(user);
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
            List<KyNangCongViec> kynangcongviec = new List<KyNangCongViec>();
            var responsekynang = await _httpClient.GetAsync($"https://localhost:7287/api/SkillCongViec/GetAll");
            if (responsekynang.IsSuccessStatusCode)
            {
                var apiresponse = await responsekynang.Content.ReadAsStringAsync();
                kynangcongviec = JsonConvert.DeserializeObject<List<KyNangCongViec>>(apiresponse);
            }
            List<KinhNghiemCongViec> kinhnghiemcongviec = new List<KinhNghiemCongViec>();
            var responsekinhnghiem = await _httpClient.GetAsync($"https://localhost:7287/api/KinhNghiemCongViec/GetAll");
            if (responsekinhnghiem.IsSuccessStatusCode)
            {
                var apiresponse = await responsekynang.Content.ReadAsStringAsync();
                kinhnghiemcongviec = JsonConvert.DeserializeObject<List<KinhNghiemCongViec>>(apiresponse);
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


            // Map Province name based on ProvinceId
            reservation.ProvinceName = provinces.FirstOrDefault(p => p.code == reservation.ProvinceId)?.full_name ?? "Unknown Province";

            // Map District name based on DistrictId
            reservation.DistrictName = districts.FirstOrDefault(d => d.code == reservation.DistrictId)?.full_name ?? "Unknown District";

            // Map Ward name based on WardId
            reservation.WardName = wards.FirstOrDefault(w => w.code == reservation.WardId)?.full_name ?? "Unknown Ward";


            DoanhNghiep doanhnghiep = new DoanhNghiep();
            doanhnghiep = await CallDoanhNghiep(reservation.DoanhNghiepId);
            List<KyNangCongViec> matchingKynang = null;
            if (reservation != null && kynangcongviec != null)
            {
                // Lọc kỹ năng có CongViecId trùng với reservation.CongViecId
                matchingKynang = kynangcongviec.Where(k => k.CongViecId == reservation.CongViecId).ToList();
            }

            List<KyNangCongViec> kynang = new List<KyNangCongViec>();
            if (matchingKynang != null && matchingKynang.Any())
            {
                foreach (var item in matchingKynang)
                {
                    var fetchedKynang = await CallkynangCongViec(item.KyNangId);
                    if (fetchedKynang != null)
                    {
                        kynang.Add(fetchedKynang);
                    }
                }
            }
            List<KinhNghiemCongViec> matchingKinhNghiem = null;
            if (reservation != null && kinhnghiemcongviec != null)
            {
                // Lọc kỹ năng có CongViecId trùng với reservation.CongViecId
                matchingKinhNghiem = kinhnghiemcongviec.Where(k => k.CongViecId == reservation.CongViecId).ToList();
            }
            List<KinhNghiemCongViec> kinhNghiem = new List<KinhNghiemCongViec>();
            if (matchingKinhNghiem != null && matchingKinhNghiem.Any())
            {
                foreach (var item in matchingKinhNghiem)
                {
                    var fetchedKinhnghiem = await CallkinhnghiemCongViec(item.KinhNghiemId);
                    if (fetchedKinhnghiem != null)
                    {
                        Console.WriteLine($"Fetched: MoTa={fetchedKinhnghiem.MoTa}, TenKinhNghiem={fetchedKinhnghiem.TenKinhNghiem}, SoNamKinhNghiem={fetchedKinhnghiem.SoNamKinhNghiem}");
                        kinhNghiem.Add(fetchedKinhnghiem);
                    }
                }
            }
            // Nếu tìm thấy kỹ năng phù hợp, gọi CallkynangCongViec với id của kỹ năng đó
            ViewData["kinhnghiem"] = kinhNghiem;
            ViewData["kynang"] = kynang;
            ViewData["doanhnghiep"] = doanhnghiep;
            ViewData["reservation"] = reservation;
            return View(reservation);
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
            /*  // Kiểm tra nếu hinh_anh không phải null
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
