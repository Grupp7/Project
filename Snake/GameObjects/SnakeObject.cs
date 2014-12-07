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

		// Is the direction the snake will move next
		private IGameObject newDirection;

		// Speed of the snake
		private int speed;

		private int tickCounter;

		private int tickCounterGreen;

		private List<GameState> gameStates;

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

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
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

		/// <summary>
		/// Gets the current states from the gameObject.
		/// </summary>
		/// <returns>The states.</returns>
		public List<GameState> getStates ()
		{
			return gameStates;
		}

		/// <summary>
		/// Adds the new state to the snake.
		/// </summary>
		/// <param name="state">State.</param>
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
		public void update (double gameTime){
			tickCounter++;tickCounterGreen++;
			if(gameStates.Contains(GameState.Dead)){
				//This will make the snake move very slowly
				// particulary deathlike
				speed = 1000000;
			}

			if(speed < tickCounter * gameTime){
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
						int maxSpeed = 30;
						int speedIncrease = 10;
						if(speed>maxSpeed){
							//This increase the speed
							speed -=speedIncrease;
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


					if(30*speed < tickCounterGreen * gameTime){

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

	
		/// <summary>
		/// Check if snake collides with itself
		/// </summary>
		/// <returns><c>true</c>, If snake collide with self, <c>false</c> otherwise.</returns>
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
		/// Draw the specified appearance to the brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (System.Drawing.Graphics brush){
			lock(snakeParts){
				foreach(var item in snakeParts){
					item.draw(brush);
				}
			}
		}

		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
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


		/// <summary>
		/// Gets the wrapper gameobject.
		/// The standard SnakePartObject at
		/// a position standard size
		/// </summary>
		/// <returns>The wrapper game object.</returns>
		/// <param name="point">Point.</param>
		private IGameObject getWrapperGameObject (Point point){
			int blockWidth = 20;
			int blockHeight = 20;

			Size block = new Size (blockWidth, blockHeight);
			return new SnakePartObject(new Rectangle(point,block));
		}
	}
}
