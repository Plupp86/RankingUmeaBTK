using Microsoft.AspNetCore.Mvc.Rendering;
using UmeaBTKRanking.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UmeaBTKRanking.Models.ViewModels
{
    public class AdminVM
    {

		[Display(Name = "Team")]
		public SelectListItem[] TeamDropList { get; set; }


		[Display(Name = "Player")]
		public SelectListItem[] PlayerDropList { get; set; }

		public string NewTeam { get; set; }

		public int PlayerToRemove { get; set; }
		public int TeamToRemove { get; set; }

		public string UserName { get; set; }
	}
}
