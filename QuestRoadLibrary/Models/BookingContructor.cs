using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestRoadLibrary.Models
{
    public class BookingContructor
    {
        public int QuestId { get; set; }
        public string TeamName { get; set; }
        public int CountOfUsers { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
