using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MarketPricingSystem.Models;
using MySql.Data.MySqlClient;
using MarketPricingSystem.ViewModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace MarketPricingSystem.DAL
{
    public class otherDal
    {

        private marketpricing _context = new marketpricing();

       static String SqlconString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        private readonly SqlConnection con = new SqlConnection(SqlconString);







        public List<Userdetails> allusers()
        {
            List<Userdetails> users= new List<Userdetails>();




            using (SqlCommand cmd = new SqlCommand("Allusers", con))
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        users.Add(new Userdetails
                        {
                            userid = Convert.ToInt32(sdr["userid"]),
                            username = sdr["name"].ToString(),
                            userphonenumber = sdr["phonenumber"].ToString(),
                            usergmail = sdr["gmail"].ToString(),
                            userpassword = sdr["password"].ToString(),
                            rolename = sdr["rolename"].ToString(),

                        });
                    }
                }
                con.Close();
            }
            return users;
        }



    }

}
