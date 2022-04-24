

namespace HR_Rerutement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Demandeur
    {
       
       

        [Key]

        public string Empl_Matricule { get; set; }
        public string Empl_Nom_Prenom { get; set; }
        public string Empl_MatResponsable { get; set; }
        public string Empl_NumPro { get; set; }
        public string Empl_Fonction { get; set; }
        public string Empl_CentreCout { get; set; }
        public string Empl_Service { get; set; }
        public Nullable<bool> Empl_Active { get; set; }
        public string Empl_Email { get; set; }

        public string image { get; set; }
        public string PathImage { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
