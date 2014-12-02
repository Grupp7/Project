using System;
using System.Drawing;

namespace Snake{
	public class SnakeFoodObject:IGameObject{

		private  Rectangle location;

		public SnakeFoodObject(Rectangle location){
			this.location = location;

		}

		#region IGameObject implementation

		public System.Collections.Generic.List<GameState> getStates ()
		{
			throw new NotImplementedException ();
		}

		public void passData (GameData newInfo){

		}

		public void update (double gameTime){

		}

		public void draw (System.Drawing.Graphics brush){
			SolidBrush myBrush2 = new SolidBrush (Color.DarkRed);
			Rectangle cir = location;
			Rectangle smaller = new Rectangle();
			smaller.X = location.X + Convert.ToInt32((location.Width-location.Width *0.7)/2);
			smaller.Y = location.Y +Convert.ToInt32((location.Height-location.Height *0.7)/2);
			smaller.Width = Convert.ToInt32(location.Width *0.7);
			smaller.Height = Convert.ToInt32(location.Height *0.7);

	
			brush.FillEllipse (myBrush2, cir);	
			myBrush2 = new SolidBrush (Color.Black);
			brush.FillEllipse (myBrush2, smaller);		

		
			myBrush2.Dispose ();
		}

		public System.Drawing.Rectangle getRectangle (){
			return location;
		}

		public bool isColliding (IGameObject objectToTest){
			return GameUtils.isColliding(this, objectToTest);
		}

		#endregion
	}
}

