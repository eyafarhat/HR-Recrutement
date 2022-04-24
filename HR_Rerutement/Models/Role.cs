

namespace HR_Rerutement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Role
    {
       
        [Key]
        public int Id_Role { get; set; }
        public string Role1 { get; set; }
        public string desc { get; set; }
    
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
