using System;
using System.Collections.Generic;

#nullable disable

namespace QuestRoadLibrary
{
    public partial class Company
    {
        public Company()
        {
            Quests = new HashSet<Quest>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string CompanyCheck { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyAccount { get; set; }
        public bool? IsConfirmed { get; set; }
        public string Adress { get; set; }

        public virtual ICollection<Quest> Quests { get; set; }
    }
}
