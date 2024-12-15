using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortfolio_MVC.Models
{
    public partial class About
    {
        [NotMapped]
        public HttpPostedFileBase AboutImage { get; set; }

        [NotMapped]
        public HttpPostedFileBase CvFile { get; set; }
    }
}