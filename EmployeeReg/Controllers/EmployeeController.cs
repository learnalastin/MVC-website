using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using EmployeeReg.Models;
using System.Configuration;

namespace EmployeeReg.Controllers
{
    public class EmployeeController : Controller
    {
       // string connectString = @"Data Source=XE-PC;Initial Catalog=EmployeeApp;Integrated Security=True";

        [HttpGet]
        public ActionResult Index()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [dbo].[EmployeeTable]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            List<Employee> lemp = new List<Employee>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new Employee {
                    EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    MiddleName = Convert.ToString(dr["MiddleName"]),
                    DateOfHire = Convert.ToDateTime(dr["DateOfHire"]),
                    JobPosition = Convert.ToString(dr["JobPosition"]),
                    CompanyName = Convert.ToString(dr["CompanyName"]),
                });

            }

            sqlconn.Close();

            return View(lemp);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View(new Employee());
        }

        public Fill()
        {
            //Create object of your Model of controller
            Model objModel = new Model();
            //Call function to  load the data for the DropDownList
            objModel.GetDropDownList();
            //return view with your object of model
            return View(objModel);
        }
        /*
        public DataSet GetState()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();

                string sqlquery1 = "Select Name from CompanyTable";
                SqlCommand sqlcomm = new SqlCommand(sqlquery1, sqlconn);

                SqlDataAdapter adapter = new SqlDataAdapter(sqlcomm);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet); // Filling data
                sqlcomm.ExecuteNonQuery();


                return dataSet;
            }

        }

    */



        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {            

                string sqlquery = "Insert into EmployeeTable values(@LastName,@FirstName,@MiddleName,@DateOfHire,@JobPosition,@CompanyName)";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@LastName", employee.LastName);
                sqlcomm.Parameters.AddWithValue("@FirstName", employee.FirstName);
                sqlcomm.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                sqlcomm.Parameters.AddWithValue("@DateOfHire", employee.DateOfHire);
                sqlcomm.Parameters.AddWithValue("@JobPosition", employee.JobPosition);
                sqlcomm.Parameters.AddWithValue("@CompanyName", employee.CompanyName);
                sqlcomm.ExecuteNonQuery();
                //sqlconn.Close();
            }                
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            Employee employee = new Employee();
            DataTable dtb = new DataTable();
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string query = "select * from EmployeeTable where EmployeeID = @EmployeeID";
                SqlDataAdapter sda = new SqlDataAdapter(query, mainconn);
                sda.SelectCommand.Parameters.AddWithValue("@EmployeeID",id);
                sda.Fill(dtb);

            }
            if (dtb.Rows.Count == 1)
            {
                employee.EmployeeID = Convert.ToInt32(dtb.Rows[0][0].ToString());
                employee.LastName = dtb.Rows[0][1].ToString();
                employee.FirstName = dtb.Rows[0][2].ToString();
                employee.MiddleName = dtb.Rows[0][3].ToString();
                employee.DateOfHire = Convert.ToDateTime(dtb.Rows[0][4].ToString());
                employee.JobPosition = dtb.Rows[0][5].ToString();
                employee.CompanyName = dtb.Rows[0][6].ToString();
                return View(employee);
            }
            else
                return RedirectToAction("Index");
 
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string sqlquery = "Update EmployeeTable set LastName=@LastName," +
                    "Firstname=@FirstName,MiddleName=@MiddleName,DateOfHire=@DateOfHire," +
                    "JobPosition=@JobPosition,CompanyName=@CompanyName " +
                    "where EmployeeID = @EmployeeID";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                sqlcomm.Parameters.AddWithValue("@LastName", employee.LastName);
                sqlcomm.Parameters.AddWithValue("@FirstName", employee.FirstName);
                sqlcomm.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                sqlcomm.Parameters.AddWithValue("@DateOfHire", employee.DateOfHire);
                sqlcomm.Parameters.AddWithValue("@JobPosition", employee.JobPosition);
                sqlcomm.Parameters.AddWithValue("@CompanyName", employee.CompanyName);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }

            return RedirectToAction("Index");
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string sqlquery = "Delete from EmployeeTable where EmployeeID = @EmployeeID";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@EmployeeID", id);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
