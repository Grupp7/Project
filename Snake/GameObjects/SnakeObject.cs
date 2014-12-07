﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Media;

namespace Snake{

	public class Snake:IGameObject{

		// Is the opposite direction the snake is currently moving
		private GameState oppositeDirection = GameState.Left;

		// Is the direction the snake is currently moving
		private GameState currentDirection = GameState.Right;

		// Is the directon the snake will move next
		private IGameObject newDirection;

		// Speed of the snake
		private int speed;

		//
		private int tickCounter;

		//
		private int tickCounterGreen;

		//
		private List<GameState> gameStates;

		//
		private GameState currentColorState;

		// List with all the snakes parts
		private LinkedList<IGameObject> snakeParts;

		// Initializes all the methods
		public Snake(){
			gameStates = new List<GameState>();
			snakeParts = new LinkedList<IGameObject>();
			currentColorState = GameState.None;

			// Sets the snake parts size and
			// move block size
			int snakePartWidth = 20;
			int snakePartHeight = 20;

			Size snakePartSize = new Size (snakePartWidth,snakePartHeight);

			// Sets the snakes first direction
			int SnakePartNewDir_X = 100;
			int SnakePartNewDir_Y = 140;

			Point snakePartDirRectPoint = new Point (SnakePartNewDir_X, SnakePartNewDir_Y);
			Rectangle snakePartDirRect = new Rectangle (snakePartDirRectPoint,snakePartSize);

		

			// Sets the location of first snakePart
			int snakePartOne_X = 100;
			int snakePartOne_Y = 400;

			Point snakePartOnePoint = new Point (snakePartOne_X, snakePartOne_Y);
			Rectangle snakePartOneRect = new Rectangle (snakePartOnePoint,snakePartSize);
			SnakePartObject snakePartOne = new SnakePartObject (snakePartOneRect);
			snakeParts.AddFirst(snakePartOne);

			// Sets the location of second snakePart
			int snakePartTwo_X = 100;
			int snakePartTwo_Y = 380;

			Point snakePartTwoPoint = new Point (snakePartTwo_X, snakePartTwo_Y);
			Rectangle snakePartTwoRect = new Rectangle (snakePartTwoPoint,snakePartSize);

			// Sets the location of the third snakePart
			int snakePartThree_X = 100;
			int snakePartThree_Y = 360;

			Point snakePartThreePoint = new Point (snakePartThree_X, snakePartThree_Y);
			Rectangle snakePartThreeRect = new Rectangle (snakePartThreePoint,snakePartSize);

			// Determines data for the snake from the beginning
			newDirection = new SnakePartObject(snakePartDirRect);
		
			snakeParts.AddFirst(new SnakePartObject(snakePartTwoRect));
			snakeParts.AddFirst(new SnakePartObject(snakePartThreeRect));
			speed = 80;
			tickCounter = 0;
		}


		#region IGameObject implementation

		// Check if colliding
		public bool isColliding (IGameObject objectToTest){
			return GameUtils.isColliding(this, objectToTest);
		}

		/// <summary>
		/// The current key pressed that will be handeled
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo){	
			addGameState(newInfo.state);
			switch(newInfo.state){
			case GameState.Up:
				if(oppositeDirection!=GameState.Up){
					currentDirection = GameState.Up;
				}
				break;
			case GameState.Down:
				if(oppositeDirection!=GameState.Down){
					currentDirection = GameState.Down;
				}

				break;
			case GameState.Left:
				if(oppositeDirection!=GameState.Left){
					currentDirection = GameState.Left;
				}

				break;
			case GameState.Right:
				if (oppositeDirection != GameState.Right) {
					currentDirection = GameState.Right;
				}
				break;



			}

		}

		// List of gameStates
		public List<GameState> getStates ()
		{
			return gameStates;
		}


		private void addGameState (GameState state){

			if(!gameStates.Contains(state)){
				gameStates.Add(state);
			}
		}

		/// <summary>
		/// The updatetimer tick here ,upda  100ms
		/// e snake movement
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime){
			tickCounter++;tickCounterGreen++;
			if(gameStates.Contains(GameState.Dead)){
				speed = 1000000;
			}
	
			if(speed < tickCounter * 10){
				lock(snakeParts){
					newDirection = getNewDirection();
					snakeCollide();
					if(gameStates.Contains(GameState.Grow)){
						gameStates.Remove(GameState.Grow);

					}
					else{
						if(!gameStates.Contains(GameState.Green)){
							snakeParts.RemoveFirst();
						}
					
					}
					if(gameStates.Contains(GameState.SpeedUp)){
						if(speed>30){
							speed -=10;
						}

						gameStates.Remove(GameState.SpeedUp);
					}
					if(gameStates.Contains(GameState.Break)){
						gameStates.Remove(GameState.Break);
						int counter = snakeParts.Count-10;
						for(int i = 0; i < counter; i++){
							snakeParts.RemoveFirst();
						}
						currentColorState= GameState.Black;
					}

					if(30*speed < tickCounterGreen * 10){

						gameStates.Remove(GameState.Green);
						gameStates.Add (GameState.None);
						tickCounterGreen = 0;
					}
					if(gameStates.Contains(GameState.Green)){
						currentColorState= GameState.Green;
						//gameStates.Remove(GameState.Red);
					}
					if(gameStates.Contains(GameState.None)){
						currentColorState= GameState.None;
						gameStates.Remove(GameState.None);
						gameStates.Remove(GameState.Green);
					}



					switch(currentColorState){
					case GameState.Black:
						newDirection.passData(new GameData(GameState.Black));
						break;
					case GameState.Green:
						newDirection.passData(new GameData(GameState.Green));
						break;
					case GameState.None:
						newDirection.passData(new GameData(GameState.None));
						break;
					default:
						newDirection.passData(new GameData(GameState.None));
						break;
					}

					snakeParts.AddLast(newDirection);

				}
				tickCounter = 0;

			}
		}

	

		private bool snakeCollide (){
			bool snakeIsDead = false;
			lock(snakeParts){
				foreach(var item in snakeParts){
					if(newDirection.isColliding(item)){
						gameStates.Add(GameState.Dead);
					}
				}
			}
			return snakeIsDead;

		}

		/// <summary>
		/// Draw the snakeparts here with the brush 60x
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (System.Drawing.Graphics brush){
			lock(snakeParts){
				foreach(var item in snakeParts){
					item.draw(brush);
				}
			}
		}

		public System.Drawing.Rectangle getRectangle (){
			return snakeParts.Last.Value.getRectangle();
		}

		#endregion
		/// <summary>
		/// Gets the new direction.
		/// </summary>
		/// <returns>The new direction.</returns>
		private IGameObject getNewDirection (){
			IGameObject temp;
			int lenght = snakeParts.Count;
			int xPos = snakeParts.Last.Value.getRectangle().X;
			int yPos = snakeParts.Last.Value.getRectangle().Y;
			int moveBlock = 20;

			// Says where next move of the snake will be
			switch(currentDirection){

			case GameState.Right:
				temp = getWrapperGameObject(new Point(xPos + moveBlock, yPos));
				oppositeDirection = GameState.Left;
				break;
			case GameState.Left:
				temp = getWrapperGameObject(new Point(xPos - moveBlock, yPos));
				oppositeDirection = GameState.Right;
				break;
			case GameState.Down:
				temp = getWrapperGameObject(new Point(xPos, yPos + moveBlock));
				oppositeDirection = GameState.Up;
				break;
			case GameState.Up:
				temp = getWrapperGameObject(new Point(xPos, yPos - moveBlock));
				oppositeDirection = GameState.Down;
				break;
			default:
				// Something goes wrong if it ends up here
				temp = getWrapperGameObject(new Point(0, 0));
				break;
			}

			return temp;
		}


		private IGameObject getWrapperGameObject (Point point){
			int BlockWidth = 20;
			int BlockHeight = 20;

			Size Block = new Size (BlockWidth, BlockHeight);
			return new SnakePartObject(new Rectangle(point,Block));
		}
	}
}
