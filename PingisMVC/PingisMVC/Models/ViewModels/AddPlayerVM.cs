using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UmeaBTKRanking.Models.ViewModels
{
    public class AddPlayerVM
    {
		[Display(Name = "Name: ")]
		public string Name { get; set; }

		[Display(Name = "Team: ")]
		public SelectListItem[] TeamDropList { get; set; }

		public int TeamId { get; set; }
	}
}
