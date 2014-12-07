using System;
using System.Drawing;

namespace Snake{
	/// <summary>
	/// Snake food object.
	/// </summary>
	public class SnakeFoodObject:IGameObject{

		private  Rectangle location;
		private System.Collections.Generic.List<GameState> gameStates;

		/// <summary>
		/// Initializes a new instance of the <see cref="Snake.SnakeFoodObject"/> class.
		/// </summary>
		/// <param name="location">Location.</param>
		public SnakeFoodObject(Rectangle location){
			this.location = location;
			gameStates = new System.Collections.Generic.List<GameState>();
		}

		#region IGameObject implementation

		/// <summary>
		/// Gets the current states from the gameObject.*/
		/// </summary>
		/// <returns>The states.</returns>
		public System.Collections.Generic.List<GameState> getStates ()
		{
			return gameStates;
		}

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo){

			addGameState(newInfo.state);
		}

		/// <summary>
		/// Update the specified gameTime to the gameObject.
		/// And let it know that time has passed so it 
		/// can do its internal update
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime){

		}
		/// <summary>
		/// Adds the state of the game.
		/// </summary>
		/// <param name="state">State.</param>
		private void addGameState (GameState state){

			if(!gameStates.Contains(state)){
				gameStates.Add(state);
			}
		}
		/// <summary>
		/// Draw the specified appearance to the brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (System.Drawing.Graphics brush){

			SolidBrush myBrush2 = new SolidBrush (Color.DarkSlateBlue);
			if(gameStates.Contains(GameState.Green)){
				myBrush2 = new SolidBrush (Color.DarkGreen);
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

		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		public System.Drawing.Rectangle getRectangle (){
			return location;
		}

		/// <summary>
		/// Check collsion between the this and
		/// the specified object to test
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="objectToTest">Object to test.</param>
		public bool isColliding (IGameObject objectToTest){
			return GameUtils.isColliding(this, objectToTest);
		}

		#endregion
	}
}

