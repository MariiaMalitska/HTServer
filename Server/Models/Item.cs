using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class Item
    {
        public Item()
        {
            UserItemStates = new HashSet<UserItemState>();
            UserItems = new HashSet<UserItem>();
        }

        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public virtual ICollection<UserItemState> UserItemStates { get; set; }
        public virtual ICollection<UserItem> UserItems { get; set; }
    }
}
