using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketPricingSystem.ViewModel;
using MarketPricingSystem.Models;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MarketPricingSystem.DAL
{
    public class productDal
    {
       static String SqlconString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        private readonly SqlConnection con = new SqlConnection(SqlconString);




        public List<productdetails> supermarketproductlist(int supermarketid)
        {
            List<productdetails> products = new List<productdetails>();




            using (SqlCommand cmd = new SqlCommand("Marketproducts", con))
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@marketid", supermarketid);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        products.Add(new productdetails
                        {   productid = Convert.ToInt32(sdr["productid"]),
                            productname = sdr["productname"].ToString(),
                            productBarcode = Convert.ToInt32(sdr["BarcodeNb"]),
                            productDescription = sdr["productDescription"].ToString(),
                            productprice = Convert.ToInt32(sdr["price"]),
                            productcategory = sdr["categoryname"].ToString(),

                        });
                    }
                }
                con.Close();
            }
            return products;
        }



        public ViewModel.product product(int id)//need productid for future work
        {
            ViewModel.product product = new ViewModel.product();

            //install sql connector to can open connection
            using (SqlConnection con = new SqlConnection(SqlconString))
            {
           
                using (SqlCommand cmd = new SqlCommand("product", con))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productid", id);
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {


                            product.productid = Convert.ToInt32(sdr["productid"]);
                            product.productname = sdr["productname"].ToString();
                            product.productBarcode = Convert.ToInt32(sdr["barcodenb"]);
                            product.productDescription = sdr["productdescription"].ToString();
                            product.productcategory = sdr["categoryname"].ToString();
                           
                        }
                    }
                    con.Close();
                }
                return product;
            }

        }



        public List<ViewModel.product> ProductList()//need productid for future work //all products with categories without price
        {
            List<ViewModel.product> products = new List<ViewModel.product>();

            //install sql connector to can open connection
            using (SqlConnection con = new SqlConnection(SqlconString))
            {
            
               using (SqlCommand cmd = new SqlCommand("ProductList", con))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            products.Add(new ViewModel.product
                            {
                                productid = Convert.ToInt32(sdr["productid"]),
                                productname = sdr["productname"].ToString(),
                                productBarcode = Convert.ToInt32(sdr["barcodenb"]),
                                productDescription = sdr["productdescription"].ToString(),
                                productcategory = sdr["categoryname"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }
                return products;
            }

        }








        public List<ViewModel.product> Allproducts()//need productid for future work//only all priced products (dont return products that are not priced)
        {
            List<ViewModel.product> products = new List<ViewModel.product>();

            //install sql connector to can open connection
            using (SqlConnection con = new SqlConnection(SqlconString))
            {
                using (SqlCommand cmd = new SqlCommand("Allproducts_withid", con))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
            
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            products.Add(new ViewModel.product
                            {
                                productid = Convert.ToInt32(sdr["productid"]),
                                productname = sdr["productname"].ToString(),
                                productBarcode = Convert.ToInt32(sdr["barcodenb"]),
                                productDescription = sdr["productdescription"].ToString(),
                                productcategory = sdr["categoryname"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }
                return products;
            }

        }




        public List<productvalue> GetCheapestproduct(int id)
        {

            List<productvalue> topcheapthree = new List<productvalue>();
            using (SqlConnection con = new SqlConnection(SqlconString))
            {

                using (SqlCommand cmd = new SqlCommand("Get_Cheapestproduct", con))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productid", id);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            topcheapthree.Add(new productvalue
                            {
                                supermarketname = sdr["supermarketname"].ToString(),
                                productname = sdr["productname"].ToString(),
                                productprice = Convert.ToInt32(sdr["price"]),

                            });

                        }
                    }
                    con.Close();
                }

            }
            return topcheapthree;

        }



        public List<productvalue> GetExpensiveproduct(int id)
        {

            List<productvalue> topexpensivethree = new List<productvalue>();
            using (SqlConnection con = new SqlConnection(SqlconString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Expensiveproduct", con))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productid", id);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                           
                            topexpensivethree.Add(new productvalue
                            {
                                supermarketname = sdr["supermarketname"].ToString(),
                                productname = sdr["productname"].ToString(),
                                productprice = Convert.ToInt32(sdr["price"]),

                            });

                        }
                    }
                    con.Close();
                }

            }
            return topexpensivethree;

        }

    }

}
