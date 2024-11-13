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
    public class KyNangCongViecController : Controller
    {
        private readonly ILogger<KyNangCongViecController> _logger;
        HttpClient _httpClient;
        public KyNangCongViecController(ILogger<KyNangCongViecController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        public async Task<ActionResult> KyNangCongViec()
        {

            List<KyNangCongViec> users = new List<KyNangCongViec>();
            var response = await _httpClient.GetAsync("https://localhost:7287/api/KyNangCongViec/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<KyNangCongViec>>(apiResponse);
            }

            return View(users);
        }
    }
}
