using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UmeaBTKRanking.Models.Entities
{
    public partial class Match
    {
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Player1Sets { get; set; }
        public int Player2Sets { get; set; }
        public DateTime Date { get; set; }
		public int Player1RatingChange { get; set; }
		public int Player2RatingChange { get; set; }

		public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }
}
