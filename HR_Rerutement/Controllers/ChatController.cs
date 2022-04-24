using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using HR_Rerutement.Models;

namespace HR_Rerutement.Controllers
{
    public class ChatController : Controller
    {

        private ProjectContext context;

        public ChatController()
        {

            context = new ProjectContext();
        }

        [HttpGet]
        public JsonResult getusers()
        {

            var user = Session["CurrentUser"] as Demandeur;
            var Demandeur = context.Demandeurs.ToList();
            return Json("Success", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult getMessages()
        {

            var user = Session["CurrentUser"] as Demandeur;
            var Demandeur = context.Demandeurs.ToList();

            //var demandeDB = context.Message.Where(d => d.id_receiver == user.Empl_Matricule || d.id_sender == user.Empl_Matricule).ToList();
            return Json(new { success = true, oldval = Demandeur },
                 JsonRequestBehavior.AllowGet);


        }
    }
}