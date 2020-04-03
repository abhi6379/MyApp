using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        //Hosted web API REST Service base url  
        private string Baseurl;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Employee()
        {

            List<Employee> EmpInfo = new List<Employee>();
            Baseurl = "http://localhost/WebApi/api/Employee/";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Define request Authorization
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"admin:12345")));

                //Sending request to find web api REST service resource GetAllEmployees from Index method using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("GetEmp");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);

                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger alert-dismissible'>"
                                            + "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>"
                                            + "<strong>Error!</strong> Server Not Respond...</div>";
                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }
        [HttpGet]
        public async Task<ActionResult> EmployeeById(int id)
        {

            Employee EmpInfo = new Employee();
            Baseurl = "http://10.104.2.35/api/Employee/";
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Define request Authorization
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"admin:12345")));

                //Sending request to find web api REST service resource GetAllEmployees from Index method using HttpClient  

                HttpResponseMessage Res = await client.GetAsync(string.Format("GetEmp/{0}", id));

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<Employee>(EmpResponse);


                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }
        [HttpGet, Route("CreateEmployee")]
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost, Route("CreateEmployee")]
        public async Task<ActionResult> CreateEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                Baseurl = "http://localhost/WebApi/api/Employee/saveEmployee";

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    // client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request Authorization
                    string credential = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"admin:12345"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credential);
                    var httpContent = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");

                    HttpResponseMessage message = await client.PostAsync(Baseurl, httpContent);
                    if (message.StatusCode.ToString() == "Created")
                    {
                        if (message.Content != null)
                        {
                            // Error Here
                            var responseContent = await message.Content.ReadAsStringAsync();
                            Employee emps = JsonConvert.DeserializeObject<Employee>(responseContent);
                            ViewData["message"] = responseContent;

                        }
                        return RedirectToAction("Employee");
                    }
                    else
                    {

                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
    }
}