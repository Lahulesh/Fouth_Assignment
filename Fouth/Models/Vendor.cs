using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fouth.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public double VendorContact { get; set; }
        public string CityName { get; set; }
    }
}