using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using UmeaBTKRanking.Controllers;
using UmeaBTKRanking.Models;
using UmeaBTKRanking.Models.Entities;
using UmeaBTKRanking.Models.ViewModels;

namespace UmeaBTKRankingTest
{
	[TestClass]
	public class ApiControllerTest

	{

		[TestMethod]
		public void TestGetPlayers()
		{
			//Arrange
			var apiHandler = new Mock<RepositoryHandlerApi>();
			apiHandler.CallBase = true;
			Player[] players = new Player[3];
			players[0] = new Player() { Name = "Kalle", TeamId = 3 };
			players[1] = new Player() { Name = "Pelle", TeamId = 3 };
			players[2] = new Player() { Name = "Jocke", TeamId = 3 };

			string json = JsonConvert.SerializeObject(players);

			apiHandler.Setup(m => m.GetLeagueTable()).Returns(json);
			//Act
			var controller = new ApiController(apiHandler.Object);
			var result = controller.Players() as JsonResult;

			//Assert
			Assert.AreEqual(json, result.Value);
		}
	}
}
