using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeReg.DbAccess
{
    public class Db
    {/*
        string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;


        public DataSet GetState()
        {
            SqlConnection sqlconn = new SqlConnection(mainconn);
            
            sqlconn.Open();

                string sqlquery1 = "Select Name from CompanyTable";
                SqlCommand sqlcomm = new SqlCommand(sqlquery1, sqlconn);

                SqlDataAdapter adapter = new SqlDataAdapter(sqlcomm);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet); // Filling data
                sqlcomm.ExecuteNonQuery();


                return dataSet;
            

        }




        public DataSet GetCompanies()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                string sqlquery = "Select Name from CompanyTable)";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();


            }


            return ();
        }

    }*/
    }
}