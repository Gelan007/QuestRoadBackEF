using System;
using System.Collections.Generic;

#nullable disable

namespace QuestRoadLibrary
{
    public partial class Quest
    {
        public Quest()
        {
            Bookings = new HashSet<Booking>();
        }

        public int QuestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DifficultyLevel { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Category { get; set; }
        public string Actors { get; set; }
        public int CompanyId { get; set; }
        public int? MaxCountUsers { get; set; }
        public int? Price { get; set; }
        public string Photo { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
