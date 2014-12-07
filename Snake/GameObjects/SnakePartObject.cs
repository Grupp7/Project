using System;
using System.Drawing;

namespace Snake
{
	/// <summary>
	/// Snake part object.
	/// </summary>
	public class SnakePartObject:IGameObject
	{
		private System.Collections.Generic.List<GameState> gameStates;
		private Rectangle location;
		private GameState currentColor;

		/// <summary>
		/// Initializes a new instance of the <see cref="Snake.SnakePartObject"/> class.
		/// </summary>
		/// <param name="location">Location.</param>
		public SnakePartObject (Rectangle location)
		{
			this.location = location;
			gameStates = new System.Collections.Generic.List<GameState>();
		}

		#region IGameObject implementation

		/// <summary>
		/// Gets the current states from the gameObject.
		/// </summary>
		/// <returns>The states.</returns>
		public System.Collections.Generic.List<GameState> getStates ()
		{
			return gameStates;
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
	
			return GameUtils.isColliding(this,objectToTest);
		}

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{	currentColor = newInfo.state;
			addGameState(newInfo.state);
		}
		private void addGameState (GameState state){
		
			if(!gameStates.Contains(state)){
				gameStates.Add(state);

			}
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
		{	SolidBrush myBrush2 = new SolidBrush (Color.DarkSlateBlue);
		
			if(currentColor == GameState.Green){
				myBrush2 = new SolidBrush (Color.DarkGreen);

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

		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		public System.Drawing.Rectangle getRectangle ()
		{
			return location;
		}

		#endregion
	}
}
