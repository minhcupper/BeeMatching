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



        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
            // Kiểm tra id hợp lệ
            if (id <= 0)
            {
                return RedirectToAction("Error", "Home", new { message = "ID không hợp lệ." });
            }

            List<CongViec> listcongviec = new List<CongViec>();

            try
            {
                // Gọi API để lấy danh sách công việc
                var congviec = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
                if (!congviec.IsSuccessStatusCode)
                {
                    _logger.LogError("Lỗi khi gọi API CongViec: {StatusCode}", congviec.StatusCode);
                    return View(new List<CongViec>());
                }

                var congviecs = await congviec.Content.ReadAsStringAsync();
                var alllistcongviec = JsonConvert.DeserializeObject<List<CongViec>>(congviecs) ?? new List<CongViec>();

                // Lọc danh sách các công việc theo DoanhNghiepId
                listcongviec = alllistcongviec.Where(p => p.DoanhNghiepId == id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xảy ra khi lấy danh sách công việc.");
                return View(new List<CongViec>()); // Trả về view trống nếu xảy ra lỗi
            }

            ViewData["congviec"]=listcongviec;
            return View(listcongviec);
        }

        [HttpGet]
        public async Task<ActionResult> UngTuyen(int id)
        {
            // Kiểm tra id hợp lệ
            if (id <= 0)
            {
                return RedirectToAction("Error", "Home", new { message = "ID không hợp lệ." });
            }

            List<UngTuyen> listungtuyen = new List<UngTuyen>();

            try
            {
                // Gọi API để lấy danh sách công việc
                var ungtuyen = await _httpClient.GetAsync("https://localhost:7287/api/UngTuyen/GetAll");
                if (!ungtuyen.IsSuccessStatusCode)
                {
                    _logger.LogError("Lỗi khi gọi API CongViec: {StatusCode}", ungtuyen.StatusCode);
                    return View(new List<UngTuyen>());
                }

                var ungtuyens = await ungtuyen.Content.ReadAsStringAsync();
                var alllistcongviec = JsonConvert.DeserializeObject<List<UngTuyen>>(ungtuyens) ?? new List<UngTuyen>();

                // Lọc danh sách các công việc theo DoanhNghiepId
                listungtuyen = alllistcongviec.Where(p => p.CongViecId == id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xảy ra khi lấy danh sách công việc.");
                return View(new List<CongViec>()); // Trả về view trống nếu xảy ra lỗi
            }

            ViewData["ungtuyen"] = listungtuyen;
            return View(listungtuyen);
        }
    }
}
