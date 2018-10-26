using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
	public class HomeControllerTest
	{

		private LeagueTableVM MockedLeagueTable()
		{
			var model = new LeagueTableVM();
			var list = new List<Player>();
			list.Add(new Player
			{
				Id = 1,
				Name = "AAA"
			});
			list.Add(new Player
			{
				Id = 2,
				Name = "ZZZ"
			});
			list.Add(new Player
			{
				Id = 3,
				Name = "BBB"
			});
			model.Players = list.ToArray();

			return model;
		}

		private AllMatchesVM MockMatches()
		{
			return new AllMatchesVM();
		}

		[TestMethod]
		public void TestIndexView()
		{
			//Arrange
			var repositoryHandler = new Mock<RepositoryHandler>();

			//Act
			var controller = new HomeController(repositoryHandler.Object);
			var result = controller.Index() as ViewResult;

			//Assert
			Assert.AreEqual("Index", result.ViewName);
		}


		[TestMethod]
		public void TestLeagueTable()
		{
			//Arrange
			var repositoryHandler = new Mock<RepositoryHandler>();
			repositoryHandler.CallBase = true;
			repositoryHandler.Setup(m => m.GetLeagueTable()).Returns(MockedLeagueTable());

			//Act
			var controller = new HomeController(repositoryHandler.Object);
			var result = controller.Index() as ViewResult;
			var model = result.Model as LeagueTableVM;

			//Assert
			Assert.AreEqual(3, model.Players.Count());
			Assert.AreEqual("AAA", model.Players[0].Name);
			Assert.AreEqual("ZZZ", model.Players[1].Name);
			Assert.AreEqual("BBB", model.Players[2].Name);
		}

		[TestMethod]
		public void TestForAdmin()
		{
			//Arrange
			var repositoryHandler = new Mock<RepositoryHandler>();
			repositoryHandler.CallBase = true;
			repositoryHandler.Setup(m => m.isAdmin()).Returns(true);
			repositoryHandler.Setup(m => m.AllMatches()).Returns(MockMatches());

			//Act
			var controller = new HomeController(repositoryHandler.Object);
			var result = controller.RecentGames() as ViewResult;

			//Assert
			Assert.AreEqual("Admin", result.ViewName);

		}

		[TestMethod]
		public void TestForNormalUser()
		{
			//Arrange
			var repositoryHandler = new Mock<RepositoryHandler>();
			repositoryHandler.CallBase = true;
			repositoryHandler.Setup(m => m.isAdmin()).Returns(false);
			repositoryHandler.Setup(m => m.AllMatches()).Returns(MockMatches());

			//Act
			var controller = new HomeController(repositoryHandler.Object);
			var result = controller.RecentGames() as ViewResult;

			//Assert
			Assert.AreEqual("RecentGames", result.ViewName);
		}

		[TestMethod]
		public void AddMatchTest()
		{
			//Arrange
			AddMatchVM model = new AddMatchVM();
			var repositoryHandler = new Mock<RepositoryHandler>();
			repositoryHandler.CallBase = true;
			repositoryHandler.Setup(m => m.isAdmin()).Returns(false);
			repositoryHandler.Setup(m => m.PopulateLists()).Returns(new AddMatchVM());
			repositoryHandler.Setup(m => m.NewMatch(model));

			//Act
			var controller = new HomeController(repositoryHandler.Object);
			var result = controller.AddMatch(model) as RedirectToActionResult;

			//Assert
			Assert.AreEqual("Index", result.ActionName);
		}
	}
}
