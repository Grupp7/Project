using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Media;

namespace Snake{

	public class Snake:IGameObject{

	
		private GameState lastDirection = GameState.Left;
		private GameState currentDirection = GameState.Right;
		private IGameObject newDirection;
		private int speed;
		private int tickCounter;
		private int tickCounterGreen;
		private List<GameState> gameStates;
		private GameState currentColorState;
		private LinkedList<IGameObject> snakeParts;


		public Snake(){
			gameStates = new List<GameState>();
			snakeParts = new LinkedList<IGameObject>();
			currentColorState = GameState.None;
			newDirection = new SnakePartObject(new Rectangle(new Point(100, 140), new Size(new Point(20, 20))));
			snakeParts.AddFirst(new SnakePartObject(new Rectangle(new Point(100, 400), new Size(new Point(20, 20)))));
			snakeParts.AddFirst(new SnakePartObject(new Rectangle(new Point(100, 380), new Size(new Point(20, 20)))));
			snakeParts.AddFirst(new SnakePartObject(new Rectangle(new Point(100, 360), new Size(new Point(20, 20)))));
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
				if(lastDirection!=GameState.Up){
					currentDirection = GameState.Up;
				}
				break;
			case GameState.Down:
				if(lastDirection!=GameState.Down){
					currentDirection = GameState.Down;
				}

				break;
			case GameState.Left:
				if(lastDirection!=GameState.Left){
					currentDirection = GameState.Left;
				}

				break;
			case GameState.Right:
				if (lastDirection != GameState.Right) {
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
						if(!gameStates.Contains(GameState.Red)){
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
						Console.WriteLine (	"ENDED GREEN");
						gameStates.Remove(GameState.Red);
						gameStates.Add (GameState.None);
						tickCounterGreen = 0;
					}
					if(gameStates.Contains(GameState.Red)){
						currentColorState= GameState.Red;
						//gameStates.Remove(GameState.Red);
					}
					if(gameStates.Contains(GameState.None)){
						currentColorState= GameState.None;
						gameStates.Remove(GameState.None);
						gameStates.Remove(GameState.Red);
					}



					switch(currentColorState){
					case GameState.Black:
						newDirection.passData(new GameData(GameState.Black));
						break;
					case GameState.Red:
						newDirection.passData(new GameData(GameState.Red));
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

		private IGameObject getNewDirection (){
			IGameObject temp;
			int lenght = snakeParts.Count;
			int xPos = snakeParts.Last.Value.getRectangle().X;
			int yPos = snakeParts.Last.Value.getRectangle().Y;

			switch(currentDirection){

			case GameState.Right:
				temp = getWrapperGameObject(new Point(xPos + 20, yPos));
				lastDirection = GameState.Left;
				break;
			case GameState.Left:
				temp = getWrapperGameObject(new Point(xPos - 20, yPos));
				lastDirection = GameState.Right;
				break;
			case GameState.Down:
				temp = getWrapperGameObject(new Point(xPos, yPos + 20));
				lastDirection = GameState.Up;
				break;
			case GameState.Up:
				temp = getWrapperGameObject(new Point(xPos, yPos - 20));
				lastDirection = GameState.Down;
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
