using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace glazbeni_shop.Models
{
    [Table("izvodac")]
    public class Izvodac
    {
        [Key]
        public int idizvodaca { get; set; }
        [Display(Name="Izvođač")]
        public string nazivizvodaca { get; set; }
    }
}