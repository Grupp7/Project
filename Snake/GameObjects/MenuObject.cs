using System;
using System.Drawing;
using System.Collections.Generic;

namespace Snake
{
	/// <summary>
	/// Menu object.
	/// 
	/// </summary>
	public class MenuObject:IGameObject
	{

		private Rectangle location;
		private GameState currentState;
		private List<GameState> states;

		private string currentString;
		private const string NEW_GAME = "Play?";
		private const string EXIT_GAME = "Exit game?";

		private TextObject2 newGameTextObject;



		/// <summary>
		/// Initializes a new instance of the <see cref="Snake.MenuObject"/> class.
		/// </summary>
		/// <param name="location">Location.</param>
		public MenuObject (Rectangle location)
		{
			this.location = location;
			currentString = NEW_GAME;
			states = new List<GameState> ();
			currentState = GameState.RunGame;
			newGameTextObject = new TextObject2 (new Rectangle(new Point(150,200),new Size(20,20)),NEW_GAME);
		}

		#region IGameObject implementation

		/// <summary>
		/// Gets the current states from the gameObject.
		/// </summary>
		/// <returns>The states.</returns>
		public System.Collections.Generic.List<GameState> getStates ()
		{	states.Clear ();
			states.Add (currentState);
			return states;
		}

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{
			switch (newInfo.state) {

			case GameState.Left:
				if(currentState == GameState.ExitGame){
					currentString = NEW_GAME;
					currentState = GameState.RunGame;

				}
				else {
					currentString = EXIT_GAME;
					currentState = GameState.ExitGame;
				}

				break;
			case GameState.Right:
				if(currentState == GameState.ExitGame){
					currentString = NEW_GAME;
					currentState = GameState.RunGame;

				}
				else {
					currentString = EXIT_GAME;
					currentState = GameState.ExitGame;
				}
	
			
				break;

			default:
		
				break;
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
		{
			if(currentState==GameState.ExitGame){
				String drawString = currentString;

				// Create font and brush.
				Font drawFont = new Font ("Arial", 60);
				SolidBrush drawBrush = new SolidBrush (Color.Black);

				// Create point for upper-left corner of drawing.
				PointF drawPoint = new PointF (location.X, location.Y);

				// Draw string to screen.
				brush.DrawString (drawString, drawFont, drawBrush, drawPoint);
			}
			else{
				newGameTextObject.draw (brush);
			}


		

		}

		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		public System.Drawing.Rectangle getRectangle ()
		{return location;

		
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
			//Can never collide with this
			return false;

		}

		#endregion
	}
}

