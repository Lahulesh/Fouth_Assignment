using Fouth.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fouth.Controllers
{
    public class ReportController : Controller
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        //////////////////////////////////Searching Functionality with Data Show/////////////////////////////////////////////////
        public ActionResult Index(string reportsearch)
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                string sqlquery = "select * from Asset where AssetName like'%" + reportsearch + "%' or VendorName like '%" + reportsearch + "%'";
                SqlCommand sqlcom = new SqlCommand(sqlquery, connect);
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                List<Report> rep = new List<Report>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    rep.Add(new Report
                    {
                        AssetID = Convert.ToInt32(dr["AssetID"]),
                        AssetName = Convert.ToString(dr["AssetName"]),
                        VendorName = dr["VendorName"].ToString(),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                        Cost = Convert.ToInt32(dr["Cost"]),
                });
                }
                return View(rep);
            }
        }
    }
}