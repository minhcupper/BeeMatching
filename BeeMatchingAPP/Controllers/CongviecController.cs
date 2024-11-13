using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace BeeMatchingAPP.Controllers
{
    public class CongviecController : Controller
    {
        private readonly ILogger<DoanhNghiepsController> _logger;
        HttpClient _httpClient;
        public CongviecController(ILogger<DoanhNghiepsController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
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
                j.DistrictName = districts.FirstOrDefault(d => d.code == j.DistrictId)?.name ?? "Unknown District";

                // Map Ward name based on WardId
                j.WardName = wards.FirstOrDefault(w => w.code == j.WardId)?.name ?? "Unknown Ward";
            }

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
             // Map Province name based on ProvinceId
                reservation.ProvinceName = provinces.FirstOrDefault(p => p.code == reservation.ProvinceId)?.full_name ?? "Unknown Province";

                // Map District name based on DistrictId
                reservation.DistrictName = districts.FirstOrDefault(d => d.code == reservation.DistrictId)?.name ?? "Unknown District";

                // Map Ward name based on WardId
                reservation.WardName = wards.FirstOrDefault(w => w.code == reservation.WardId)?.name ?? "Unknown Ward";

            List<KyNangCongViec> kyNangCongViec = new List<KyNangCongViec>();
            var kyNang = await _httpClient.GetAsync("https://localhost:7287/api/SkillCongviec/GetAll");
            if (kyNang.IsSuccessStatusCode)
            {
                var kyNangCongViecrespon = await wardResponse.Content.ReadAsStringAsync();
                kyNangCongViec = JsonConvert.DeserializeObject<List<KyNangCongViec>>(kyNangCongViecrespon);
            }
            // Map Province name based on 
            reservation.MoTaKyNang = kyNangCongViec.FirstOrDefault(p => p.CongViecId == reservation.CongViecId)?.MoTa ?? "Unknown Province";

            // Map District name based on 
            reservation.TenKyNang = kyNangCongViec.FirstOrDefault(p => p.CongViecId == reservation.CongViecId)?.TenKyNang ?? "Unknown District";

        

            ViewData["reservation"] = reservation;
            return View();
        }

    }
}
