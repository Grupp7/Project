using System;
using System.Drawing;

namespace Snake
{
	public class BlockObject:IGameObject
	{
		private Rectangle location;
		private GameState state;
		public BlockObject (Rectangle location)
		{this.location = location;
		}

		#region IGameObject implementation

		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}

		public void passData (GameData newInfo)
		{
			state = newInfo.state;
		}

		public void update (double gameTime)
		{

		}

		public void draw (System.Drawing.Graphics brush)
		{	SolidBrush myBrush2 = new SolidBrush (Color.Gray);
			if(state == GameState.None){
				myBrush2 =new SolidBrush (Color.LightGreen);
			}
			else if(state == GameState.Black){
				myBrush2 =new SolidBrush (Color.DarkGoldenrod);
			}
			else if(state == GameState.Grey){
				myBrush2 =new SolidBrush (Color.Gray);
			}
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
