using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortfolio_MVC.Models
{
    public partial class Testimonial
    {
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}