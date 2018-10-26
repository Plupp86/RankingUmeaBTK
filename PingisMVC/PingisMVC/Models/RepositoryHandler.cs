using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmeaBTKRanking.Models.Entities;
using UmeaBTKRanking.Models.ViewModels;

namespace UmeaBTKRanking.Models
{
	public class RepositoryHandler
	{
		private readonly Repository rep;// = new Repository();'

		public RepositoryHandler()
		{

		}

		public RepositoryHandler(Repository rep)
		{
			this.rep = rep;
		}


		public virtual LeagueTableVM GetLeagueTable()
		{
			return rep.GetLeagueTable();
		}

		public virtual bool isAdmin()
		{
			//var userName = HttpContext.User.Identity.Name;
			string userName = "Admin";

			if (userName == "Admin")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual AllMatchesVM AllMatches()
		{

			var model = new AllMatchesVM()
			{
				recentMatches = rep.GetRecentMatches()
			};

			return model;
		}

		internal PlayerStatsVM PlayerStats(int id)
		{
			return new PlayerStatsVM()
			{
				recentMatches = rep.GetMatchesById(id)
			};
		}

		internal AddPlayerVM AddPlayerVM()
		{
			var teams = rep.GetTeams();
			var model = new AddPlayerVM
			{
				TeamDropList = new SelectListItem[teams.Length]
			};

			for (int i = 0; i < teams.Length; i++)
			{
				model.TeamDropList[i] = new SelectListItem { Value = teams[i].Id.ToString(), Text = teams[i].ClassName };
			}

			return model;
		}

		internal void AddPlayer(AddPlayerVM model)
		{
			model.TeamId = 3;
			rep.AddPlayer(new Player(model.Name, model.TeamId));
		}

		public virtual AddMatchVM PopulateLists()
		{
			return rep.PopulateLists();
		}

		public virtual void NewMatch(AddMatchVM model)
		{
			Match newMatch = new Match()
			{
				Player1Id = model.SelectedPlayer1Id,
				Player2Id = model.SelectedPlayer2Id,
				Player1Sets = model.SelectedPlayer1Sets,
				Player2Sets = model.SelectedPlayer2Sets,
				Date = DateTime.Now
			};

			rep.AddMatch(newMatch);
		}
	}
}
