using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HR_Rerutement.Models;

namespace HR_Rerutement.Controllers
{
    public class SharedController : Controller
    {

        public ProjectContext db { get; private set; }

        public SharedController()
        {
            db = new ProjectContext();
        }

        // GET: Account
        public ActionResult Login()
        {
            //  return View(db.Demandeur.ToList());
            return View();
        }


        public ActionResult ConfirmEmail()
        {
            //  return View(db.Demandeur.ToList());
            return View();
        } 

        [HttpPost]
        public JsonResult ConfirmEmail(string email)
        {
            var user = db.Demandeurs.FirstOrDefault(x => x.Empl_Email.Equals(email.Trim()));
             if (user != null) {
                string Password = CreateRandomPassword(10);
                var userdb= db.Demandeurs.Find(user.Empl_Matricule);
                userdb.Permission.Password = Password;
                db.SaveChanges();
                Task.Run(() => Utilities.SendMail(email, "Nouveau Password Utilisateur ", $"Bonjour {user.Empl_Nom_Prenom },{Environment.NewLine}Votre mot de passe Utilisater sur Notre plateforme  a été modifié avec succés par l'administrateur.{Environment.NewLine}Nom d'utilisateur: {user.Empl_Matricule}{Environment.NewLine}Mot de passe: {Password}{Environment.NewLine}Cordialement,{Environment.NewLine}Équipe Tis"));
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("echec", JsonRequestBehavior.AllowGet);
            }

         

        }
        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        [HttpPost]
        public JsonResult Login(string password, string Matricule)
        {


            var user = db.Demandeurs.SingleOrDefault(a => a.Empl_Matricule.Equals(Matricule.Trim()));
            //  var user = db.Demandeurs.FirstOrDefault(a => a.Empl_Matricule.Equals(Matricule.Trim()));
            //var user1 = db.Demandeurs.Where(a => a.Empl_Matricule.Equals(Matricule.Trim())).ToList();
            //var user2 = db.Demandeurs.Find(Matricule.Trim());// cle primer

            if (user != null)
            {
                if (user.Permission != null && user.Permission.Password.Equals(password))
                {
                    Session["CurrentUser"] = user;


                    if (user.Permission.Role.Role1.Equals("Admin"))
                    {
                        return Json("Admin", JsonRequestBehavior.AllowGet);
                    }
                    if (user.Permission.Role.Role1.Equals("Chef"))
                    {
                        return Json("Chef", JsonRequestBehavior.AllowGet);
                    }
                    if (user.Permission.Role.Role1.Equals("RH"))
                    {
                        return Json("RH", JsonRequestBehavior.AllowGet);
                    }
                    if (user.Permission.Role.Role1.Equals("Demandeur"))
                    {
                        return Json("Demandeur", JsonRequestBehavior.AllowGet);
                    }




                    return Json("succes", JsonRequestBehavior.AllowGet);

                }

                else
                {
                    ViewBag.error1 = "Mot De Passe Incorrecte";
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                ViewBag.error = "Utilisateur Invalide";
                return Json("failed", JsonRequestBehavior.AllowGet);
            }

        }




    }
}
