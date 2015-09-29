using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using glazbeni_shop.Models;

namespace glazbeni_shop.Controllers
{
    public class KorisniksController : Controller
    {
        private GlShopContext db = new GlShopContext();

        // GET: Korisniks
        public ActionResult NakonRegistracije()
        {
            return View();
        }

        // GET: Korisniks/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //zabranjuje pristup ukoliko se id-ovi ne poklapaju
            if (id.ToString() != Session["ID"].ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            Korisnik korisnik = db.Korisnik.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // GET: Korisniks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //zabranjuje pristup ukoliko se id-ovi ne poklapaju
            if (id.ToString() != Session["ID"].ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            Korisnik korisnik = db.Korisnik.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        // POST: Korisniks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idkorisnik,ime,prezime,mail,lozinka,mjesto,ulica")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnik).State = EntityState.Modified;
                db.SaveChanges();
                Session["LoggedIme"] = korisnik.ime.ToString();
                return RedirectToAction("Index","Home");
            }
            return View(korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registracija([Bind(Include = "idkorisnik,ime,prezime,mail,lozinka,mjesto,ulica")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Korisnik.Add(korisnik);
                db.SaveChanges();
                return RedirectToAction("NakonRegistracije");
            }
            return View("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string mail, string lozinka)
        {
            if (ModelState.IsValid)
            {
                var v = db.Korisnik.Where(a => a.mail.Equals(mail) && a.lozinka.Equals(lozinka)).FirstOrDefault();
                if (v != null)
                {
                    Session["ID"] = v.idkorisnik;
                    Session["LoggedIme"] = v.ime.ToString();
                    Session["LoggedPrezime"] = v.prezime.ToString();
                    Session["LoggedAdresa"] = v.mjesto.ToString();
                    Session["LoggedMail"] = v.mail.ToString();
                    Session["LoggedUlica"] = v.ulica.ToString();
                    if(v.admin==1)
                    {
                        Session["Admin"] = true;
                    }
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
