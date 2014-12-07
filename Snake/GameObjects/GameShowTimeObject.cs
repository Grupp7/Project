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
			//stopwatch.Start();

		}


		#region IGameObject implementation
		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{
			if(newInfo.state==GameState.Pause){
				stopwatch.Stop();
			}
			else if(newInfo.state==GameState.Start){
				stopwatch.Start();
			}
			else if(newInfo.state == GameState.Reset){
				stopwatch.Restart();

			}
		}
		/// <summary>
		/// Update the specified gameTime to the gameObject.
		/// And let it know that time has passed so it 
		/// can do its internal update*/
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime)
		{
			elaptime = Convert.ToString (stopwatch.Elapsed.TotalSeconds);
			string[] temp = elaptime.Split (',');
			elaptime = temp [0];

		}
		/// <summary>
		/// Draw the specified appearance to the brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (Graphics brush)
		{
			// Create string to draw.
			String drawString = "Time: "+ elaptime ;

			// Create font and brush.
			Font drawFont = new Font("Arial", 14);
			SolidBrush drawBrush = new SolidBrush(Color.White);

			// Create point for upper-left corner of drawing.
			PointF drawPoint = new PointF(location.X, 0F);

			// Draw string to screen.
			brush.DrawString(drawString, drawFont, drawBrush, drawPoint);
		}
		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		public Rectangle getRectangle ()
		{
			return location;
		}
		/// <summary>
		/// Gets the current states from the gameObject.
		/// </summary>
		/// <returns>The states.</returns>
		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}
		/// <summary>
		/// Check collsion between the this and
		/// the specified object to test
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="objectToTest">Object to test.</param>
		public bool isColliding (IGameObject objectToTest)
		{
			// No colliding with unit interface
			return false;
		}
		#endregion
	
	}

}

