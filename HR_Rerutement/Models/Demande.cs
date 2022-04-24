using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HR_Rerutement.Models
{
  

    public  class Demande
    {
        public Demande()
        {
            Date_Creation = DateTime.Now;
        }
        [Key]
        public int Id_demande { get; set; }       
      
        public DateTime Date_Creation { get; set; }

        public string NomPrenom { get; set; }
        public Statut Statut { get; set; }
        public string RoleCreateur { get; set; }

       public string Matricule_Createur { get; set; }
        
     
    }
}
