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
		public void AddPlayer(Player newPlayer)
		{
			context.Player
				.Add(newPlayer);
			context.SaveChanges();
		}

		public void RemovePlayer(int id)
		{
			var playerToRemove = GetPlayerById(id);

			var match = context.Match
				.Where(m => m.Player1Id == id || m.Player2Id == id)
				.Select(m => m.Id)
				.ToArray();

			foreach (var item in match)
			{
				RemoveMatch(item);
			}

			context.Player
				.Remove(playerToRemove);
			context.SaveChanges();
		}

		public void AddTeam(string TeamName)
		{
			context.Team
				.Add(new Team
				{
					ClassName = TeamName
				});
			context.SaveChanges();
		}

		public void AddMatch(Match newMatch)
		{
			int ratingChange = 0;

			if (newMatch.Player1Sets > newMatch.Player2Sets)
			{
				ratingChange = UppdatePlayers(newMatch.Player1Id, newMatch.Player2Id, newMatch.Player1Sets, newMatch.Player2Sets);
				newMatch.Player1RatingChange = ratingChange;
				newMatch.Player2RatingChange = -ratingChange;
			}
			else
			{
				ratingChange = UppdatePlayers(newMatch.Player2Id, newMatch.Player1Id, newMatch.Player2Sets, newMatch.Player1Sets);
				newMatch.Player2RatingChange = ratingChange;
				newMatch.Player1RatingChange = -ratingChange;
			}

			context.Match
				.Add(newMatch);
			context.SaveChanges();
		}

		public int UppdatePlayers(int winner, int loser, int winnerSets, int loserSets)
		{
			var winningPlayer = context.Player
				.Single(p => p.Id == winner);

			var losingPlayer = context.Player
				.Single(p => p.Id == loser);

			winningPlayer.MatchesWon++;
			winningPlayer.MatchesPlayed++;
			winningPlayer.SetsWon += winnerSets;
			winningPlayer.SetsLost += loserSets;
			winningPlayer.SetDifference += (winnerSets - loserSets);

			losingPlayer.MatchesLost++;
			losingPlayer.MatchesPlayed++;
			losingPlayer.SetsWon += loserSets;
			losingPlayer.SetsLost += winnerSets;
			losingPlayer.SetDifference += (loserSets - winnerSets);



			double ratingWinner = Math.Pow(10, Convert.ToDouble(winningPlayer.Elo) / 400);
			double ratingLoser = Math.Pow(10, Convert.ToDouble(losingPlayer.Elo) / 400);

			double rateChange = 1 - ratingWinner / (ratingWinner + ratingLoser);

			int weight = 10 * winnerSets;

			winningPlayer.Elo += Convert.ToInt32(weight * rateChange) + 1;
			losingPlayer.Elo -= Convert.ToInt32(weight * rateChange) - 1;

			context.SaveChanges();

			return Convert.ToInt32(weight * rateChange);
		}

		public void RemoveMatch(int id)
		{
			var match = context.Match
				.SingleOrDefault(m => m.Id == id);

			var player1 = context.Player
				.SingleOrDefault(p => p.Id == match.Player1Id);

			var player2 = context.Player
				.SingleOrDefault(p => p.Id == match.Player2Id);

			player1.MatchesPlayed--;
			player2.MatchesPlayed--;

			player1.SetsWon -= match.Player1Sets;
			player1.SetsLost -= match.Player2Sets;

			player2.SetsWon -= match.Player2Sets;
			player2.SetsLost -= match.Player1Sets;

			player1.SetDifference -= (match.Player1Sets - match.Player2Sets);
			player2.SetDifference -= (match.Player2Sets - match.Player1Sets);

			player1.Elo -= match.Player1RatingChange +1;
			player2.Elo -= match.Player2RatingChange +1;

			if (match.Player1Sets > match.Player2Sets)
			{
				player1.MatchesWon--;
				player2.MatchesLost--;
			}
			else
			{
				player2.MatchesWon--;
				player1.MatchesLost--;
			}

			context.Match
				.Remove(match);

			context.SaveChanges();

		}

		internal bool RemoveTeam(int teamToRemove)
		{
			var team = context.Team
				.Single(t => t.Id == teamToRemove);

			var hej = context.Player
				.FirstOrDefault(p => p.TeamId == teamToRemove);

			if (hej == null)
			{
				context.Team
					.Remove(team);
				context.SaveChanges();
				return true;
			}
			else
				return false;
		}
	}
}
