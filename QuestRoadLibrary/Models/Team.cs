using System;
using System.Collections.Generic;

#nullable disable

namespace QuestRoadLibrary
{
    public partial class Team
    {
        public Team()
        {
            Bookings = new HashSet<Booking>();
            Members = new HashSet<Member>();
        }

        public int TeamId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
