using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class GoogleID
    {
        public int UserId { get; set; }

        [Required]
        public string GoogleId { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual User User { get; set; }
    }
}
