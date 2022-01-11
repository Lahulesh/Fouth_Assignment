using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fouth.Models
{
    public class Asset
    {
        string str = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;

        [Key]
        public int AssetId { get; set; }


        [Required]
        public string AssetName { get; set; }


        [Required]
        public DateTime PurchaseDate { get; set; }


        [Required]
        public decimal Cost { get; set; }

        public int? VendorId { get; set; }

        [Required]
        public string VendorName { get; set; }


        public List<Asset> ShowAllAsset { get; set; }
        public List<SelectListItem> vendorMaster { get; set; }
        public List<VendorMaster> vendorSelect { get; set; }

        public List<Asset> GetAllAsset()
        {
            List<Asset> assetMaster = new List<Asset>();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("select AssetId, AssetName, PurchaseDate, VendorName,Cost from Asset", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Asset assetObj = null;
                for ( int i = 0; i < ds.Tables[0].Rows.Count; i++ )
                {
                    assetObj = new Asset();
                    assetObj.AssetId = Convert.ToInt32(ds.Tables[0].Rows[i]["AssetId"]);
                    assetObj.AssetName = Convert.ToString(ds.Tables[0].Rows[i]["AssetName"]);
                    assetObj.VendorName = Convert.ToString(ds.Tables[0].Rows[i]["VendorName"]);
                    assetObj.PurchaseDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["PurchaseDate"]);
                    assetObj.Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Cost"]);
                    assetMaster.Add(assetObj);
                }
                return assetMaster;
            }
        }
        public List<SelectListItem> GetVendorList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlDataAdapter da = new SqlDataAdapter("select VendorName from Vendor", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for ( int i = 0; i < ds.Tables[0].Rows.Count; i++ )
                {
                    items.Add(new SelectListItem
                    {
                        Text = Convert.ToString(ds.Tables[0].Rows[i]["VendorName"]),
                        Value = Convert.ToString(ds.Tables[0].Rows[i]["VendorName"])
                    });

                }
                return items;
            }
        }
        public int AddAsset(Asset asset)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("AddAsset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssetName", asset.AssetName);
                cmd.Parameters.AddWithValue("@VendorName", asset.VendorName);
                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Cost", asset.Cost);
                SqlParameter returnvalue = new SqlParameter("returnVal", SqlDbType.Int);
                returnvalue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnvalue);
                con.Open();
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(returnvalue.Value);
                return count;
            }
        }
        public int UpdateAsset(Asset asset)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateAsset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssetId", asset.AssetId);
                cmd.Parameters.AddWithValue("@AssetName", asset.AssetName);
                cmd.Parameters.AddWithValue("@VendorName", asset.VendorName);
                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Cost", asset.Cost);
                SqlParameter returnvalue = new SqlParameter("returnVal", SqlDbType.Int);
                returnvalue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnvalue);
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(returnvalue.Value);
                return count;
            }
        }
        public bool DeleteAsset(int id)
        {
            using ( SqlConnection con = new SqlConnection(str) )
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteAsset", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssetId", id);
                int i = cmd.ExecuteNonQuery();
                if ( i >= 1 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}