using System;
using System.Drawing;

namespace Snake
{
	public class ShowScoreObject:IGameObject
	{
		private Rectangle location;
		private int highScore= 123344; //stuff
		private int score= 1231231;  

		public ShowScoreObject (Rectangle location)
		{
			this.location = location;
		}

		#region IGameObject implementation

		public void passData (GameData newInfo)
		{

		}

		public void update (double gameTime)
		{

		}

		public void draw (System.Drawing.Graphics brush)
		{
			// Create string to draw.
			String drawString = "Score "+ score+ " HighScore"+ highScore;

			// Create font and brush.
			Font drawFont = new Font("Arial", 14);
			SolidBrush drawBrush = new SolidBrush(Color.Black);

			// Create point for upper-left corner of drawing.
			PointF drawPoint = new PointF(20.0F, 0F);

			// Draw string to screen.
			brush.DrawString(drawString, drawFont, drawBrush, drawPoint);
		}

		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
		}

		public bool isColliding (IGameObject objectToTest)
		{
			return false;
		}

		#endregion
	}
}

