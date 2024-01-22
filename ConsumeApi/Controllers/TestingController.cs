using APi.Data;
using APi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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


        public IActionResult AddCustomer()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(localUrl);
                        var data = JsonConvert.SerializeObject(model);
                        var ContentData = new StringContent(data, Encoding.UTF8, "application/json");

                        if (model.Id == 0)
                        {
                            HttpResponseMessage response = client.PostAsync("/Customer/AddCustomer", ContentData).Result;
                            TempData["success"] = response.Content.ReadAsStringAsync().Result;

                        }
                        else
                        {
                            HttpResponseMessage response = client.PutAsync("/Customer/UpdateCustomer", ContentData).Result;
                            TempData["success"] = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ModelState is not Valid!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(localUrl);
                HttpResponseMessage response = client.DeleteAsync("/Customer/DeleteCustomer/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    TempData["error"] = $" {response.ReasonPhrase}";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Customer cus = new Customer();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(localUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("/Customer/GetCustomerId/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    cus = System.Text.Json.JsonSerializer.Deserialize<Customer>(stringData, new JsonSerializerOptions()
                    { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    TempData["error"] = $"{response.ReasonPhrase}";
                }



            }
            return View("AddCustomer", cus);
        }
    }
}
