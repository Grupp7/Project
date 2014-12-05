﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace Snake
{
	public class MenuObject:IGameObject
	{
		private Rectangle location;
		private string currentString;
		private const string NEW_GAME = "Play?";
		private const string EXIT_GAME = "Exit game?";
		private TextObject2 textObject;
		private GameState currentState;
		private List<GameState> states;
		public MenuObject (Rectangle location)
		{
			this.location = location;
			currentString = NEW_GAME;
			states = new List<GameState> ();
			currentState = GameState.RunGame;
			textObject = new TextObject2 (new Rectangle(new Point(150,200),new Size(20,20)),NEW_GAME);
		}

		#region IGameObject implementation

		public System.Collections.Generic.List<GameState> getStates ()
		{	states.Clear ();
			states.Add (currentState);
			return states;
		}

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

		public void update (double gameTime)
		{

		}

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
				textObject.draw (brush);
			}


		

		}

		public System.Drawing.Rectangle getRectangle ()
		{return location;

		
		}

		public bool isColliding (IGameObject objectToTest)
		{
			return false;

		}

		#endregion
	}
}

