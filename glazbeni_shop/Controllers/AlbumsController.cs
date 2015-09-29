using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using glazbeni_shop.Models;
using glazbeni_shop.Reports;
using System.IO;

namespace glazbeni_shop.Controllers
{
    public class AlbumsController : Controller
    {
        private GlShopContext db = new GlShopContext();

        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }

        public PartialViewResult AlbumiPartial(string izvodac, string naziv)
        {
            var popis = from a in db.Albums select a;

            if (!String.IsNullOrEmpty(izvodac))
                popis = popis.Where(ap => (ap.nazivIzvodaca.nazivizvodaca).ToUpper().Contains(izvodac.ToUpper()));
            if (!String.IsNullOrEmpty(naziv))
                popis = popis.Where(ap => (ap.imealbuma).ToUpper().Contains(naziv.ToUpper()));
            return PartialView(popis.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idalbum,imealbuma,zanr,cijena,izvodac,slika,opis")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            var naziviZanra = db.Zanrs.ToList();
            //naziviZanra.Add(new Zanr { idzanr = 1, imezanr = "Nedefiniran" });
            ViewBag.naziviZanra = naziviZanra;
            var sviIzvodaci = db.Izvodacs.ToList();
            ViewBag.sviIzvodaci = sviIzvodaci;
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idalbum,imealbuma,zanr,cijena,izvodac,slika,opis")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int postoji(int id)
        {
            List<Album> kosarica = (List<Album>)Session["kosarica"];
            for (int i=0; i<kosarica.Count; i++)
            {
                if (kosarica[i].idalbum == id)
                    return i;
            }
            return -1;
        }

        public ActionResult Kupi(int id)
        {
            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (Session["kosarica"] == null)
            {
                List<Album> kosarica = new List<Album>();
                kosarica.Add(db.Albums.Find(id));
                Session["kosarica"] = kosarica;
                Session["broj"] = kosarica.Count.ToString();
            }
            else
            {
                List<Album> kosarica = (List<Album>)Session["kosarica"];
                kosarica.Add(db.Albums.Find(id));
                Session["kosarica"] = kosarica;
                Session["broj"] = kosarica.Count.ToString();
            }
            return View("Kupi");
        }

        public ActionResult Brisi(int id)
        {
            int index = postoji(id);
            List<Album> kosarica = (List<Album>)Session["kosarica"];
            kosarica.RemoveAt(index);
            Session["kosarica"] = kosarica;
            Session["broj"] = kosarica.Count.ToString();
            return View("Kupi");
        }

        public ActionResult PrikaziKosaricu()
        {
            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View("Kupi");
        }

        public FileStreamResult Ispisi(string izvodac, string naziv)
        {
            List<Album> kosarica = (List<Album>)Session["Kosarica"];
            var popis = from a in kosarica select a;

            if (!String.IsNullOrEmpty(izvodac))
                popis = popis.Where(ap => (ap.nazivIzvodaca.nazivizvodaca).ToUpper().Contains(izvodac.ToUpper()));
            if (!String.IsNullOrEmpty(naziv))
                popis = popis.Where(ap => (ap.imealbuma).ToUpper().Contains(naziv.ToUpper()));
            Racun r = new Racun(popis.ToList(), 
                Session["LoggedPrezime"].ToString(),
                Session["LoggedIme"].ToString(),
                Session["LoggedAdresa"].ToString(),
                Session["LoggedMail"].ToString(),
                Session["LoggedUlica"].ToString());
            return new FileStreamResult(new MemoryStream(r.Podaci), "racun.pdf");
        }
    }
}
