using System;
using System.Collections.Generic;

namespace UmeaBTKRanking.Models.Entities
{
    public partial class Player
    {
        public Player()
        {
            MatchPlayer1 = new HashSet<Match>();
            MatchPlayer2 = new HashSet<Match>();
        }

		public Player(string name, int teamId)
		{
			Name = name;
			TeamId = teamId;
		}

        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesLost { get; set; }
        public int SetsWon { get; set; }
        public int SetsLost { get; set; }
        public int SetDifference { get; set; }
        public int? Elo { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
        public ICollection<Match> MatchPlayer1 { get; set; }
        public ICollection<Match> MatchPlayer2 { get; set; }
    }
}
