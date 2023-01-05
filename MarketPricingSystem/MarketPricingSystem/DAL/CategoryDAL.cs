using MarketPricingSystem.Models;
using MarketPricingSystem.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MarketPricingSystem.DAL
{
    public class CategoryDAL
    {
     
        String SqlconString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void setproductnocategory(int Categoryid)
        {
            int nocategoryid=0;
            using (SqlConnection con = new SqlConnection(SqlconString))
            {
              

                using (SqlCommand cmd = new SqlCommand("Nocategoryid", con))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            nocategoryid = Convert.ToInt32(sdr["categoryid"]);
                           
                        }
                    }
                    con.Close();
                }
               
                using (SqlCommand cmd = new SqlCommand("updatecategorystatus", con))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nocategoryid", nocategoryid);
                    cmd.Parameters.AddWithValue("@categoryid", Categoryid);
                    SqlDataReader sdr = cmd.ExecuteReader();

                }
                con.Close();
            }




        }
    }
}