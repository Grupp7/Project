using System;
using System.Diagnostics;
using System.Drawing;

namespace Snake
{
	public class ShowTimeObject:IGameObject
	{
		private Rectangle location;
		private Stopwatch stopwatch = new Stopwatch ();
		private string elaptime;

		public ShowTimeObject (Rectangle location)
		{
			this.location = location;
			stopwatch.Start();

		}


		#region IGameObject implementation
		public void passData (GameData newInfo)
		{

		}
		public void update (double gameTime)
		{
			elaptime = Convert.ToString (stopwatch.Elapsed.TotalSeconds);
			string[] temp = elaptime.Split (',');
			elaptime = temp [0];

		}
		public void draw (Graphics brush)
		{
			// Create string to draw.
			String drawString = "Time: "+ elaptime ;

			// Create font and brush.
			Font drawFont = new Font("Arial", 14);
			SolidBrush drawBrush = new SolidBrush(Color.Black);

			// Create point for upper-left corner of drawing.
			PointF drawPoint = new PointF(location.X, 0F);

			// Draw string to screen.
			brush.DrawString(drawString, drawFont, drawBrush, drawPoint);
		}
		public Rectangle getRectangle ()
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

