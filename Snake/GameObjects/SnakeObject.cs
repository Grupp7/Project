using System;
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
			int snakePartHeigth = 20;

			Size snakePartSize = new Size (snakePartWidth,snakePartHeigth);

			// Sets the snakes first direction
			Point snakePartDirRectPoint = new Point (100, 140);
			Rectangle snakePartDirRect = new Rectangle (snakePartDirRectPoint,snakePartSize);

		

			// Sets the location of first snakePart
			Point snakePartOnePoint = new Point (100, 400);
			Rectangle snakePartOneRect = new Rectangle (snakePartOnePoint,snakePartSize);
			SnakePartObject snakePartOne = new SnakePartObject (snakePartOneRect);
			snakeParts.AddFirst(snakePartOne);

			// Sets the location of second snakePart
			Point snakeSecPartPoint = new Point (100, 380);
			Rectangle snakeSecPartRect = new Rectangle (snakeSecPartPoint,snakePartSize);

			// Sets the location of the third snakePart
			Point snakeThirPartPoint = new Point (100, 360);
			Rectangle snakeThirPartRect = new Rectangle (snakeThirPartPoint,snakePartSize);

			// Determines data for the snake from the beginning
			newDirection = new SnakePartObject(snakePartDirRect);
		
			snakeParts.AddFirst(new SnakePartObject(snakeSecPartRect));
			snakeParts.AddFirst(new SnakePartObject(snakeThirPartRect));
			speed = 80;
			tickCounter = 0;
		}


		#region IGameObject implementation

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

			// Says where next move of the snake will be made
			switch(currentDirection){

			case GameState.Right:
				temp = getWrapperGameObject(new Point(xPos + 20, yPos));
				oppositeDirection = GameState.Left;
				break;
			case GameState.Left:
				temp = getWrapperGameObject(new Point(xPos - 20, yPos));
				oppositeDirection = GameState.Right;
				break;
			case GameState.Down:
				temp = getWrapperGameObject(new Point(xPos, yPos + 20));
				oppositeDirection = GameState.Up;
				break;
			case GameState.Up:
				temp = getWrapperGameObject(new Point(xPos, yPos - 20));
				oppositeDirection = GameState.Down;
				break;
			default:
				temp = getWrapperGameObject(new Point(0, 0));
				break;
			}

			return temp;
		}


		private IGameObject getWrapperGameObject (Point point){
			return new SnakePartObject(new Rectangle(point, new Size(new Point(20, 20))));
		}
	}
}
