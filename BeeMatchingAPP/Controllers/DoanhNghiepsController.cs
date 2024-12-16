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
                // Chuyển hướng đến trang lỗi nếu ID không hợp lệ
                return RedirectToAction("Error", "Home", new { message = "ID không hợp lệ." });
            }

            List<CongViec> listcongviec = new List<CongViec>();

            try
            {
                // Gọi API để lấy danh sách công việc
                var congviec = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");

                if (!congviec.IsSuccessStatusCode)
                {
                    // Ghi lại lỗi nếu API không trả về thành công
                    _logger.LogError("Lỗi khi gọi API CongViec: {StatusCode}", congviec.StatusCode);
                    ViewData["congviecMessage"] = "Có lỗi xảy ra khi lấy dữ liệu công việc.";
                    return View(new List<CongViec>());
                }

                var congviecs = await congviec.Content.ReadAsStringAsync();

                // Kiểm tra dữ liệu từ API
                if (string.IsNullOrEmpty(congviecs))
                {
                    _logger.LogWarning("API không trả về dữ liệu công việc.");
                    ViewData["congviecMessage"] = "Không có công việc cho công ty này.";
                    return View(new List<CongViec>());
                }

                // Deserialize dữ liệu API thành danh sách công việc
                var alllistcongviec = JsonConvert.DeserializeObject<List<CongViec>>(congviecs) ?? new List<CongViec>();

                // Lọc danh sách công việc theo DoanhNghiepId
                listcongviec = alllistcongviec.Where(p => p.DoanhNghiepId == id).ToList();

                // Nếu không có công việc cho doanh nghiệp này
                if (listcongviec == null || !listcongviec.Any())
                {
                    ViewData["congviecMessage"] = "Không có công việc cho công ty này.";
                    return RedirectToAction("Error", "Home", new { message = "Không có công việc cho công ty này." });
                }
                else
                {
                    ViewData["congviecMessage"] = null; // Clear thông báo nếu có công việc
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi có sự cố trong quá trình gọi API hoặc giải mã dữ liệu
                _logger.LogError(ex, "Lỗi xảy ra khi lấy danh sách công việc.");
                ViewData["congviecMessage"] = "Đã xảy ra lỗi khi lấy danh sách công việc.";
                return View(new List<CongViec>());
            }

            // Trả về view với danh sách công việc
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
        public async Task<ActionResult> DoanhNghiep()
        {

            List<DoanhNghiep> users = new List<DoanhNghiep>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/Company/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<DoanhNghiep>>(apiResponse);
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
            foreach (var j in users)
            {
                // Map Province name based on ProvinceId
                j.ProvinceName = provinces.FirstOrDefault(p => p.code == j.ProvinceId)?.full_name ?? "Unknown Province";

                // Map District name based on DistrictId
                j.DistrictName = districts.FirstOrDefault(d => d.code == j.DistrictId)?.name ?? "Unknown District";

                // Map Ward name based on WardId
                j.WardName = wards.FirstOrDefault(w => w.code == j.WardId)?.name ?? "Unknown Ward";
            }
            return View(users);
        }
        public async Task<ActionResult> CongViec()
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
        [HttpGet]
        public async Task<ActionResult> DeleteCongViec(int id)
        {
            try
            {
                CongViec reservation = null;
                var response = await _httpClient.GetAsync($"https://localhost:7287/api/CongViec/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<CongViec>(apiResponse);
                }
                else
                {
                    ModelState.AddModelError("", "Không thể tải thông tin công việc. Vui lòng thử lại.");
                }
                return View(reservation);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
                return RedirectToAction("Error", "Home"); // Điều hướng đến trang lỗi chung
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCongViec(int id, CongViec job)
        {
            if (!ModelState.IsValid) // Kiểm tra dữ liệu hợp lệ
            {
                return View(job);
            }

            if (id != job.CongViecId)
            {
                ModelState.AddModelError("", "ID không khớp.");
                return View(job);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(job), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7287/api/CongViec/Edit/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Xóa công việc thành công!";
                    return RedirectToAction("CongViec", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật công việc. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
            }

            return View(job);
        }
        [HttpGet]
        public async Task<ActionResult> EditCongViec(int id)
        {
            try
            {
                CongViec reservation = null;
                var response = await _httpClient.GetAsync($"https://localhost:7287/api/CongViec/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<CongViec>(apiResponse);
                }
                else
                {
                    ModelState.AddModelError("", "Không thể tải thông tin công việc. Vui lòng thử lại.");
                }
                return View(reservation);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
                return RedirectToAction("Error", "Home"); // Điều hướng đến trang lỗi chung
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCongViec(int id, CongViec job)
        {
            if (!ModelState.IsValid) // Kiểm tra dữ liệu hợp lệ
            {
                return View(job);
            }

            if (id != job.CongViecId)
            {
                ModelState.AddModelError("", "ID không khớp.");
                return View(job);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(job), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7287/api/CongViec/Edit/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cập nhật công việc thành công!";
                    return RedirectToAction("CongViec", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật công việc. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
            }

            return View(job);
        }
    }
}
