
namespace HR_Rerutement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Config
    {
        [Key]
        public int key { get; set; }
        public Nullable<int> val { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public Nullable<int> IsActive { get; set; }
    }
}
