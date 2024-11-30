using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;
using API_He_thong.Controllers;


namespace BeeMatchingAPP.Controllers
{
    public class DoanhNghiepsController : Controller
    {
        private readonly ILogger<DoanhNghiepsController> _logger;
        HttpClient _httpClient;
        public DoanhNghiepsController(ILogger<DoanhNghiepsController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        public async Task<ActionResult> DoanhNghiep()
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
        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {

            DoanhNghiep users = new DoanhNghiep();
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/Company/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<DoanhNghiep>(apiResponse);
            }

            List<CongViec> listcongviec = new List<CongViec>();
            var congviec = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
            if (congviec.IsSuccessStatusCode)
            {
                var congviecs = await congviec.Content.ReadAsStringAsync(); // Sử dụng congviec.Content thay vì response.Content
                var alllistcongviec = JsonConvert.DeserializeObject<List<CongViec>>(congviecs);

                // Lọc danh sách các công việc có DoanhNghiepId trùng với id
                listcongviec = alllistcongviec.Where(p => p.DoanhNghiepId == users.DoanhNghiepId).ToList();
            }
            /*
            UngTuyen ListUngTuyen= new UngTuyen();
            var responseungtuyen = await _httpClient.GetAsync($"https://localhost:7287/api/UngTuyen/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + apiResponse);  // Log để kiểm tra API trả về gì
                var allUngTuyen = JsonConvert.DeserializeObject<List<UngTuyen>>(apiResponse);

                // Tìm kiếm người dùng theo ID
                ListUngTuyen = allUngTuyen.Find(p => p.CongViecId == id);
            }
          */
            return View(listcongviec);
        }
    }
}
