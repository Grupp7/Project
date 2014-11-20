using System;
using System.Collections.Generic;

namespace Snake
{
	public class Snake
	{
		Userinput userInput;

		public Snake ()
		{userInput = new Userinput ();
			Main ();
		}

		int X = userInput.x;
		int Y = userInput.y;

		struct snakeparts
		{
			public int cordX;
			public int cordY;
		}


		List <snakeparts> snakeBody = new List<snakeparts> ();
		snakeparts firstPart = new snakeparts ();
		snakeparts part = new snakeparts();



		private void Main ()
		{


		}
		private void setFirstPart()
		{
			firstPart.cordX = X;
			firstPart.cordY = Y;
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

