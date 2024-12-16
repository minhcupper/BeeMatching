using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }

        [HttpPost("UseChatGpt")]
        public async Task<IActionResult> UseChatGpt([FromBody] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { error = "Query cannot be null or empty." });
            }

            // Lấy API Key từ biến môi trường
            var apiKey = Environment.GetEnvironmentVariable("CHATGPT_SESSION_TOKEN");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogError("API Key is missing. Ensure CHATGPT_SESSION_TOKEN is set in the environment.");
                return StatusCode(500, new { error = "API Key is not configured." });
            }

            try
            {
                // Tạo payload gửi đến OpenAI API
                var payload = new
                {
                    model = "gpt-3.5-turbo", // Sử dụng mô hình mới
                    messages = new[]
                    {
                        new { role = "user", content = query } // Định dạng tin nhắn theo GPT
                    },
                    max_tokens = 50
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, ApiUrl)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };

                // Thêm API Key vào header
                httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");

                // Gửi yêu cầu đến OpenAI
                var response = await _httpClient.SendAsync(httpRequest);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    return Ok(new { response = result.choices[0].message.content });
                }
                else
                {
                    _logger.LogError("Error from OpenAI API: {0}", jsonResponse);
                    return StatusCode((int)response.StatusCode, new { error = jsonResponse });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while using OpenAI API.");
                return StatusCode(500, new { error = "An error occurred while processing the request." });
            }
        }
    }
}