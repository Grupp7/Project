using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Media;

namespace Snake{
	public class GameModel:IGameModel{
		#region ModelConfig
		// The Bitmap that the MainFrame will get and draw to screen
		private Bitmap backBuffer;

		// The timer that will update the gameObjects
		private System.Timers.Timer tickTimer;

		// update method called every 10ms
		private const double gameUpdateSpeed = 10;

		// The keypressed updated from the MainFrame
		private char keyPressed;

		// The clientSize from the MainFrame
		// This determinates the size of the Bitmap in this model
		private Size clientSize;

		#endregion
		#region IGameObjects

		// The dangerous obstacles for the snake
		private List<IGameObject> gameObstacles;

		private List<IGameObject> snakeFood;

		// The snakeObject that includes our SnakeObject
		// But since we only need to talk to our interface
		// we do not need to know how it is implemented here.
		private IGameObject snake;
		#endregion

		private SoundPlayer player = new SoundPlayer("Pickup.wav");

		public GameModel(Size clientSize){
		
			// How fast the objects will update
			// 10 equals 10ms
			// In a sense every IGameobject will have their
			// update method called every 10ms
			tickTimer = new  System.Timers.Timer(gameUpdateSpeed);
			tickTimer.Elapsed += tickGameObjects;
			tickTimer.Enabled = true;

			//save the initial Form size
			// this size is needed for the Bitmap
			// that the form will get from
			// getBitmap method from IGameModel interface
			this.clientSize = clientSize;

			// Initializze our gameobject list
			gameObstacles = new List<IGameObject>();
			snakeFood = new List<IGameObject>();
			player.Load();
			initGameData();
		}

		/// <summary>
		/// Here every gameObject is initialized 
		/// for the snake game
		/// creating the snakeObject
		/// creating the map
		/// creating the food
		/// 
		/// </summary>
		private void initGameData (){
			// Creates our implemented snake
			snake = new Snake();

			snakeFood.Add(new SnakeFoodObject(new Rectangle(new Point(100,200),new Size(new Point(20,20)))));
			// Create the obstacles in the map
			createPlayingField ();
		}

		/// <summary>
		/// Creates the playing field.
		/// </summary>
		private void createPlayingField(){

			for (int i = 0; i < 500; i+=50) {
				gameObstacles.Add (new BlockObject (new Rectangle (new Point (0, i), new Size (50, 20))));
				gameObstacles.Add (new BlockObject (new Rectangle (new Point (500, i), new Size (50, 20))));
			}
			for (int i =0; i < 500; i+=20) {
				gameObstacles.Add (new BlockObject (new Rectangle (new Point (i, 0), new Size (20, 20))));
				gameObstacles.Add (new BlockObject (new Rectangle (new Point (i,500), new Size (20, 20))));

			}

		}

		#region ImodelInterface methods

		/// <summary>
		/// Updates the current key to the model
		/// </summary>
		/// <param name="key">Key.</param>
		public void updateCurrentKey(char key){
			GameState snakState = getKeyState(key);
			if(snakState != GameState.None){
				snake.passData(new GameData(snakState));
			}
		
		}

		/// <summary>
		/// Gets the bitmap from the model
		/// </summary>
		/// <returns>The bitmap.</returns>
		public Bitmap getBitmap (){
			if(backBuffer==null){
				backBuffer = new Bitmap(this.clientSize.Width, this.clientSize.Height);
			}
			renderGameObjects();
			return backBuffer;
		}

		/// <summary>
		/// Change the size of the bitmap that it will draw in the model
		/// </summary>
		/// <param name="clientSize">Client size.</param>
		public void resizeBitmap (Size clientSize){
			if(backBuffer!=null){
				backBuffer.Dispose();
				backBuffer = null;
			}
			this.clientSize = clientSize;
		}
		#endregion

		private GameState getKeyState(char key){
			GameState state = GameState.None;

			switch(key){
			case (char)Keys.W:
				state = GameState.Up;
				break;
			case (char)Keys.S:
				state = GameState.Down;

				break;
			case (char)Keys.D:
				state = GameState.Right;

				break;
			case (char)Keys.A:
				state = GameState.Left;

				break;

			}

				return state;
		}

		/// <summary>
		/// The tickTimer (Timer) will call this method every 10ms
		/// This will update every GameObject in the game
		/// Calling the update method in IGameObject in the gameObjectList 
		/// It also checks collision for snake and any dangerous objects
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void tickGameObjects (object sender, System.EventArgs e){

			foreach(var item in gameObstacles){

				item.update(gameUpdateSpeed);
				if(GameUtils.isColliding(item,snake)){
				
					snake.passData (new GameData(GameState.Dead));
				}

			}
			foreach(var item in snakeFood){
				if(GameUtils.isColliding(item,snake)){
					player.Play();
					snake.passData (new GameData(GameState.Grow));
					Random rand = new Random();
					snakeFood.Clear();
					snakeFood.Add (new SnakeFoodObject (new Rectangle (new Point (rand.Next(50,500), rand.Next(5,500)), new Size (new Point (rand.Next(1,50), rand.Next(1,50))))));

				}
			}
			snake.update (gameUpdateSpeed);
		}

	
		/// <summary>
		/// This method renders all gameObjects and its childs
		/// Calls the draw method in every objects interface(IGameObject)
		/// 
		/// </summary>
		private void renderGameObjects (){
			Graphics g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			foreach(var item in gameObstacles){
				item.draw(g);
			}
			foreach(var item in snakeFood){
				item.draw(g);
			}
			snake.draw (g);
			g.Dispose();
		}


	}
}
