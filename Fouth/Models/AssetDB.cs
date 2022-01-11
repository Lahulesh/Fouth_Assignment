using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fouth.Models
{
    public class AssetDB
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;

        // ********** VIEW Asset DETAILS ********************
        public List<Asset> GetAsset()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                List<Asset> Assetlist = new List<Asset>();
                SqlCommand cmd = new SqlCommand("AssetDetails", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Assetlist.Add(
                        new Asset
                        {
                            AssetId = Convert.ToInt32(dr["AssetID"]),
                            AssetName = Convert.ToString(dr["AssetName"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                            Cost = Convert.ToInt32(dr["Cost"])
                        });
                }
                return Assetlist;
            }
        }

        // **************** ADD NEW Asset *********************
        public bool AddAsset(Asset ObjAsset)
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("AddAsset", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@AssetID", ObjAsset.AssetID);
                cmd.Parameters.AddWithValue("@AssetName", ObjAsset.AssetName);
                cmd.Parameters.AddWithValue("@VendorName", ObjAsset.VendorName);
                cmd.Parameters.AddWithValue("@PurchaseDate", ObjAsset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Cost", ObjAsset.Cost);
                int i = cmd.ExecuteNonQuery();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        // ***************** UPDATE STUDENT DETAILS *********************
        public bool UpdateAsset(Asset ObjAsset)
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("UpdateAsset", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AssetID",ObjAsset.AssetId);
                cmd.Parameters.AddWithValue("@AssetName", ObjAsset.AssetName);
                cmd.Parameters.AddWithValue("@VendorName", ObjAsset.VendorName);
                cmd.Parameters.AddWithValue("@PurchaseDate", ObjAsset.PurchaseDate);
                cmd.Parameters.AddWithValue("@Cost", ObjAsset.Cost);
                int i = cmd.ExecuteNonQuery();
                if(i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // ********************** DELETE STUDENT DETAILS *******************
        public bool DeleteAsset(int id)
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("DeleteAsset", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AssetID", id);
                int i = cmd.ExecuteNonQuery();
                if (i >= 1)
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