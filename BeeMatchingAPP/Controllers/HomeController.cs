using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;

namespace BeeMatchingAPP.Controllers
{
    //tuan day
    //tuan cong
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
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

        // POST: MvcReservationcontroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NguoiDung user)
        {
            if (!ModelState.IsValid)
            {
                // Ghi log các lỗi ModelState để gỡ lỗi
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage);
                }

                return View(user);
            }
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"https://localhost:7287/api/User/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Ghi log lỗi nếu gọi API thất bại
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Error occurred while creating combo: {responseContent}");
            ModelState.AddModelError(string.Empty, $"Error occurred while creating combo: {responseContent}");
            return View(user);
        }

        public async Task<ActionResult> Company()
        {

            List<DoanhNghiep> users = new List<DoanhNghiep>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/Company/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<DoanhNghiep>>(apiResponse);
            }

            return View(users);
        }
        public async Task<ActionResult> CongViec()
        {

            List<CongViec> users = new List<CongViec>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<CongViec>>(apiResponse);
            }

            return View(users);
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
