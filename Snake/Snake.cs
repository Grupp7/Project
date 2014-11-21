using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
	public struct snakeparts
	{
		public int cordX;
		public int cordY;

	
	}
	public class Snake:IGameObject
	{
		int X;
		int Y;
		Userinput userInput;
		List <snakeparts> snakeBody;
		snakeparts firstPart;
		snakeparts part;


		public Snake ()
		{
//			userInput = new Userinput ();
//			snakeBody = new List<snakeparts> ();
//			firstPart = new snakeparts ();
//			part = new snakeparts();
//			X = userInput.x;
//			Y = userInput.y;
		
		}
		#region IGameObject implementation

		/// <summary>
		/// The current key pressed that will be handeled
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{
		
		}

		/// <summary>
		/// The updatetimer tick here ,upda  100ms
		/// e snake movement
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime)
		{

		}

		/// <summary>
		/// Draw the snakeparts here with the brush 60x
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (System.Drawing.Graphics brush)
		{
			Pen rectanglePen = new Pen(Color.AliceBlue);
			rectanglePen.Color = Color.Aqua;
			Rectangle rect = new Rectangle (1, 2, 20, 30);
			brush.DrawRectangle (rectanglePen,rect);

			Pen myPen= new Pen(Color.Black);

		}

		public System.Drawing.Rectangle getRectangle ()
		{
			return new Rectangle(0,0,1,1);
		}

		#endregion

		private void setFirstPart()
		{
			firstPart.cordX = X;
			firstPart.cordY = Y;
		}
		private void move ()
		{
			int lastPart;
			lastPart = snakeBody.Count;
			snakeBody.RemoveAt (lastPart);
			snakeBody.Insert (0, firstPart);
		}
	}
}

