using System;
using System.Drawing;

namespace Snake
{
	public class ShowScoreObject:IGameObject
	{
		private Rectangle location;
		public int highScore;
		private int score;  

		/// <summary>
		/// Initializes a new instance of the <see cref="Snake.ShowScoreObject"/> class.
		/// </summary>
		/// <param name="location">Location.</param>
		public ShowScoreObject (Rectangle location)
		{
			this.location = location;
		}

		#region IGameObject implementation

		/// <summary>
		/// Gets the current states from the gameObject.*/
		/// </summary>
		/// <returns>The states.</returns>
		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{


			score+=newInfo.score;

		}

		/// <summary>
		/// Update the specified gameTime to the gameObject.
		/// And let it know that time has passed so it 
		/// can do its internal update
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime)
		{
			if(highScore<score){

				highScore = score;
			}
		}

		/// <summary>
		/// Draw the specified appearance to the brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
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

		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
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
			return false;
		}

		#endregion
	}
}

