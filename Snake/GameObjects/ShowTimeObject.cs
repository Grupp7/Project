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

		public ShowTimeObject ()
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
			stopwatch.Elapsed.ToString(elaptime);
		}
		public void draw (System.Drawing.Graphics brush)
		{
			// Create string to draw.
			String drawString = "Score "+ elaptime;

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

