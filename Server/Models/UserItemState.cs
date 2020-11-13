using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class UserItemState
    {
        public int UserId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int ItemStateId { get; set; }

        public virtual Item Item { get; set; }
        public virtual ItemState ItemState { get; set; }
        public virtual User User { get; set; }
    }
}
