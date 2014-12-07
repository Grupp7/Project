using System;
using System.Drawing;

namespace Snake
{
	/// <summary>
	/// Block object.
	/// Standard blockObject
	/// That can be drawn with 
	/// different colors
	/// depending on which state
	/// it gets updated with
	/// </summary>
	public class BlockObject:IGameObject
	{

		private Rectangle location;
		private GameState state;

		/// <summary>
		/// Creates a standard BlockObject with a
		/// specified location
		/// </summary>
		/// <param name="location">Location.</param>
		public BlockObject (Rectangle location)
		{
			this.location = location;
		}

		#region IGameObject implementation

		/// <summary>
		/// Gets the current states from the gameObject.
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
			state = newInfo.state;
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
			SolidBrush myBrush2 = new SolidBrush (Color.Gray);
			if (state == GameState.None) {
				myBrush2 = new SolidBrush (Color.LightGreen);
			} else if (state == GameState.Black) {
				myBrush2 = new SolidBrush (Color.DarkGoldenrod);
			} else if (state == GameState.Grey) {
				myBrush2 = new SolidBrush (Color.Gray);
			}
			Rectangle cir = location;
			brush.FillRectangle (myBrush2, cir);		
			myBrush2.Dispose ();
		}

		/// <summary>
		/// Gets the rectangle with its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
		}

		/// <summary>
		/// Ises the colliding.
		/// </summary>
		/// <returns><c>true</c>, if colliding was ised, <c>false</c> otherwise.</returns>
		/// <param name="objectToTest">Object to test.</param>
		public bool isColliding (IGameObject objectToTest)
		{
			return GameUtils.isColliding (this, objectToTest);
		}

		#endregion
	}
}
