using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginPageMVC.Models
{
    public class employee
    {
        public int Sno { get; set; }
        public string EmpNumber { get; set; }
        public string EmpName { get; set; }
        public string EmpJob { get; set; }
        public int EmpSal { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPwd { get; set; }
    }
}