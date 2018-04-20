using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UmeaBTKRanking.Models;
using UmeaBTKRanking.Models.ModelViews;
using UmeaBTKRanking.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UmeaBTKRanking.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly Repository rep;
		private readonly AccountRepository accRep;

		
		public AdminController(Repository rep, AccountRepository accRep)
		{
			this.rep = rep;
			this.accRep = accRep;
		}
		// GET: /<controller>/

		//[AllowAnonymous]
		//[HttpGet]
		//public async Task<IActionResult> Create()
		//{
		//	var (res, succ) = await accRep.CreateUserAsync();
		//	return RedirectToAction(nameof(AdminController.Login));
		//}

		[HttpGet]
		public IActionResult Index()
		{
			var players = rep.GetPlayers();
			var model = new AdminVM()
			{
				PlayerDropList = new SelectListItem[players.Length]
			};

			for (int i = 0; i < players.Length; i++)
			{
				var teamname = rep.GetTeamName(players[i].TeamId);
				var text = $"{teamname} - {players[i].Name}";
				model.PlayerDropList[i] = new SelectListItem { Value = players[i].Id.ToString(), Text = text };
			}


			var teams = rep.GetTeams();
			model.TeamDropList = new SelectListItem[teams.Length];

			for (int i = 0; i < teams.Length; i++)
			{
				model.TeamDropList[i] = new SelectListItem { Value = teams[i].Id.ToString(), Text = teams[i].ClassName };
			}

			model.UserName = User.Identity.Name;

			return View(model);
		}

		[HttpPost]
		public IActionResult RemovePlayer(AdminVM model)
		{
			rep.RemovePlayer(model.PlayerToRemove);
			return RedirectToAction(nameof(AdminController.Index));
		}

		[HttpPost]
		public IActionResult RemoveTeam(AdminVM model)
		{
			var success = rep.RemoveTeam(model.TeamToRemove);
			if (success)
				return RedirectToAction(nameof(AdminController.Index));
			else
				return RedirectToAction(nameof(AdminController.TeamNotEmpty));
		}

		[HttpGet]
		public IActionResult TeamNotEmpty()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddTeam(AdminVM model)
		{
			rep.AddTeam(model.NewTeam);
			return RedirectToAction(nameof(AdminController.Index));
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login(LoginVM model)
		{
			var result = accRep.ValidateUser(model);
			if (result.Result.Succeeded)
			{
				return RedirectToAction(nameof(AdminController.Index));
			}
			else
				return View(model);
		}

		public IActionResult RecentGames()
		{
			return View(new AllMatchesVM()
			{
				recentMatches = rep.GetRecentMatches()
			});
		}

		public IActionResult RemoveMatch(int id)
		{
			rep.RemoveMatch(id);
			return RedirectToAction(nameof(AdminController.RecentGames));
		}

		[HttpGet]
		public IActionResult Logout()
		{
			accRep.SignOutUser();
			return RedirectToAction(nameof(HomeController.Index),"Home");
		}
	}
	
}
