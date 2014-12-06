using System;
using System.Drawing;

namespace Snake{
	public class SnakeFoodObject:IGameObject{

		private  Rectangle location;
		private System.Collections.Generic.List<GameState> gameStates;
		public SnakeFoodObject(Rectangle location){
			this.location = location;
			gameStates = new System.Collections.Generic.List<GameState>();
		}

		#region IGameObject implementation

		public System.Collections.Generic.List<GameState> getStates ()
		{
			return gameStates;
		}

		public void passData (GameData newInfo){

			addGameState(newInfo.state);
		}

		public void update (double gameTime){

		}
		private void addGameState (GameState state){

			if(!gameStates.Contains(state)){
				gameStates.Add(state);
			}
		}
		public void draw (System.Drawing.Graphics brush){

			SolidBrush myBrush2 = new SolidBrush (Color.DarkSlateBlue);
			if(gameStates.Contains(GameState.Red)){
				myBrush2 = new SolidBrush (Color.Red);
			}
			else {
				
			}
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

