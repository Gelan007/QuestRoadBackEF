using System;
using System.Collections.Generic;

#nullable disable

namespace QuestRoadLibrary
{
    public partial class Member
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime? WhenAssigned { get; set; }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
