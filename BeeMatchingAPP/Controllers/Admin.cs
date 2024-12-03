using API_He_thong.DATA;
using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace BeeMatchingAPP.Controllers
{
    public class Admin : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<Admin> _logger;
        HttpClient _httpClient;
        public Admin(ILogger<Admin> logger, HttpClient httpClient, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _httpClient = httpClient;
            _hostingEnvironment = hostingEnvironment;
        }
        //demo1

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> NguoiDung()
        {

            List<NguoiDung> nguoiDungs = new List<NguoiDung>();
            var userResponse = await _httpClient.GetAsync("https://localhost:7287/api/User/GetAll");
            if (userResponse.IsSuccessStatusCode)
            {
                var apiUserResponse = await userResponse.Content.ReadAsStringAsync();
                nguoiDungs = JsonConvert.DeserializeObject<List<NguoiDung>>(apiUserResponse);
            }
            return View(nguoiDungs);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleteResponse = await _httpClient.DeleteAsync($"https://localhost:7287/api/User/Delete/{id}");
            if (deleteResponse.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Người dùng đã được xóa thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa người dùng thất bại. Vui lòng thử lại.";
            }

            return RedirectToAction("NguoiDung");
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
        public async Task<ActionResult> DeleteDoanhNghiep(int id)
        {
            try
            {
                DoanhNghiep doanhNghiep = null;
                var response = await _httpClient.GetAsync($"https://localhost:7287/api/Company/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    doanhNghiep = JsonConvert.DeserializeObject<DoanhNghiep>(apiResponse);
                    return View(doanhNghiep); // Hiển thị View để xác nhận xóa
                }
                else
                {
                    ModelState.AddModelError("", "Không thể tải thông tin doanh nghiệp. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
                return RedirectToAction("Error", "Home"); // Điều hướng đến trang lỗi
            }

            return View(); // Trường hợp không thành công
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDoanhNghiep(int id, DoanhNghiep job)
        {
            if (!ModelState.IsValid) // Kiểm tra dữ liệu hợp lệ
            {
                return View(job);

            }

            if (id != job.DoanhNghiepId)
            {
                ModelState.AddModelError("", "ID không khớp.");
                return View(job);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(job), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7287/api/Company/Edit/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Xóa Doanh Nghiep thành công!";
                    return RedirectToAction("DoanhNghiep", "Admin");
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
        public async Task<ActionResult> EditDoanhNghiep(int id)
        {
            try
            {
                DoanhNghiep reservation = null;
                var response = await _httpClient.GetAsync($"https://localhost:7287/api/Company/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<DoanhNghiep>(apiResponse);
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
        public async Task<ActionResult> EditDoanhNghiep(int id, DoanhNghiep job)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage);

                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation error: {error}"); // Log lỗi để kiểm tra
                }

                ModelState.AddModelError("", "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các trường nhập.");
                return View(job);
            }

            if (id != job.DoanhNghiepId)
            {
                ModelState.AddModelError("", "ID không khớp.");
                return View(job);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(job), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7287/api/Company/Edit/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cập nhật doanh nghiep thành công!";
                    return RedirectToAction("DoanhNghiep", "Admin");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Không thể cập nhật doanh nghiep: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã xảy ra lỗi: {ex.Message}");
            }

            return View(job);
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
    }
}
