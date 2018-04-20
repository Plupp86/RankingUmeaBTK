using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UmeaBTKRanking.Models.Entities;
using UmeaBTKRanking.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static UmeaBTKRanking.Models.ViewModels.PlayedMatch;

namespace UmeaBTKRanking.Models
{
	public partial class Repository
	{
		private readonly PingisContext context;

		public Repository(PingisContext context)
		{
			this.context = context;
		}

		public LeagueTableVM GetLeagueTable()
		{
			return new LeagueTableVM()
			{
				Players = context.Player
					.OrderByDescending(p => p.Elo)
					.ThenByDescending(p => p.MatchesWon)
					.ToArray()
			};
		}

		public Player GetPlayerById(int id)
		{
			return context.Player
				.Single(p => p.Id == id);
		}

		public PlayedMatch[] GetRecentMatches()
		{
			return context.Match
				.Include(m => m.Player1)
				.Include(m => m.Player2)
				.OrderByDescending(m => m.Date)
				.Select(m => new PlayedMatch
				{
					PlayerOne = m.Player1.Name,
					PlayerTwo = m.Player2.Name,
					SetsOne = m.Player1Sets,
					SetsTwo = m.Player2Sets,
					DatePlayed = m.Date,
					MatchID = m.Id

				})
				.Take(20)
				.ToArray();
		}

		public PlayedMatch[] GetMatchesById(int id) => context.Match
				.Include(m => m.Player1)
				.Include(m => m.Player2)
				.OrderByDescending(m => m.Date)
				.Where(m => m.Player1Id == id || m.Player2Id == id)
				.Select(m => new PlayedMatch
				{
					PlayerOne = m.Player1.Name,
					PlayerTwo = m.Player2.Name,
					SetsOne = m.Player1Sets,
					SetsTwo = m.Player2Sets,
					DatePlayed = m.Date
				})
				.Take(5)
				.ToArray();

		public Team[] GetTeams() => context.Team
				.ToArray();

		public Player[] GetPlayers()
		{
			return context.Player
				.OrderBy(p => p.TeamId)
				.ThenBy(p => p.Name)
				.ToArray();
		}

		public string GetTeamName(int id) => context.Team
				.Single(t => t.Id == id)
				.ClassName;

		public AddMatchVM PopulateLists()
		{
			var players = GetPlayers();
			var model = new AddMatchVM()
			{
				Sets = new SelectListItem[4],
				ListOfPlayers = new SelectListItem[players.Length]
			};

			for (int i = 0; i < players.Length; i++)
			{
				var teamname = GetTeamName(players[i].TeamId);
				var text = $"{players[i].Name}";
				model.ListOfPlayers[i] = new SelectListItem { Value = players[i].Id.ToString(), Text = text };
			}

			model.Sets[0] = new SelectListItem { Value = "0", Text = "0" };
			model.Sets[1] = new SelectListItem { Value = "1", Text = "1" };
			model.Sets[2] = new SelectListItem { Value = "2", Text = "2" };
			model.Sets[3] = new SelectListItem { Value = "3", Text = "3" };

			return model;
		}

	}
}
