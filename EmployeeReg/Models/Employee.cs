using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EmployeeReg.Models
{
        public class Employee
        {
            public int EmployeeID { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public DateTime DateOfHire { get; set; }
            public string JobPosition { get; set; }
            public string CompanyName { get; set; }
            [NotMapped]
            public List<Company> companies { get; set; }
    
            public void GetDropDownList()
            {

                //Pass your data base connection string here 
                using (SqlConnection c = new SqlConnection("Data Source = XE - PC; Initial Catalog = EmployeeApp; Integrated Security = True"))
                //Pass your SQL Query and above created SqlConnection object  "c"
                using (SqlCommand cmd = new SqlCommand("SELECT Name FROM CompanyTable", c))
                {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DropDownList.Add(rdr["Name"].ToString());
        
                        }
                    }
                }
            }
    
        }
}