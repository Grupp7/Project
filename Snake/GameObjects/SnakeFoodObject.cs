using System;
using System.Drawing;

namespace Snake{
	public class SnakeFoodObject:IGameObject{

		private Rectangle location;

		public SnakeFoodObject(Rectangle location){
			this.location = location;
		}

		#region IGameObject implementation

		public void passData (GameData newInfo){

		}

		public void update (double gameTime){
		
		}

		public void draw (System.Drawing.Graphics brush){
			SolidBrush myBrush2 = new SolidBrush (Color.Black);
			Rectangle cir = location;
			brush.FillEllipse (myBrush2, cir);		

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

