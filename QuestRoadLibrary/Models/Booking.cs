using System;
using System.Collections.Generic;

#nullable disable

namespace QuestRoadLibrary
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int QuestId { get; set; }
        public int TeamId { get; set; }
        public DateTime Time { get; set; } 
        public string Description { get; set; }
        public int Price { get; set; }

        public virtual Quest Quest { get; set; }
        public virtual Team Team { get; set; }
    }
}
