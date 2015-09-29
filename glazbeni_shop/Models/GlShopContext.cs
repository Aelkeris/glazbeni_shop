using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace glazbeni_shop.Models
{
  
    public class GlShopContext : DbContext
    {
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Zanr> Zanrs { get; set; }
        public DbSet<Izvodac> Izvodacs { get; set; }
    }
}