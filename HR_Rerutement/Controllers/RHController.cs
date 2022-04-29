﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using HR_Rerutement.Models;
using Newtonsoft.Json;

namespace HR_Rerutement.Controllers
{
    public class RHController : Controller
    {
        // GET: RH
        private ProjectContext context;

        public RHController()
        {

            context = new ProjectContext();
        }
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Session["CurrentUser"] == null)
            {
                RedirectToAction("login", "shared").ExecuteResult(this.ControllerContext);
            }
        }
        
        public ActionResult Dashboard()
        {
            ViewBag.countRecrutements = context.Recrutements.Where(x => x.Statut == Statut.EnAttente).ToList().Count();
            ViewBag.countFormations = context.Formations.Where(x=>x.Statut==Statut.EnAttente).ToList().Count();
            ViewBag.countDemandeur =  context.Demandeurs.Count();

            return View();
        }

        public ActionResult HistoriqueDemandeRecrutement()
        {
            var user = Session["CurrentUser"] as Demandeur;

            return View(context.Recrutements.Where(x => x.Matricule_Createur.Equals(user.Empl_Matricule)).ToList());
        }

        public ActionResult NouvelleDemandeRecrutement()
        {
            var user = Session["CurrentUser"] as Demandeur;
            return View(user);
        }

        public ActionResult ListeDesDemandesDeRecrutement()
        {
            var user = Session["CurrentUser"] as Demandeur;
            return View(context.Recrutements.ToList());
        }
        public ActionResult ConsulterDemandesRecrutement(int id)
        {
            var Recrutement = context.Recrutements.Find(id);
            return View(Recrutement);
        }
        public ActionResult ConsulterDemandesFormation(int id)
        {
            var Formation = context.Formations.Find(id);
            return View(Formation);
        }
        public ActionResult HistoriqueDemandeFormation()
        {
            var user = Session["CurrentUser"] as Demandeur;

            return View(context.Formations.Where(x => x.Matricule_Createur.Equals(user.Empl_Matricule)).ToList());
        }
        public ActionResult ListeDesDemandesDeFormation()
        {
             var user = Session["CurrentUser"] as Demandeur;
            return View(context.Formations.ToList());
        }
        [HttpPost]
        public JsonResult AccepterDemande(int iddemande)
        {
            var Recrutementdb = context.Recrutements.Find(iddemande);
            Recrutementdb.Statut = Statut.Acceptè;
            var Demandeur = context.Demandeurs.Find(Recrutementdb.Matricule_Createur);
            if (Demandeur != null && !string.IsNullOrEmpty(Demandeur.Empl_Email))
            {
                Task.Run(() => Utilities.SendMail(Demandeur.Empl_Email, "Nouveau demande de recrutement ", $"Bonjour {Demandeur.Empl_Nom_Prenom },{Environment.NewLine} Votre demande de recrutement a été accepté  .{Environment.NewLine} Cordialement,{Environment.NewLine}Équipe Tis"));
            }

            context.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RefuserDemande(int iddemande, string causeRefus)
        {
            var Recrutementdb = context.Recrutements.Find(iddemande);
            Recrutementdb.Statut = Statut.Refusé;
            Recrutementdb.CauseRefus = causeRefus;
            var Demandeur = context.Demandeurs.Find(Recrutementdb.Matricule_Createur);
            if (Demandeur != null && !string.IsNullOrEmpty(Demandeur.Empl_Email))
            {
                Task.Run(() => Utilities.SendMail(Demandeur.Empl_Email, "Nouveau demande de recrutement ", $"Bonjour {Demandeur.Empl_Nom_Prenom },{Environment.NewLine} Votre demande de recrutement a été refusé a cause de.{Recrutementdb.CauseRefus }  .{Environment.NewLine} Cordialement,{Environment.NewLine}Équipe Tis"));
            }
            context.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AcccepterDemandeFormation(int Iddemande)
        {
            var Formationdb = context.Formations.Find(Iddemande);
            Formationdb.Statut = Statut.Acceptè;
            var Demandeur = context.Demandeurs.Find(Formationdb.Matricule_Createur);
            if (Demandeur != null && !string.IsNullOrEmpty(Demandeur.Empl_Email))
            {
                Task.Run(() => Utilities.SendMail(Demandeur.Empl_Email, "Nouveau demande de recrutement ", $"Bonjour {Demandeur.Empl_Nom_Prenom },{Environment.NewLine} Votre demande de recrutement a été accepté  .{Environment.NewLine} Cordialement,{Environment.NewLine}Équipe Tis"));
            }
            context.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RefuserDemandeFormation(int iddemande, string CauseRefusFormation)
        {
            var Formationdb = context.Formations.Find(iddemande);
            Formationdb.Statut = Statut.Refusé;
            Formationdb.CauseRefusFormation = CauseRefusFormation;
            var Demandeur = context.Demandeurs.Find(Formationdb.Matricule_Createur);
            if (Demandeur != null && !string.IsNullOrEmpty(Demandeur.Empl_Email))
            {
                Task.Run(() => Utilities.SendMail(Demandeur.Empl_Email, "Nouveau demande de recrutement ", $"Bonjour {Demandeur.Empl_Nom_Prenom },{Environment.NewLine} Votre demande de recrutement a été refusé a cause de .{Formationdb.CauseRefusFormation }  .{Environment.NewLine} Cordialement,{Environment.NewLine}Équipe Tis"));
            }
            context.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult NouvelleDemandeFormation()
        {
            var user = Session["CurrentUser"] as Demandeur;
            return View(user);
        }
        public ActionResult DetailsRec(int id)
        {
            var Recrutement = context.Recrutements.Find(id);
            return View(Recrutement);
        }
        public ActionResult DetailsForm(int id)
        {
            var Formation = context.Formations.Find(id);
            return View(Formation);
        }
        public ActionResult MonCompte()
        {
            var user = Session["CurrentUser"] as Demandeur;
            return View(user);
        }
        [HttpGet]
        public ActionResult Deconnexion()
        {
            Session.Abandon();
            return RedirectToAction("login", "shared");


        }

        [HttpPost]
        public ActionResult NouvelleDemandeRecrutement(Recrutement recrutement, HttpPostedFileBase Demande)
        {

            string Savepath = "";
            var user = Session["CurrentUser"] as Demandeur;
            recrutement.Matricule_Createur = user.Empl_Matricule;
            recrutement.Statut = Statut.EnAttente;
            recrutement.RoleCreateur = user.Permission.Role.Role1;


            var file = Request.Files[0];
            if (!string.IsNullOrEmpty(file.FileName))
            {
                var FileName = DateTime.Now.ToString("MMddyyyyHHmmss") + file.FileName.Replace("_", "").Replace(" ", "").Replace("+", "");
                var pathDirectory = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Demandes");
                bool IsExists = Directory.Exists(pathDirectory);
                if (!IsExists)
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                Savepath = Path.Combine(Server.MapPath("~/Demandes"), Path.GetFileName(FileName));
                file.SaveAs(Savepath);
                recrutement.FileName = FileName;
                recrutement.Savepath = Savepath;
            }
            context.Recrutements.Add(recrutement);
            context.SaveChanges();
            return View("HistoriqueDemandeRecrutement", context.Recrutements.Where(x => x.Matricule_Createur.Equals(user.Empl_Matricule)).ToList());
        }
        public JsonResult creerDemande(Formation formation)
        {
            var user = Session["CurrentUser"] as Demandeur;
            formation.Matricule_Createur = user.Empl_Matricule;
            formation.Statut = Statut.EnAttente;
            context.Formations.Add(formation);
            context.SaveChanges();




            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ModifierDemandeRecrutement(Recrutement recrutement, HttpPostedFileBase Demande)
        {
            var demandeDB = context.Recrutements.Find(recrutement.Id_demande);
            var user = Session["CurrentUser"] as Demandeur;

            demandeDB.NomPrenom = recrutement.NomPrenom;
            demandeDB.TitreRecrutement = recrutement.TitreRecrutement;
            demandeDB.Fonction_Demande = recrutement.Fonction_Demande;
            demandeDB.TypeRecrutement = recrutement.TitreRecrutement;
            demandeDB.Nb_poste = recrutement.Nb_poste;
            demandeDB.Diplome = recrutement.Diplome;
            demandeDB.Affectation = recrutement.Affectation;
            demandeDB.Remarque = recrutement.Remarque;
            demandeDB.Mission = recrutement.Mission;
            demandeDB.Contrat = recrutement.Contrat;
            demandeDB.NomCollaborateur = recrutement.NomCollaborateur;
            demandeDB.DateDemandeR = recrutement.DateDemandeR;
            demandeDB.Nb_experience = recrutement.Nb_experience;

            string Savepath = "";

            recrutement.Matricule_Createur = user.Empl_Matricule;
            recrutement.Statut = Statut.EnAttente;
            recrutement.RoleCreateur = user.Permission.Role.Role1;


            var file = Request.Files[0];
            if (!string.IsNullOrEmpty(file.FileName))
            {
                var FileName = DateTime.Now.ToString("MMddyyyyHHmmss") + file.FileName.Replace("_", "").Replace(" ", "").Replace("+", "");
                var pathDirectory = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Demandes");
                bool IsExists = Directory.Exists(pathDirectory);
                if (!IsExists)
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                Savepath = Path.Combine(Server.MapPath("~/Demandes"), Path.GetFileName(FileName));
                file.SaveAs(Savepath);
                recrutement.FileName = FileName;
                recrutement.Savepath = Savepath;
            }

            context.SaveChanges();
            return View("HistoriqueDemandeRecrutement", context.Recrutements.Where(x => x.Matricule_Createur.Equals(user.Empl_Matricule)).ToList());
        }
        [HttpPost]
        public JsonResult ModifierDemandeFormation(Formation formation)
        {
            var demandeDB = context.Formations.Find(formation.Id_demande);
            var user = Session["CurrentUser"] as Demandeur;

            demandeDB.NomPrenom = formation.NomPrenom;
            demandeDB.IntituleFormation = formation.IntituleFormation;
            demandeDB.Objectif = formation.Objectif;
            demandeDB.Priorite = formation.Priorite;
            demandeDB.typeFormation = formation.typeFormation;
            demandeDB.Service = formation.Service;


            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);




        }
        [HttpPost]
        public JsonResult DeleteDemande(int idRecrutement)
        {
            var demandeDB = context.Demandes.Find(idRecrutement);
            context.Demandes.Remove(demandeDB);
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);


        }
        public ActionResult Chat()
        {

            var user = Session["CurrentUser"] as Demandeur;
            return View(context.Demandeurs.ToList());

        }

        [HttpPost]
        public JsonResult GetDataMessages(string matricule_user)
        {

            // Initialization.  
            JsonResult result = new JsonResult();


            // var data = context.Message.ToList();

            var data = context.Message.Where(d => d.id_receiver == matricule_user || d.id_sender == matricule_user).ToList();

            result = Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);

            return result;

        }

        [HttpPost]
        public JsonResult newMessage(Message message, string content, string receiver)
        {

            var user = Session["CurrentUser"] as Demandeur;

            message.id_receiver = receiver;

            message.id_sender = user.Empl_Matricule;

            message.contenu = content;

            context.Message.Add(message);

            context.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult DeleteDemandeFormation(int IdFormation)
        {
            var demandeDB = context.Demandes.Find(IdFormation);
            context.Demandes.Remove(demandeDB);
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);


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


    }
}