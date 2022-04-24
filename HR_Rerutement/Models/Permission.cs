
namespace HR_Rerutement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Permission
    {       
        
         [Key]
        public int Id_compte { get; set; }
        public string Empl_Matricule { get; set; }      
        public string Password { get; set; }

        public virtual ICollection<Demande> Demande { get; set; }
    
        public virtual Role Role { get; set; }

    }
}
