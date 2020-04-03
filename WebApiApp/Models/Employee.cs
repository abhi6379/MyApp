using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiApp.Models
{
    [Table("tbl_employee")]
    public class Employee
    {
        [Key, DataType("bigint")]
        public int id { get; set; }
        [Display(Name = "Employee Name"), DataType("nvarchar", ErrorMessage = "Please Enter Name"), MaxLength(50, ErrorMessage = "Please Enter Name upto 50 character long!")]
        public string name { get; set; }
        [DataType("varchar"), MaxLength(10)]
        public string Gender { get; set; }

    }
    [Table("tbl_user")]
    public class User
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class Services
    {
        public static bool Login(string username, string password)
        {
            using (DatabaseContext con = new DatabaseContext())
            {
                return con.users.Any(x => x.userName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.password == password);
            }
        }
    }
}