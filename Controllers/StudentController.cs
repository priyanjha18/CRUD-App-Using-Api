using System.Drawing.Text;
using System.Text;
using System.Text.Json.Serialization;
using CrudAppUsingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrudAppUsingApi.Controllers

{

    public class StudentController : Controller
    {
        private string url = "https://localhost:7263/api/StudentAPI/";
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null)
                {
                    students = data;
                }
            }
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Student Added Successfully";
                return RedirectToAction("Index");

            }
            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Student>(data);
                if(result != null)
                {
                    std = result;
                }

            }
            return View(std);


        }
        [HttpPost]
        public IActionResult Edit(int id,Student std)
        {
            if (std.id != id)
            {
                return NotFound();
            }
            var data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + id,content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Student Updated Successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Student>(data);
                if (result != null)
                {
                    std = result;
                }
                
            }
            return View(std);


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Student>(data);
                if (result != null)
                {
                    std = result;
                }
            }
            return View(std);


        }
        [HttpPost]
        public IActionResult Delete(Student std,int id )
        {
            if (id != null || std.id==id)
            {
                HttpResponseMessage response = client.DeleteAsync(url + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Student Deleted Successfully";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");

        }




    }
}


