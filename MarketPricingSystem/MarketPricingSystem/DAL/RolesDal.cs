using MarketPricingSystem.Models;
using MarketPricingSystem.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MarketPricingSystem.DAL
{
    public class RolesDal
    {
        private marketpricing _context = new marketpricing();


        private readonly SqlConnection con = new SqlConnection("Server=(local);DataBase=marketpricing;Integrated Security=SSPI");





        public List<permission> Rolepermission (int roleid)
        {
            List<permission> permissions = new List<permission>();
            using (SqlCommand cmd = new SqlCommand("Rolepermission", con))
            {
                cmd.Connection = con;
                con.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@roleid", roleid);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        permissions.Add(new permission
                        {
                            PermissionName = sdr["permissionname"].ToString(),
                         
                        });
                    }
                }
                con.Close();
            }
            return permissions;
        }



        public  void addperTorole(int roleid , int perid)
        {
            var checkexist = _context.rolepermissions.FirstOrDefault(rp => rp.RoleId == roleid && rp.PermissionId == perid);
            if (checkexist!= null)
            {
                return;
            }


            rolepermission newrolepermission = new rolepermission();

            newrolepermission.RoleId = roleid;
            newrolepermission.PermissionId = perid;

                _context.rolepermissions.Add(newrolepermission);
                _context.SaveChanges();
            


        }

        

    }
}