using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace NewsMVC.Controllers
{
    public class NewsController : Controller
    {
        private string baseURL = "https://localhost:44323/api/News/";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get()
        {
            IList<News> news = new List<News>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage getData = await client.GetAsync("");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    news = JsonConvert.DeserializeObject<List<News>>(results);
                }
                else
                    Console.WriteLine("Error calling web API ");

                ViewData.Model = news;

            }
            return View();
        }
    }
}
