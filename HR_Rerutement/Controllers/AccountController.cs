using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HR_Rerutement.Models;

namespace HR_Rerutement.Controllers
{
    public class AccountController : Controller
    {
      
        public ProjectContext db { get; private set; }

        public AccountController()
        {
            db = new ProjectContext();
        }

        // GET: Account
        public ActionResult Index()
        {
          //  return View(db.Demandeur.ToList());
            return View();
        }

        


        [HttpPost]
        [Authorize]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            FormsAuthentication.SignOut(); //In order to force logout in LDAP authentication
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandeur demandeur = db.Demandeurs.Find(id);
            if (demandeur == null)
            {
                return HttpNotFound();
            }
            return View(demandeur);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Empl_Matricule,Empl_Nom_Prenom,Empl_MatResponsable,Empl_NumPro,Empl_Fonction,Empl_CentreCout,Empl_Service,Empl_Active,Empl_Email,Password")] Demandeur demandeur)
        {
            if (ModelState.IsValid)
            {
                db.Demandeurs.Add(demandeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(demandeur);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandeur demandeur = db.Demandeurs.Find(id);
            if (demandeur == null)
            {
                return HttpNotFound();
            }
            return View(demandeur);
        }

        // POST: Account/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Empl_Matricule,Empl_Nom_Prenom,Empl_MatResponsable,Empl_NumPro,Empl_Fonction,Empl_CentreCout,Empl_Service,Empl_Active,Empl_Email,Password")] Demandeur demandeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demandeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(demandeur);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandeur demandeur = db.Demandeurs.Find(id);
            if (demandeur == null)
            {
                return HttpNotFound();
            }
            return View(demandeur);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Demandeur demandeur = db.Demandeurs.Find(id);
            db.Demandeurs.Remove(demandeur);
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
    }
}
