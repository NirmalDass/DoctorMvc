using DoctorMvc.DtoViewModel;
using DoctorMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DoctorMvc.Controllers
{
    public class DoctorController : Controller
    {
        string Baseurl = "https://localhost:44374/";
        HttpClient client;
        public async Task<ActionResult> Index()
        {
            List<Doctor> DoctorInfo = new List<Doctor>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Doctor");
                if (Res.IsSuccessStatusCode)
                {
                    var DoctorResponse = Res.Content.ReadAsStringAsync().Result;
                    DoctorInfo = JsonConvert.DeserializeObject<List<Doctor>>(DoctorResponse);
                }
                return View(DoctorInfo);
            }
        }

        [HttpGet]
        public ActionResult DetailsId(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            Doctor model = new Doctor();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Doctor/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Doctor>(data);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Department> DepartmentInfo = new List<Department>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Doctor/Department/Doctor");
                if (Res.IsSuccessStatusCode)
                {
                    var DepartmentResponse = Res.Content.ReadAsStringAsync().Result;
                    DepartmentInfo = JsonConvert.DeserializeObject<List<Department>>(DepartmentResponse);
                }
                var model = new DepartmentViewModel
                {
                    Department= DepartmentInfo
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(Doctor model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            var response = client.PostAsJsonAsync<Doctor>("api/Doctor", model);
            var result=response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            List<Department> DepartmentInfo = new List<Department>();
            List<Doctor> doctors = new List<Doctor>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            Doctor model = new Doctor();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Doctor/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Doctor>(data);
            }
            HttpResponseMessage Res = await client.GetAsync("api/Doctor/Department/Doctor");
            if (Res.IsSuccessStatusCode)
            {
                var DepartmentResponse = Res.Content.ReadAsStringAsync().Result;
                DepartmentInfo = JsonConvert.DeserializeObject<List<Department>>(DepartmentResponse);
            }
            var model1 = new EditViewModel
            {
                Department = DepartmentInfo,
                Doctor = model
            };
            //ViewBag.Dept_Id = new SelectList(model1.Department, "Id", "DepartmentName");
            return View(model1);
        }

        [HttpPost]
        public ActionResult Edit(Doctor model, int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/Doctor/Edit?Id=" + Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            Doctor model = new Doctor();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Doctor/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Doctor>(data);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Doctor model, int Id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/Doctor/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
