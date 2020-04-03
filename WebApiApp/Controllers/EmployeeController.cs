using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("GetEmp/{id?}")]
        public HttpResponseMessage GetEmployee(int? id = null)
        {
            try
            {
                using (DatabaseContext con = new DatabaseContext())
                {
                    if (id != null)
                    {
                        Employee emp = con.emp.SingleOrDefault(x => x.id == id);
                        if (emp != null)
                        {
                            return Request.CreateResponse<Employee>(HttpStatusCode.OK, emp);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee Not Found!");
                        }
                    }
                    else
                    {
                        List<Employee> emp = con.emp.ToList();
                        if (emp != null)
                        {
                            return Request.CreateResponse<List<Employee>>(HttpStatusCode.OK, emp);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Records Found!");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetEmployee- " + ex.Message);
            }
        }
        [HttpPost, Route("saveEmployee")]
        public HttpResponseMessage saveEmployee(Employee emp)
        {
            
            try
            {
                using (DatabaseContext con = new DatabaseContext())
                {

                    con.emp.Add(emp);
                    con.SaveChanges();
                    return Request.CreateResponse<Employee>(HttpStatusCode.Created, emp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error: ", ex);
            }
        }
        [HttpGet]
        [Route("GetUserData")]
        public HttpResponseMessage GetUserData()
        {
            using (DatabaseContext con = new DatabaseContext())
            {
                List<User> obj = con.users.ToList();
                return Request.CreateResponse<List<User>>(HttpStatusCode.OK, obj);
            }
        }
    }
}
