using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UmeaBTKRanking.Models;
using UmeaBTKRanking.Models.Entities;
using UmeaBTKRanking.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UmeaBTKRanking.Controllers
{
    public class HomeController : Controller
    {
		public RepositoryHandler repHandler;

		public HomeController(RepositoryHandler repHandler)
		{
			this.repHandler = repHandler;
		}


		// GET: /<controller>/
		public IActionResult Index()
        {
			var model = repHandler.GetLeagueTable();
            return View("Index",model);
        }

		public IActionResult RecentGames()
		{
			if (repHandler.isAdmin())
			{
				//return View("Admin");
				return RedirectToAction(nameof(AdminController.RecentGames), "Admin");
			}
			else
			{
				return View(repHandler.AllMatches());
			}
		}

		public IActionResult PlayerStats(int id)
		{
			return PartialView("PlayStats", repHandler.PlayerStats(id));

		}

		[HttpGet]
		public IActionResult AddPlayer()
		{
			return View(repHandler.AddPlayerVM());
		}

		[HttpPost]
		public IActionResult AddPlayer(AddPlayerVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			else
			{
				repHandler.AddPlayer(model);
				return RedirectToAction(nameof(Index));
			}
	
		}	

		[HttpGet]
		public IActionResult AddMatch()
		{
			return View(repHandler.PopulateLists());
		}

		[HttpPost]
		public IActionResult AddMatch(AddMatchVM model)
		{
			if (model.SelectedPlayer1Id == model.SelectedPlayer2Id)
			{
				ModelState.AddModelError("SelectedPlayer2Id", "Välj två olika spelare!");
			}

			if (model.SelectedPlayer1Sets == model.SelectedPlayer2Sets)
			{
				ModelState.AddModelError("SelectedPlayer2Sets", "Felaktigt resultat! Matcher kan inte sluta lika!.");
			}

			if (!ModelState.IsValid)
			{
				return View(repHandler.PopulateLists());
			}

			repHandler.NewMatch(model);

			return RedirectToAction(nameof(Index));
		}
	}
}
