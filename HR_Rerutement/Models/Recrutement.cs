
namespace HR_Rerutement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class Recrutement : Demande
    {
     
      public Recrutement()
        {
            DateDemandeR = DateTime.Now;
          

        }    
     
        public string Fonction_Demande { get; set; }
       
        public DateTime DateDemandeR { get; set; }
        public string TypeRecrutement { get; set; }
        public string TitreRecrutement { get; set; }
        public int Nb_poste { get; set; } 
        public string Mission { get; set; }
        public string Contrat { get; set; }
        public string Diplome { get; set; }
        public string Nb_experience { get; set; }
        public string Remarque { get; set; }
        public string Affectation { get; set; }
        public string Savepath { get; set; }

        public string NomCollaborateur { get; set; }
        public string FileName { get; set; }
        public string CauseRefus { get; set; }


    }
}
