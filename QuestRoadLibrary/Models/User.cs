using System;
using System.Collections.Generic;

#nullable disable

namespace QuestRoadLibrary
{
    public partial class User
    {
        public User()
        {
            Members = new HashSet<Member>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public int? CompanyId { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
