using Fouth.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fouth.Controllers
{
    public class Asset1Controller : Controller
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        //////////////////////////////////Searching Functionality/////////////////////////////////////////////////
        public ActionResult Index(string assetsearch)
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                string sqlquery = "select * from Asset where AssetName like'%" + assetsearch + "%' or VendorName like '%" + assetsearch + "%'";
                SqlCommand sqlcom = new SqlCommand(sqlquery, connect);
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                List<Asset> ass = new List<Asset>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ass.Add(new Asset
                    {
                        AssetId = Convert.ToInt32(dr["AssetID"]),
                        AssetName = Convert.ToString(dr["AssetName"]),
                        VendorName = dr["VendorName"].ToString(),
                        PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                        Cost = Convert.ToInt32(dr["Cost"]),
                    });
                }
                return View(ass);
            }
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Asset/Create
        public ActionResult Create()
        {
            Asset assetObj = new Asset();
            assetObj.vendorMaster = assetObj.GetVendorList();
            return View(assetObj);
        }
        [HttpPost]
        public ActionResult Create(Asset asset, FormCollection collection)
        {
            int status = 0;
            status = asset.AddAsset(asset);
            if (status == 1)
            {
                ModelState.Clear();
            }
            else
            {
                Asset obj = new Asset();
                obj.vendorMaster = obj.GetVendorList();
                TempData["Duplicate"] = asset.AssetName + " " + "Asset is already added";
                //return View(obj);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> itemVendor = new List<SelectListItem>();
            Asset assetObj = new Asset();
            itemVendor = assetObj.GetVendorList();
            var SelectVendor = assetObj.VendorId;
            ViewBag.VendorList = new SelectList(itemVendor, "Value", "Text", SelectVendor);
            return View(assetObj.GetAllAsset().Find(asset => asset.AssetId == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Asset asset, FormCollection collection)
        {
            int status = 0;
            asset.AssetId = id;
            asset.AssetName = collection["AssetName"].ToString();
            asset.VendorName = collection["VendorName"].ToString();
            asset.PurchaseDate = DateTime.Parse(collection["PurchaseDate"]);
            asset.Cost = decimal.Parse(collection["Cost"]);
            status = asset.UpdateAsset(asset);
            if (status == 1)
            {
                ModelState.Clear();
            }
            else
            {
                TempData["Duplicate"] = "City name is already added";
                List<SelectListItem> itemVendor = new List<SelectListItem>();
                Asset objAsset = new Asset();
                itemVendor = objAsset.GetVendorList();
                var SelectVendor = objAsset.VendorId;

                ViewBag.VendorList = new SelectList(itemVendor, "Value", "Text", SelectVendor);
                //return View(objAsset);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Asset assetDb = new Asset();
            if (assetDb.DeleteAsset(id))
            {
                ViewBag.AlertMsg = "Student Deleted Successfully";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}