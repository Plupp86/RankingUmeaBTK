using UmeaBTKRanking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmeaBTKRanking.Models.ViewModels
{
    public class PlayedMatch
    {
		public int MatchID { get; set; }
		public string PlayerOne { get; set; }
		public string PlayerTwo { get; set; }
		public int SetsOne { get; set; }
		public int SetsTwo { get; set; }
		public DateTime DatePlayed { get; set; }
	}
}
