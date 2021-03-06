﻿using System;
using System.Drawing;

namespace Snake
{
	public class TextObject:IGameObject
	{
		private Rectangle location;
		private string stringToDraw;
		public TextObject (Rectangle location,string stringToDraw)
		{
			this.location = location;
			this.stringToDraw = stringToDraw;
		}

		#region IGameObject implementation

		public void passData (GameData newInfo)
		{
			throw new NotImplementedException ();
		}

		public void update (double gameTime)
		{

		}

		public void draw (System.Drawing.Graphics brush)
		{
			// Create string to draw.
			String drawString = stringToDraw;

			// Create font and brush.
			Font drawFont = new Font("Arial", 45);
			SolidBrush drawBrush = new SolidBrush(Color.Yellow);

			// Create point for upper-left corner of drawing.
			PointF drawPoint = new PointF(location.X, location.Y);

			// Draw string to screen.
			brush.DrawString(drawString, drawFont, drawBrush, drawPoint);
		}

		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
		}

		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}

		public bool isColliding (IGameObject objectToTest)
		{
			return false;
		}

		#endregion
	}
}

