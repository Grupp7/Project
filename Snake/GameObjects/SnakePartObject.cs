using System;
using System.Drawing;

namespace Snake
{
	public class SnakePartObject:IGameObject
	{
		private System.Collections.Generic.List<GameState> gameStates;
		private Rectangle location;
		private GameState currentColor;

		public SnakePartObject (Rectangle location)
		{
			this.location = location;
			gameStates = new System.Collections.Generic.List<GameState>();
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
		{	currentColor = newInfo.state;
			addGameState(newInfo.state);
		}
		private void addGameState (GameState state){
		
			if(!gameStates.Contains(state)){
				gameStates.Add(state);

			}
		}

		public void update (double gameTime)
		{



		}

		public void draw (System.Drawing.Graphics brush)
		{	SolidBrush myBrush2 = new SolidBrush (Color.DarkSlateBlue);
		
			if(currentColor == GameState.Red){
				myBrush2 = new SolidBrush (Color.Red);

			}
			else if(currentColor==GameState.Black){
					myBrush2 = new SolidBrush (Color.Black);
			}
			else if(currentColor==GameState.None){
				myBrush2 = new SolidBrush (Color.DarkSlateBlue);
			}

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
