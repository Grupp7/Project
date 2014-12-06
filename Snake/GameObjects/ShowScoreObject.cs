using System;
using System.Drawing;

namespace Snake
{
	public class ShowScoreObject:IGameObject
	{
		private Rectangle location;
		public int highScore;
		private int score;  

		public ShowScoreObject (Rectangle location)
		{
			this.location = location;
		}

		#region IGameObject implementation

		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}

		public void passData (GameData newInfo)
		{


			score+=newInfo.score;

		}

		public void update (double gameTime)
		{
			if(highScore<score){

				highScore = score;
			}
		}

		public void draw (System.Drawing.Graphics brush)
		{
			// Create string to draw.
			String drawString = "Score: "+ score+ "               HighScore: "+ highScore;

			// Create font and brush.
			Font drawFont = new Font("Arial", 14);
			SolidBrush drawBrush = new SolidBrush(Color.White);

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

