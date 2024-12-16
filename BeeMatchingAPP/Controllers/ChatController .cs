/*using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeeMatchingAPP.Controllers
{
    public class ChatController : Controller
    {
        private const string ENDPOINT = "https://ai-minhmvpd094444263ai868887431975.cognitiveservices.azure.com/\r\n\r\n";
        private const string API_KEY = "CT8UiTVAzhUJ1CrPg3xB7ut5RcGQLHwJKt85nGmTQFcnG2OwR6DQJQQJ99ALACHYHv6XJ3w3AAAAACOGgYDL"; // Thay bằng API Key của bạn

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return View("Index", new { Message = "Please enter a question." });
            }

            try
            {
                var responseMessage = await GetChatResponse(question);
                ViewData["ms"]=responseMessage;         
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Index", new { Message = $"Error: {ex.Message}" });
            }
        }

        private async Task<string> GetChatResponse(string question)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

                var payload = new
                {
                    messages = new object[]
                    {
                        new { role = "system", content = "You are an AI assistant." },
                        new { role = "user", content = question }
                    },
                    temperature = 0.7,
                    top_p = 0.95,
                    max_tokens = 800
                };

                var response = await httpClient.PostAsync(
                    ENDPOINT,
                    new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    return responseData.choices[0].message.content.ToString();
                }
                else
                {
                    // Log chi tiết thông tin phản hồi khi có lỗi
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}, {response.ReasonPhrase}. Response: {errorContent}";
                }
            }
        }
    }
}*/