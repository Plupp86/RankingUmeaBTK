using Microsoft.AspNetCore.Mvc.Rendering;
using UmeaBTKRanking.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UmeaBTKRanking.Models.ViewModels
{
    public class AddMatchVM
    {
		//public AddMatchVM()
		//{
		//	SelectListItem[] Sets = new SelectListItem[3];
		//	Sets[0] = new SelectListItem { Value = "1", Text = "1" };
		//	Sets[1] = new SelectListItem { Value = "2", Text = "2" };
		//	Sets[2] = new SelectListItem { Value = "3", Text = "3" };
		//}

		[Display(Name = "Player")]
		public SelectListItem[] ListOfPlayers { get; set; }

		public SelectListItem[] Sets { get; set; }

		[Display(Name ="Player 1: ")]
		public int SelectedPlayer1Id { get; set; }

		[Display(Name = "Player 2: ")]
		public int SelectedPlayer2Id { get; set; }

		[Display(Name = "Score: ")]
		public int SelectedPlayer1Sets { get; set; }

		[Display(Name = "Score: ")]
		public int SelectedPlayer2Sets { get; set; }
	}
}
