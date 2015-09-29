using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace glazbeni_shop.Models
{
    [Table("album")]
    public class Album
    {
        [Key]
        public int idalbum { get; set; }
        [Display(Name = "Ime albuma")]
        public string imealbuma { get; set; }
        [Display(Name = "Žanr")]
        [ForeignKey("nazivZanra")]
        public int zanr { get; set; }
        [Display(Name = "Cijena")]
        public int cijena { get; set; }
        [Display(Name = "Izvođač")]
        [ForeignKey("nazivIzvodaca")]
        public int izvodac { get; set; }
        public string slika { get; set; }
        [Display(Name = "Opis")]
        public string opis { get; set; }
        public virtual Zanr nazivZanra { get; set; }
        public virtual Izvodac nazivIzvodaca { get; set; }
    }
}