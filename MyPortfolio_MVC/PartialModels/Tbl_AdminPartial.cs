using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortfolio_MVC.Models
{
    public partial class Tbl_Admin
    {
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}