using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using HR_Rerutement.Models;

namespace HR_Rerutement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private ProjectContext context;

        public AdminController()
        {

            context = new ProjectContext();
        }
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Session["CurrentUser"] == null)
            {
                RedirectToAction("login", "shared").ExecuteResult(this.ControllerContext);
            }
            if (Session["CurrentUser"] != null)
            {
                var user = Session["CurrentUser"] as Demandeur;
                if(user != null && user.Permission != null && !user.Permission.Role.Role1.Equals("Admin") )
                RedirectToAction("login", "shared").ExecuteResult(this.ControllerContext);
            }
        }
     
        public ActionResult GestionComptes()
        {
           
            var listDemandeur = context.Demandeurs.ToList();
            return View(listDemandeur);
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
        public ActionResult Dashboard()
        {
            ViewBag.countdemandeur = context.Demandeurs.ToList().Count();
            ViewBag.countCompte = context.Demandeurs.Where(x=>x.Permission != null).ToList().Count();
            return View();
        }
        public ActionResult ConsulterEmployee()
        {
            var a = context.Demandeurs.ToList();
            return View(a);
        }
        [HttpPost]
        public JsonResult AjouterDemandeur(string Empl_Matricule, string Empl_Email, int Role)
        {
            var role = context.Roles.Find(Role);
            var demandeur = context.Demandeurs.Find(Empl_Matricule);
            string Password = CreateRandomPassword(10);
            if (demandeur!=null)
            {
                demandeur.Permission = new Models.Permission { Empl_Matricule = demandeur.Empl_Matricule, Role = role, Password = Password };
               // Utilities.SendMail("amal.benamara@istic.ucar.tn", "hello","test");
                Task.Run(() => Utilities.SendMail(Empl_Email, "Nouveau compte Utilisateur ", $"Bonjour {demandeur.Empl_Nom_Prenom },{Environment.NewLine}Votre compte Utilisater sur Notre plateforme  a été crée par l'administrateur.{Environment.NewLine}Nom d'utilisateur: {demandeur.Empl_Matricule}{Environment.NewLine}Mot de passe: {Password}{Environment.NewLine}Cordialement,{Environment.NewLine}Équipe Tis"));
                context.SaveChanges();
            }
          

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDemandeuBymatricule(string matricule)
        {

            var Demandeur = context.Demandeurs.Find(matricule);



            return Json(new { nomPrenom = Demandeur.Empl_Nom_Prenom, matResponsable = Demandeur.Empl_MatResponsable, numPro = Demandeur.Empl_NumPro, fonction = Demandeur.Empl_Fonction, service = Demandeur.Empl_Service, email = Demandeur.Empl_Email, }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteDemandeur(string matricule)
        {
            var Demandeur = context.Demandeurs.Find(matricule);
           // Demandeur.Permission = null;
            context.Demandeurs.Remove(Demandeur);
            context.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ModifierDemandeur(string matricule, int role)
        {
            var Demandeur = context.Demandeurs.Find(matricule);
            var roleDemandeur = context.Roles.Find(role);
            Demandeur.Permission.Role = roleDemandeur;
            context.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Deconnexion()
        {
            Session.Abandon();
            return RedirectToAction("login", "shared");


        }
        public ActionResult MonCompte()
        {
            var user = Session["CurrentUser"] as Demandeur;
            return View(user);


        }

        public JsonResult ModiferCompte(Demandeur demandeur)
        {
            Demandeur demandeurdb = context.Demandeurs.Find(demandeur.Empl_Matricule);

            demandeurdb.Empl_Nom_Prenom = demandeur.Empl_Nom_Prenom;
            demandeurdb.Empl_NumPro = demandeur.Empl_NumPro;
            demandeurdb.Empl_Email = demandeur.Empl_Email;

            context.SaveChanges();
            var user = Session["CurrentUser"] as Demandeur;
            return Json("Success", JsonRequestBehavior.AllowGet);


        }


        public JsonResult sendProEmail(string receiver, string content)
        {
            var body = content;
            var message = new MailMessage();
            message.To.Add(new MailAddress(receiver));
            message.CC.Add(new MailAddress(ConfigurationManager.AppSettings.Get("CopieEmail")));
            message.From = new MailAddress(ConfigurationManager.AppSettings.Get("gmailAccountAddress"));
            message.Subject = "subject test";
            message.Body = body;
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings.Get("gmailAccountAddress"),
                    Password = ConfigurationManager.AppSettings.Get("gmailAccountPassword")
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    smtp.Send(message);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(Ex);
                    return Json("echec", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult ChangerPassword(string email)
        {

            var user = context.Demandeurs.FirstOrDefault(x => x.Empl_Email.Equals(email.Trim()));
            if (user != null)
            {
                string Password = CreateRandomPassword(10);
                var userdb = context.Demandeurs.Find(user.Empl_Matricule);
                userdb.Permission.Password = Password;
                context.SaveChanges();
                Task.Run(() => Utilities.SendMail(email, "Nouveau Password Utilisateur ", $"Bonjour {user.Empl_Nom_Prenom },{Environment.NewLine}Votre mot de passe Utilisater sur Notre plateforme  a été modifié avec succés par l'administrateur.{Environment.NewLine}Nom d'utilisateur: {user.Empl_Matricule}{Environment.NewLine}Mot de passe: {Password}{Environment.NewLine}Cordialement,{Environment.NewLine}Équipe Tis"));
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("echec", JsonRequestBehavior.AllowGet);
            }



        }





    }
}