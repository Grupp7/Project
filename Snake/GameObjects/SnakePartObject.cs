using System;
using System.Drawing;

namespace Snake
{
	public class SnakePartObject:IGameObject
	{
		private Rectangle location;

		public SnakePartObject (Rectangle location)
		{
			this.location = location;
		}

		#region IGameObject implementation

		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}

		public bool isColliding (IGameObject objectToTest)
		{
			Rectangle otherRectangle = objectToTest.getRectangle ();
			Rectangle thisRectangle = location;
			bool hasCollided = false;

			if (thisRectangle.X < otherRectangle.X + otherRectangle.Width &&
				thisRectangle.X + thisRectangle.Width > otherRectangle.X &&
				thisRectangle.Y < otherRectangle.Y + otherRectangle.Height &&
				thisRectangle.Height + thisRectangle.Y > otherRectangle.Y) {
				hasCollided = true;
			}
			return hasCollided;
		}

		public void passData (GameData newInfo)
		{

		}

		public void update (double gameTime)
		{

		}

		public void draw (System.Drawing.Graphics brush)
		{
			SolidBrush myBrush2 = new SolidBrush (Color.DarkRed);
			Rectangle cir = location;
			brush.FillRectangle (myBrush2, cir);		
	
			myBrush2.Dispose ();
		}

		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
		}

		#endregion
	}
}
