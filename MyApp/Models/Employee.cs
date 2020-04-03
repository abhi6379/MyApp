using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    
    public class Employee
    {
        
        public int id { get; set; }
       
        public string name { get; set; }
       
        public string Gender { get; set; }

    }
   
}