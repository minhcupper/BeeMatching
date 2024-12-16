using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using API_He_thong.Controllers;
using helper;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;


namespace BeeMatchingAPP.Controllers
{
  
    public class CongviecController : Controller
    {
        private readonly ILogger<CongviecController> _logger;
        HttpClient _httpClient;
        public CongviecController(ILogger<CongviecController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        [HttpGet]
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
                j.DistrictName = districts.FirstOrDefault(d => d.code == j.DistrictId)?.name ?? "Unknown District";

                // Map Ward name based on WardId
                j.WardName = wards.FirstOrDefault(w => w.code == j.WardId)?.name ?? "Unknown Ward";
            }
            ViewData["job"] = job;
            return View(); // Pass jobs with province, district, and ward names to the view
        }
        public ActionResult Logout()
        {
            
            HttpContext.Session.Clear(); 

            
            return RedirectToAction("Index", "Home");
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

        private string RemoveDiacritics(string text)
        {
            // Chuẩn hóa chuỗi đầu vào để loại bỏ dấu
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            // Duyệt qua từng ký tự trong chuỗi đã chuẩn hóa
            foreach (var c in normalizedString)
            {
                // Lấy loại ký tự của ký tự hiện tại
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                // Nếu ký tự không phải là dấu, thêm vào chuỗi kết quả
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Trả về chuỗi đã loại bỏ dấu, chuẩn hóa lại
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
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

            // Loại bỏ dấu từ chuỗi tìm kiếm
            string searchWithoutDiacritics = RemoveDiacritics(search).ToLower();

            foreach (CongViec congviec in job)
            {
                // Loại bỏ dấu từ tiêu đề công việc
                string titleWithoutDiacritics = RemoveDiacritics(congviec.TieuDe).ToLower();

                if (titleWithoutDiacritics.Contains(searchWithoutDiacritics))
                {
                    hassearch.Add(congviec);
                }
            }
            ViewBag.JobCount = $"{hassearch.Count} công việc";

            ViewData["job"] = hassearch;
            return View("CongViec");
        }

        [HttpPost]
        public async Task<ActionResult> CheckBoxDangCongViec(string dangCongViec)
        {
            List<CongViec> jobList = new List<CongViec>();
            List<CongViec> hassearch = new List<CongViec>();
            List<CongViec> jobListHasChecked = new List<CongViec>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                jobList = JsonConvert.DeserializeObject<List<CongViec>>(apiResponse);
            }
            foreach (var item in jobList)
            {
                if (item.DangCongViec.ToLower() == dangCongViec.ToLower())
                {
                    jobListHasChecked.Add(item);
                    hassearch.Add(item);
                }
            }
            ViewBag.JobCount = $"{hassearch.Count} công việc";
            ViewData["job"] = jobListHasChecked;
            return View("CongViec");
        }
    }
}