using UmeaBTKRanking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmeaBTKRanking.Models.ViewModels
{
    public class LeagueTableVM
    {
		public Player[] Players { get; set; }
		public int Counter { get; set; }
	}
}
