using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace glazbeni_shop.Models
{
    [Table("korisnik")]
    [Serializable]
    public class Korisnik
    {
        [Key]
        public int idkorisnik { get; set; }
        [Display(Name = "Ime")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public string ime { get; set; }
        [Display(Name = "Prezime")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public string prezime { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public string mail { get; set; }
        [DataType(DataType.Password)]
        [Display(Name="Lozinka")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public string lozinka { get; set; }
        [Display(Name = "Mjesto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public string mjesto { get; set; }
        [Display(Name = "Ulica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public string ulica { get; set; }
        public int admin { get; set; }
    }
}