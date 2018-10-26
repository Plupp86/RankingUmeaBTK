using System;
using System.Collections.Generic;
using System.Text;
using UmeaBTKRanking.Models.Entities;

namespace UmeaBTKRankingTest
{
    class MockContext
    {
		public List<Player> Player { get; set; }

		MockContext()
		{
			Player = new List<Player>
			{
				new Player("Player1",1),
				new Player("Player2",2)
			};
		}
	}
}
