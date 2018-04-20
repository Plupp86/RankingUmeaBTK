using System;
using System.Collections.Generic;

namespace UmeaBTKRanking.Models.Entities
{
    public partial class Team
    {
        public Team()
        {
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string ClassName { get; set; }

        public ICollection<Player> Player { get; set; }
    }
}
