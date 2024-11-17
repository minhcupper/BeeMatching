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
namespace BeeMatchingAPP.Controllers
{
    //tuan day
    //tuan cong
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient , IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _httpClient = httpClient;
            _hostingEnvironment = hostingEnvironment;   
        }
        //demo1
        public IActionResult Login()
        {
            return View("Login");
        }
      public async Task<ActionResult> Index()
        {

            List<NguoiDung> users = new List<NguoiDung>();
           var response = await _httpClient.GetAsync("https://localhost:7287/api/User/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<NguoiDung>>(apiResponse);
            }

            return View(users);
        }

        public async Task<ActionResult> Details(int id)
        {

            NguoiDung reservation = new NguoiDung();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/User/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiresponse = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<NguoiDung>(apiresponse);
            }
            return View(reservation);
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
