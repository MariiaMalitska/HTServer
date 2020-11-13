using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class ItemState
    {
        public ItemState()
        {
            UserItemStates = new HashSet<UserItemState>();
        }

        public int ItemStateId { get; set; }

        [Required]
        public string ItemStateName { get; set; }

        public virtual ICollection<UserItemState> UserItemStates { get; set; }
    }
}
