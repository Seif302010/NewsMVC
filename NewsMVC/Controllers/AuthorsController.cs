using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NewsMVC.Models.DomainModels;
using System.Data;
using System.Net.Http.Headers;

namespace NewsMVC.Controllers
{
    public class AuthorsController : Controller
    {
        private string baseURL = "https://localhost:44323/api/Authors/";

        public async Task<IActionResult> Index()
        { 

            return RedirectToAction("Get");

        }
        public async Task<IActionResult> Get()
        {
            IList<Author> authors = new List<Author>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    authors = JsonConvert.DeserializeObject<List<Author>>(results);
                }
                else
                    Console.WriteLine("Error calling web API ");

                ViewData.Model = authors;

            }
            return View();
        }
        public async Task<IActionResult> Post(Author author)
        {
            Author obj = new()
            {
                Name = author.Name
            };
            if(obj.Name != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var formData = new List<KeyValuePair<string, string>>
                     {
                            new KeyValuePair<string, string>("Name", obj.Name)
                     };
                    var formContent = new FormUrlEncodedContent(formData);

                    HttpResponseMessage postData = await client.PostAsync("", formContent);

                    if (postData.IsSuccessStatusCode)
                        return RedirectToAction("Get");
                    else
                        Console.WriteLine("Error calling web API ");
                }
            }
            return View("Form", author);
        }

        public async Task<IActionResult> Put(Author author)
        {
            Author obj = new()
            {
                Name = author.Name
            };
            if (obj.Name != null&& author.Id>0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var formData = new List<KeyValuePair<string, string>>
                     {
                            new KeyValuePair<string, string>("Name", obj.Name)
                     };
                    var formContent = new FormUrlEncodedContent(formData);

                    HttpResponseMessage postData = await client.PutAsync($"{author.Id}", formContent);

                    if (postData.IsSuccessStatusCode)
                        return RedirectToAction("Get");
                    else
                        Console.WriteLine("Error calling web API ");
                }
            }
            return View("Form",author);
        }

        public async Task<IActionResult> Delete(Author author)
        {
            if (author.Id > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzZWlmIiwianRpIjoiZWM3NmEyNzEtNmM4ZS00YjQwLWIzNmEtYWY3NDhlOTcwMWE2IiwiZW1haWwiOiJzZWlmMzAyMDEwQGdtYWlsLmNvbSIsInVpZCI6IjM2ZmIwNjg4LWJhZDUtNDkyYS1iNmVkLWRjZDUyMTc3OTkxOSIsInJvbGVzIjpbIkFkbWluIiwiVXNlciJdLCJleHAiOjE2ODgwMTQ4NzQsImlzcyI6IlNlY3VyZUFQSSIsImF1ZCI6IlNlY3VyZUFQSV9Vc2VyIn0.COBtEcQQN2elCwl86JqfzdTWWom2jAC6cpjB-GBTPO4";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage postData = await client.DeleteAsync($"{author.Id}");

                    if (postData.IsSuccessStatusCode)
                        return RedirectToAction("Get");
                    else
                        Console.WriteLine("Error calling web API ");
                }
            }

            return View(author);
        }


    }
}
