//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UmeaBTKRanking.Models.Entities;

//namespace UmeaBTKRankingTest
//{
//    class MockPlayer : DbSet<Player>
//    {
//		public MockPlayer()
//		{
			
//			var data = new List<Player>
//			{
//				new Player { Id = 1, Name = "BBB", TeamId = 1 },
//				new Player { Id = 2, Name = "ZZZ", TeamId = 1 },
//				new Player { Id = 3, Name = "AAA", TeamId = 1 },
//			}.AsQueryable();

//			MockPlayer mockSet = new MockPlayer();

//			mockSet.As<IQueryable<Player>>().Setup(m => m.Provider).Returns(data.Provider);
//			mockSet.As<IQueryable<Player>>().Setup(m => m.Expression).Returns(data.Expression);
//			mockSet.As<IQueryable<Player>>().Setup(m => m.ElementType).Returns(data.ElementType);
//			mockSet.As<IQueryable<Player>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

//		}
//	}
//}
