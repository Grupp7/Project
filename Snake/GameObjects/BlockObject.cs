using System;
using System.Drawing;

namespace Snake
{
	public class BlockObject:IGameObject
	{
		private Rectangle location;
		public BlockObject (Rectangle location)
		{this.location = location;
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
			SolidBrush myBrush2 = new SolidBrush (Color.Gray);
			Rectangle cir = location;
			brush.FillRectangle (myBrush2, cir);		
			myBrush2.Dispose ();
		}

		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
		}

		public bool isColliding (IGameObject objectToTest)
		{
			return GameUtils.isColliding (this, objectToTest);
		}

		#endregion
	}
}
