using EmployeeReg.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeReg.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [HttpGet]
        public ActionResult Index()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [dbo].[CompanyTable]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            List<Company> lemp = new List<Company>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new Company
                {
                    CompanyID = Convert.ToInt32(dr["CompanyID"]),
                    Name = Convert.ToString(dr["Name"]),
                    SizeOfCompany = Convert.ToInt32(dr["SizeOfCompany"]),
                    CompanyLegalType = Convert.ToString(dr["CompanyLegalType"]),
                });
            }
            sqlconn.Close();
            return View(lemp);
        }

        // GET: Company/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Company());
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(Company company)
        {
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

                using (SqlConnection sqlconn = new SqlConnection(mainconn))
                {
                    sqlconn.Open();
                    string sqlquery = "Insert into CompanyTable values(@Name,@SizeOfCompany,@CompanyLegalType)";
                    SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                    sqlcomm.Parameters.AddWithValue("@Name", company.Name);
                    sqlcomm.Parameters.AddWithValue("@SizeOfCompany", company.SizeOfCompany);
                    sqlcomm.Parameters.AddWithValue("@CompanyLegalType", company.CompanyLegalType);
                    sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();
                }
                return RedirectToAction("Index");
            }

        // GET: Company/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            Company company = new Company();
            DataTable dtb = new DataTable();
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string query = "select * from CompanyTable where CompanyID = @CompanyID";
                SqlDataAdapter sda = new SqlDataAdapter(query, mainconn);
                sda.SelectCommand.Parameters.AddWithValue("@CompanyID", id);
                sda.Fill(dtb);

            }
            if (dtb.Rows.Count == 1)
            {
                company.CompanyID = Convert.ToInt32(dtb.Rows[0][0].ToString());
                company.Name = dtb.Rows[0][1].ToString();
                company.SizeOfCompany = Convert.ToInt32(dtb.Rows[0][2].ToString());
                company.CompanyLegalType = dtb.Rows[0][3].ToString();
                return View(company);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(Company company)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string sqlquery = "Update CompanyTable set Name=@Name," +
                    "SizeOfCompany=@SizeOfCompany,CompanyLegalType=@CompanyLegalType where CompanyID = @CompanyID";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                sqlcomm.Parameters.AddWithValue("@Name", company.Name);
                sqlcomm.Parameters.AddWithValue("@SizeOfCompany", company.SizeOfCompany);
                sqlcomm.Parameters.AddWithValue("@CompanyLegalType", company.CompanyLegalType);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }
            return RedirectToAction("Index");
        }

        // GET: Company/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string sqlquery = "Delete from CompanyTable where CompanyID = @CompanyID";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@CompanyID", id);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
            }
            return RedirectToAction("Index");

          
        }
      
    }
}
