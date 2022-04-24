

namespace HR_Rerutement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class Formation :  Demande
    {
        public Formation()
        {
            DateFormation = DateTime.Now;
        }

      
        public string IntituleFormation { get; set; }
        public string Objectif { get; set; }
        public string Priorite { get; set; }

        public string typeFormation { get; set; }
        public string Service { get; set; }     
 
        [Required]
        public DateTime DateFormation { get; set; }
        public string CauseRefusFormation { get; set; }


    }
}
