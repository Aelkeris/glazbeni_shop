using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace glazbeni_shop.Models
{
    [Table("zanr")]
    public class Zanr
    {
        [Key]
        public int idzanr { get; set; }
        [Display(Name = "Žanr")]
        public string imezanr { get; set;}
    }
}