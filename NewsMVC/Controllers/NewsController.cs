using Humanizer;
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

        public async Task<IActionResult> GetPublished()
        {
            IList<News> news = new List<News>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("published");

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

        public async Task<IActionResult> Post(NewsDto news)
        {
            NewsDto obj = new()
            {
                Title = news.Title,
                Content = news.Content,
                image = news.image,
                publication_date = news.publication_date,
                AuthorId = news.AuthorId
            };
            if (obj.Title != null && obj.Content != null && obj.publication_date != null && obj.AuthorId != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var formData = new List<KeyValuePair<string, object>>
                     {
                            new KeyValuePair<string, object>("Title", obj.Title),
                            new KeyValuePair<string, object>("Content", obj.Content),
                            new KeyValuePair < string, object > ("publication_date", obj.publication_date.ToString()),
                            new KeyValuePair < string, object > ("AuthorId", obj.AuthorId.ToString())
                     };
                    if(obj.image !=null)
                    {
                        formData.Add(new KeyValuePair<string, object>("image", obj.image));
                    }
                    var multipartContent = new MultipartFormDataContent();

                    foreach (var item in formData)
                    {
                        if (item.Value is string)
                        {
                            multipartContent.Add(new StringContent(item.Value.ToString()), item.Key);
                        }
                        else if (item.Value is IFormFile file)
                        {
                            var fileContent = new StreamContent(file.OpenReadStream());
                            multipartContent.Add(fileContent, item.Key, file.FileName);
                        }
                    }

                    HttpResponseMessage postData = await client.PostAsync("", multipartContent);

                    if (postData.IsSuccessStatusCode)
                        return RedirectToAction("Get");
                    else
                        Console.WriteLine("Error calling web API ");
                }
            }
            return View("Form", news);
        }

        public async Task<IActionResult> Put(NewsDto news)
            {
                NewsDto obj = new()
                {
                    Title = news.Title,
                    Content = news.Content,
                    image = news.image,
                    publication_date = news.publication_date,
                    AuthorId = news.AuthorId
                };
                if (obj.Title != null && obj.Content != null && obj.publication_date != null && obj.AuthorId != null && news.Id > 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseURL);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        var formData = new List<KeyValuePair<string, object>>
                     {
                            new KeyValuePair<string, object>("Title", obj.Title),
                            new KeyValuePair<string, object>("Content", obj.Content),
                            new KeyValuePair<string, object>("image", obj.image),
                            new KeyValuePair < string, object > ("publication_date", obj.publication_date.ToString()),
                            new KeyValuePair < string, object > ("AuthorId", obj.AuthorId.ToString())
                     };

                    if (obj.image != null)
                    {
                        formData.Add(new KeyValuePair<string, object>("image", obj.image));
                    }
                    var multipartContent = new MultipartFormDataContent();

                    foreach (var item in formData)
                    {
                        if (item.Value is string)
                        {
                            multipartContent.Add(new StringContent(item.Value.ToString()), item.Key);
                        }
                        else if (item.Value is IFormFile file)
                        {
                            var fileContent = new StreamContent(file.OpenReadStream());
                            multipartContent.Add(fileContent, item.Key, file.FileName);
                        }
                    }

                    HttpResponseMessage postData = await client.PutAsync($"{news.Id}", multipartContent);

                        if (postData.IsSuccessStatusCode)
                            return RedirectToAction("Get");
                        else
                            Console.WriteLine("Error calling web API ");
                    }
                }
                return View("Form", news);
            }

        public async Task<IActionResult> Delete(NewsDto news)
        {
            if (news.Id > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage postData = await client.DeleteAsync($"{news.Id}");

                    if (postData.IsSuccessStatusCode)
                        return RedirectToAction("Get");
                    else
                        Console.WriteLine("Error calling web API ");
                }
            }

            return View(news);
        }
    }
}