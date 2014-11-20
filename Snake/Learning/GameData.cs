using System;
using System.Collections.Generic;

namespace Snake
{
	public class GameData
	{
		List <snakeparts> snakeBody = new List<snakeparts> ();
		snakeparts firstPart = new snakeparts ();
		snakeparts part = new snakeparts();

		struct snakeparts
		{
			public int cordX;
			public int cordY;
		}

		public GameData ()
		{

		}
		private void move ()
		{
			int Lastpart;
			Lastpart = snakeBody.FindLast (part);
			snakeBody.RemoveAt (Lastpart);
			snakeBody.Insert (1, firstPart);
		}
	}
}

