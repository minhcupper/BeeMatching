using BeeMatchingAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Text;


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

            List<CongViec> users = new List<CongViec>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/CongViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<CongViec>>(apiResponse);
            }

            return View(users);
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
            return View(reservation);
        }

    }
}
