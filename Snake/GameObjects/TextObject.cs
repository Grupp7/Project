using System;
using System.Drawing;

namespace Snake
{
	/// <summary>
	/// Text object.
	/// Draws a specified text
	/// on a position
	/// </summary>
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

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Update the specified gameTime to the gameObject.
		/// And let it know that time has passed so it 
		/// can do its internal update
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime)
		{

		}

		/// <summary>
		/// Draw the specified appearance to the brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (System.Drawing.Graphics brush)
		{
			// Create string to draw.
			String drawString = stringToDraw;

			// Create font and brush.
			Font drawFont = new Font("Arial", 45);
			SolidBrush drawBrush = new SolidBrush(Color.Black);

			// Create point for upper-left corner of drawing.
			PointF drawPoint = new PointF(location.X, location.Y);

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
			//No colliding with unit interface
			return false;
		}

		#endregion
	}
}

