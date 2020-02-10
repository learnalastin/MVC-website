using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeReg.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public int SizeOfCompany { get; set; }
        public string CompanyLegalType { get; set; }

    }
}