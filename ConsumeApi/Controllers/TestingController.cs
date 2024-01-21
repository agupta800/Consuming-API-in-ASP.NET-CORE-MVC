using APi.Data;
using APi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsumeApi.Controllers
{
    public class TestingController : Controller
    {
        private string localUrl = "https://localhost:7134";

        //public async Task<IActionResult> Index()
        //{
        //    var existing = await _context.customers.ToListAsync();
        //    return View(existing);
        //}

        public IActionResult Index()
        {
            var data = new List<Customer>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(localUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("/Customer/GetAllCustomers").Result;

                    client.Dispose();
                    if (response.IsSuccessStatusCode)
                    {
                        string stringData = response.Content.ReadAsStringAsync().Result;
                        data = JsonConvert.DeserializeObject<List<Customer>>(stringData);
                    }
                    else
                    {
                        TempData["error"] = $"{response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["exception"] = ex.Message;
            }
            return View(data);
        }
    }
}
