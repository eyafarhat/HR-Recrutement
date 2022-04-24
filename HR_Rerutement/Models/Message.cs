using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_Rerutement.Models
{
    public class Message
    {
        public Message()
        {
            date_create = DateTime.Now;
        }


        [Key]
        public int id_mess { get; set; }

        public string id_receiver { get; set; }

        public string id_sender { get; set; }

        public string contenu { get; set; }

        public string is_read { get; set; }

        public DateTime date_create { get; set; }

    }


}