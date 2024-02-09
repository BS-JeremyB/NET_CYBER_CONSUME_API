using Microsoft.AspNetCore.Mvc;
using NET_CYBER_CONSUME_API.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace NET_CYBER_CONSUME_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://v2.jokeapi.dev/joke/");
        }

        public IActionResult Index()
        {
            //string kindOfJoke = "Programming";
            //HttpResponseMessage response = _httpClient.GetAsync($"{kindOfJoke}?blacklistFlags=nsfw,religious,racist,sexist,explicit&type=single").Result;
            //if(response.IsSuccessStatusCode)
            //{
            //    JokeModel joke = response.Content.ReadFromJsonAsync<JokeModel>().Result;
            //    return View(joke);
            //}

            HttpResponseMessage response = _httpClient.GetAsync($"Any?blacklistFlags=nsfw,religious,racist,sexist,explicit&type=single").Result;
            if (response.IsSuccessStatusCode) 
            {
                string responseText = response.Content.ReadAsStringAsync().Result;
                JokeModel joke = JsonConvert.DeserializeObject<JokeModel>(responseText);
                return View(joke);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}