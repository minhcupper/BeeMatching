using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;

namespace BeeMatchingAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

      public async Task<ActionResult> Index()
        {

            List<User> users = new List<User>();
           var response = await _httpClient.GetAsync("https://localhost:7287/api/User/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
            }

            return View(users);
        }
        public async Task<ActionResult> Detailsuser(int id)
        {

            User reservation = new User();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiresponse = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<User>(apiresponse);
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
        public async Task<ActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                // N?u d? li?u ??u vào không h?p l?, tr? l?i View v?i các thông tin l?i
                return View(user);
            }

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7170/api/combo", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // X? lý l?i t? ph?n h?i API
            var responseContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Error occurred while creating combo: {responseContent}");

            // Tr? l?i View v?i thông tin l?i
            return View(user);
        }

        public async Task<ActionResult> Company()
        {

            List<Company> users = new List<Company>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/Company/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<Company>>(apiResponse);
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
