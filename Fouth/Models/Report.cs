using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fouth.Models
{
    public class Report
    {

        [Key]
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public string VendorName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Cost { get; set; }
    }
}