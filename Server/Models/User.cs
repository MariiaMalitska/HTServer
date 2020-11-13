using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class User
    {
        public User()
        {
            UserItemStates = new HashSet<UserItemState>();
            UserItems = new HashSet<UserItem>();
        }

        public int Uid { get; set; }
        public int Crystals { get; set; }
        public int Coins { get; set; }
        public int TutorialParts { get; set; }
        public int UserLevel { get; set; }
        public int CharSkinId { get; set; }
        public string DeviceId { get; set; }

        public virtual GoogleID GoogleId { get; set; }
        public virtual ICollection<UserItemState> UserItemStates { get; set; }
        public virtual ICollection<UserItem> UserItems { get; set; }
    }
}
