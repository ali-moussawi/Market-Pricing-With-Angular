using MarketPricingSystem.Models;
using MarketPricingSystem.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketPricingSystem.DAL
{
    public class TrackDal
    {
        static String SqlconString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public string productprices(int marketid , int productid )
        {


            string data = "";

      


            using (SqlConnection con = new SqlConnection(SqlconString))
            {
                string query = " select productname, price, FORMAT(date, 'yyyy-MM-dd') as date\r\n from marketpricing.products p , marketpricing.productprices pp  where p.productid = pp.productid  and pp.productid = "+productid+" and pp.supermarketid = "+marketid + "   and pp.id in (select MAX(id) as id from marketpricing.productprices pp where pp.supermarketid=" + marketid + " Group by date);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            if (!data.StartsWith(sdr["productname"].ToString()))
                            {
                                string name = sdr["productname"].ToString();

                                data += name + ",";

                            }
                            data+= sdr["price"].ToString()+"," ;
                            data+= sdr["date"] +"," ;

                        }
                    }
                    con.Close();
                }
            }
      

            return data;
        }
    }
}
