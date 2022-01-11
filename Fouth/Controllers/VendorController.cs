using Fouth.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fouth.Controllers
{
    public class VendorController : Controller
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        // GET: Vendor
        public ActionResult Index()
        {
            return View(GetVendor());
        }
        public List<Vendor> GetVendor()
        {
            List<Vendor> VendorObj = new List<Vendor>();
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand com = new SqlCommand("select Vendor.VendorId, Vendor.VendorName, Vendor.VendorEmail, Vendor.VendorContact, City.CityName from Vendor inner join City on Vendor.CityId = City.CityId", connect);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Vendor v = new Vendor();
                    v.VendorId = Convert.ToInt32(dr["VendorId"]);
                    v.VendorName = dr["VendorName"].ToString();
                    v.VendorEmail = dr["VendorEmail"].ToString();
                    v.VendorContact = Convert.ToDouble(dr["VendorContact"]);
                    v.CityName = dr["CityName"].ToString();
                    VendorObj.Add(v);
                }
            }
            return VendorObj;
        }
    }
}