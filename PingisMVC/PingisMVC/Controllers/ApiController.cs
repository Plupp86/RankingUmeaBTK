using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UmeaBTKRanking.Models;
using UmeaBTKRanking.Models.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UmeaBTKRanking.Controllers
{
    public class ApiController : Controller
    {

		public RepositoryHandlerApi apiHandler;

		public ApiController(RepositoryHandlerApi apiHandler)
		{
			this.apiHandler = apiHandler;
		}
		// GET: /<controller>/
		public IActionResult Players()
        {
			var players = apiHandler.GetLeagueTable();
            return Json(players);
        }

		[Route("api/matches/")]
		public IActionResult Matches()
		{
			var matches = apiHandler.GetRecentMAtches();
			return Json(matches);
		}

		[Route("api/matches/{id}")]
		public IActionResult Matches(int id)
		{
			var matches = apiHandler.GetMatchesById(id);
			return Json(matches);
		}

		[Route("api/AddPlayer/")]
		[HttpPost]
		public IActionResult AddPlayer([FromBody]Player player)
		{
			apiHandler.AddPlayer(player);
			return Json("ok");
		}

		[Route("api/AddMatch/")]
		[HttpPost]
		public IActionResult AddPlayer([FromBody]Match newMatch)
		{
			apiHandler.AddMatch(newMatch);
			return Json("ok");
		}
	}
}
