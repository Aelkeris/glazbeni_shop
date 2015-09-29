using glazbeni_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace glazbeni_shop.Controllers
{
    public class HomeController : Controller
    {
        private GlShopContext db = new GlShopContext();

        public ActionResult Index()
        {
            int albumID = db.Albums.Max(a => a.idalbum);
            Album posljednji = db.Albums.Find(albumID);
            return View(posljednji);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}