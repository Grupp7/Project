using System;
using System.Drawing;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Snake {

	/// <summary>
	/// Game model.
	/// </summary>
	public class GameModel:IGameModel {

		#region ModelConfig

		// The Bitmap that the MainFrame will get and draw to screen
		private Bitmap backBuffer;

		// The timer that will update the gameObjects
		private System.Timers.Timer tickTimer;

		// update method called every 10ms
		private const double gameUpdateSpeed = 10;

		// The clientSize from the MainFrame
		// This determinates the size of the Bitmap in this model
		// that will get drawn.
		private Size clientSize;

		// string constants
		private const string FOOD_SOUND = "Pickup.wav";
		private const string HIGH_SCORE = "HIGHSCORE ";
		private const string DEAD_SOUND = "Death.wav";
		private const char UP = 'w';
		#endregion

		#region IGameObjects

		// The dangerous obstacles for the snake
		private List<IGameObject> gameObstacles;

		// The friendly obstacles for the snake
		// This will make the snake grow
		// and get a score for it
		private List<IGameObject> gameSnakeFood;

		// The snakeObject that includes our SnakeObject
		// But since we only need to talk to our interface
		// we do not need to know how it is implemented here.
		private IGameObject gameSnake;

		// MainMenu
		private IGameObject mainMenuBackground;
		private IGameObject mainMenuSelectionBox;
		private IGameObject mainMenuShowHighScore;

		//Game
		//Shows the score and highscore to the game
		private IGameObject gameScore;
		//Shows the elapsed gametime in seconds
		private IGameObject gameTime;

		#endregion

		#region GameData

		// The current state of the model
		// 1:state show menu
		// 2:state show and start game
		private GameState modelState;

		//The sound for eating a food
		private SoundPlayer playerFood;

		private SoundPlayer playerDead;
		// Keeping track of the current scores
		private int tempHighScore;
		private int highScore;
		private int tempScore;
		private int score;

		private int snakeFoodCounter;
		// Saving value for going back and forth
		// between game and mainMenu
		private bool userWantsNewGame;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Snake.GameModel"/> class.
		/// </summary>
		/// <param name="clientSize">Client size.</param>
		public GameModel (Size clientSize)
		{
			// How fast the objects will update
			// 10 equals 10ms
			// In a sense every IGameobject will have their
			// update method called every 10ms
			tickTimer = new  System.Timers.Timer (gameUpdateSpeed);
			tickTimer.Elapsed += tickGameObjects;
			tickTimer.Enabled = true;

			//save the initial Form size
			// this size is needed for the Bitmap
			// that the form will get from
			// getBitmap method from IGameModel interface
			this.clientSize = clientSize;

			// User can choose to exit application or start game
			userWantsNewGame = true;

			//Start in the mainMenu
			modelState = GameState.Menu;

			//Initiate the sound for food
			playerFood = new SoundPlayer (FOOD_SOUND);
			playerFood.Load ();

			playerDead = new SoundPlayer(DEAD_SOUND);
			playerDead.Load ();
			initMainMenu ();
			initGameData ();
		}

		/// <summary>
		/// Inits the main menu display
		/// </summary>
		private void initMainMenu ()
		{
			//Init menuselectionBox
			int selectionBoxPosX = 50;
			int selectionBoxPosY = 200;
			Point selectionBoxPoint = new Point (selectionBoxPosX, selectionBoxPosY);

			int selectionBoxWidth = 50;
			int selectionBoxHeight = 50;
			Size selectionBoxSize = new Size (selectionBoxWidth, selectionBoxHeight);

			Rectangle selectionBoxRect = new Rectangle (selectionBoxPoint, selectionBoxSize);
			mainMenuSelectionBox = new MenuObject (selectionBoxRect);

			//Init menu High Score
			int highScorePosX =20;
			int highScorePosY =100;
			Point highScorePoint = new Point (highScorePosX, highScorePosY);

			int highScoreWidth = 50;
			int highScoreHeigth = 50;
			Size highScoreSize = new Size (highScoreWidth, highScoreHeigth);

			Rectangle highScoreRect = new Rectangle (highScorePoint,highScoreSize);

			string highScoreString = HIGH_SCORE+highScore.ToString ();
			mainMenuShowHighScore = new TextObject (highScoreRect,highScoreString );
		}

		/// <summary>
		/// Here every gameObject is initialized 
		/// for the snake game
		/// creating the snakeObject
		/// creating the map
		/// creating the food
		/// 
		/// </summary>
		private void initGameData ()
		{

			// Init gameTime
			int gameTimeWidth = 10;
			int gameTimeHeight = 10;

			Size gameTimeSize = new Size (gameTimeWidth, gameTimeHeight);

			int gameTimePosX = 400;
			int gameTimePosY = 0;

			Point gameTimePoint = new Point (gameTimePosX, gameTimePosY);

			Rectangle gameTimeRect = new Rectangle (gameTimePoint, gameTimeSize);

			gameTime = new ShowTimeObject (gameTimeRect);

			//Init gameObstacles
			gameObstacles = new List<IGameObject> ();

			// Create the obstacles in the map
			addObstaclesToList ();

			//Init snakeFood
			gameSnakeFood = new List<IGameObject> ();
			// Creates our implemented snake
			gameSnake = new Snake ();
			mainMenuBackground = GameUtils.getBlockObject (0, 0, 600, 600);
			mainMenuBackground.passData (new GameData (GameState.None));

			gameSnakeFood.Add (new SnakeFoodObject(new Rectangle(new Point(300,350),new Size(100,100))));
			ShowScoreObject gameScore = new ShowScoreObject (new Rectangle (new Point (0, 200), new Size (20, 20)));
			if (highScore < tempHighScore) {
				highScore = tempHighScore;
			}

			gameScore.highScore = highScore;
			tempHighScore = 0;
			score = 0;
			snakeFoodCounter = 0;
			this.gameScore = gameScore;
		

		}

		/// <summary>
		/// Creates the playing field.
		/// </summary>
		private void addObstaclesToList ()
		{
			int fieldWidth = 500;
			int fieldHeigth = 500;
			int blockHeight = 20;
			int blockWidth = 20;

			for (int i = 0; i <= fieldWidth; i += blockWidth) {
				gameObstacles.Add (GameUtils.getBlockObject (fieldWidth, i, blockWidth, blockHeight));
			}
			for (int i = 0; i <= fieldWidth; i += blockWidth) {
				BlockObject temp = GameUtils.getBlockObject (0, i, blockWidth, blockHeight);
				temp.passData (new GameData (GameState.Grey));
				gameObstacles.Add (temp);
			}
			for (int i = 0; i <= fieldHeigth; i += blockWidth) {
				BlockObject temp = GameUtils.getBlockObject (i, 0, blockWidth, blockHeight);
				temp.passData (new GameData (GameState.Grey));
				gameObstacles.Add (temp);
			}
			for (int i = 0; i <= fieldHeigth; i += blockWidth) {

				BlockObject temp = GameUtils.getBlockObject (i, fieldHeigth, blockWidth, blockHeight);
				temp.passData (new GameData (GameState.Grey));
				gameObstacles.Add (temp);
			}

		}

		#region ImodelInterface methods

		public List<GameState> getStates ()
		{
			List<GameState> temp = new List<GameState> ();
			temp.Add (modelState);
			return temp;
		}

		/// <summary>
		/// Updates the current key to the model
		/// </summary>
		/// <param name="key">Key.</param>
		public void updateCurrentKey (char key)
		{
			bool userPressedConfirmKey = getKeyState (key) == GameState.Confirm;
			bool selectionBoxcontainsRunGame = mainMenuSelectionBox.getStates ().Contains (GameState.RunGame);
			bool selectionBoxcontainsExitGame = mainMenuSelectionBox.getStates ().Contains (GameState.ExitGame);
			switch(modelState){
			case GameState.Menu:
				mainMenuSelectionBox.passData (new GameData (getKeyState (key)));
				if (userPressedConfirmKey && selectionBoxcontainsRunGame) {
					modelState = GameState.RunGame;
					if(userWantsNewGame){
						gameTime.passData (new GameData (GameState.Reset));
						gameTime.passData (new GameData (GameState.Start));
						userWantsNewGame = false;
					}
				}
				else if(userPressedConfirmKey && !selectionBoxcontainsRunGame){
						modelState = GameState.ExitGame;
				}
				break;
			case GameState.RunGame:

				GameState getNewSnakeStateFromKeyboardInput = getKeyState (key);

				if (getNewSnakeStateFromKeyboardInput != GameState.None) {
					gameSnake.passData (new GameData (getNewSnakeStateFromKeyboardInput));
				}
				break;
			}
		}

		/// <summary>
		/// Gets the bitmap from the model
		/// </summary>
		/// <returns>The bitmap.</returns>
		public Bitmap getBitmap ()
		{
			if (backBuffer == null) {
				backBuffer = new Bitmap (this.clientSize.Width, this.clientSize.Height);
			}
			renderGameObjects ();
			return backBuffer;
		}

		/// <summary>
		/// Change the size of the bitmap that it will draw in the model
		/// </summary>
		/// <param name="clientSize">Client size.</param>
		public void resizeBitmap (Size clientSize)
		{
			if (backBuffer != null) {
				backBuffer.Dispose ();
				backBuffer = null;
			}
			this.clientSize = clientSize;
		}

		#endregion

		private GameState getKeyState (char key)
		{
			GameState state = GameState.None;

			switch (key) {
			case UP:
				state = GameState.Up;
				break;
			case 's':
				state = GameState.Down;

				break;
			case 'd':
				state = GameState.Right;

				break;
			case 'a':
				state = GameState.Left;
				break;
			case (char)Keys.Enter:
				state = GameState.Confirm;
				gameTime.passData (new GameData (GameState.Start));
				mainMenuSelectionBox.passData (new GameData (GameState.UnPause));
				break;
			case (char)Keys.Escape:
				if(modelState != GameState.Menu){
					gameTime.passData (new GameData (GameState.Pause));
					mainMenuSelectionBox.passData (new GameData (GameState.Pause));
				}
				modelState = GameState.Menu;

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
		private void tickGameObjects (object sender, System.EventArgs e)
		{
			if (modelState == GameState.Menu) {
				menUpdateRunning ();
			} 
			else if (modelState == GameState.RunGame) {
				gameUpdateRunning ();
			}

		
		}

		/// <summary>
		/// Mens the update running.
		/// </summary>
		private void menUpdateRunning ()
		{

		}

		private void gameUpdateRunning ()
		{
			gameTime.update (gameUpdateSpeed);
			foreach (var item in gameObstacles) {

				item.update (gameUpdateSpeed);
				if (GameUtils.isColliding (item, gameSnake)) {
					gameSnake.passData (new GameData (GameState.Dead));
				
				}

			}
			lock (gameSnakeFood) {
				foreach (var item in gameSnakeFood) {
					if (GameUtils.isColliding (item, gameSnake)) {
						snakeFoodCounter++;
						if(item.getStates().Contains(GameState.Red)){
							gameSnake.passData (new GameData (GameState.Red));
						}
						else{
							gameSnake.passData (new GameData (GameState.None));
						}

						playerFood.Play ();
						gameSnake.passData (new GameData (GameState.Grow));
						gameSnake.passData (new GameData (GameState.SpeedUp));
						gameSnakeFood.Clear ();
						if(snakeFoodCounter>10){
							snakeFoodCounter = 0;
							SnakeFoodObject temp = GameUtils.getRandomSnakeFoodObject();
							temp.passData(new GameData(GameState.Red));
							gameSnakeFood.Add(temp);
						}
						else{
							gameSnakeFood.Add (GameUtils.getRandomSnakeFoodObject ());
						}
					
						gameScore.passData (new GameData (GameState.Score));
						score++;
						tempScore++;
					}
				}
			
			}

			if(tempScore>75){
				tempScore = 0;
				gameSnake.passData(new GameData (GameState.Break));
			}

			if (tempHighScore < score) {
				tempHighScore = score;

			}

			gameSnake.update (gameUpdateSpeed);
			gameScore.update (gameUpdateSpeed);

			if (gameSnake.getStates ().Contains (GameState.Dead)) {
				playerDead.Play ();
				gameTime.passData (new GameData (GameState.Pause));
				initGameData ();
				initMainMenu ();
				modelState = GameState.Menu;
			
			}
		}

		/// <summary>
		/// This method renders all gameObjects and its childs
		/// Calls the draw method in every objects interface(IGameObject)
		/// 
		/// </summary>
		private void renderGameObjects ()
		{
			if (modelState == GameState.Menu) {
				menuRenderRunning ();
			} else if (modelState == GameState.RunGame) {
				gameRenderRunning ();
			}

		}

		private void menuRenderRunning ()
		{
			Graphics g = Graphics.FromImage (backBuffer);
			g.Clear (Color.LightGreen);      
			g.SmoothingMode = SmoothingMode.AntiAlias;

			foreach (var item in gameObstacles) {
				item.draw (g);
			}
			lock (gameSnakeFood) {
				foreach (var item in gameSnakeFood) {
					item.draw (g);
				}
			}
			gameSnake.draw (g);
			mainMenuSelectionBox.draw (g);
			//mainMenuShowHighScore.draw (g);
			gameTime.draw (g);
			gameScore.draw (g);
		
			g.Dispose ();
		}

		private void gameRenderRunning ()
		{
			Graphics g = Graphics.FromImage (backBuffer);

			g.Clear (Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			mainMenuBackground.draw (g);
			foreach (var item in gameObstacles) {
				item.draw (g);
			}
			lock (gameSnakeFood) {
				foreach (var item in gameSnakeFood) {
					item.draw (g);
				}
			}

			gameSnake.draw (g);
			gameTime.draw (g);
			gameScore.draw (g);
			g.Dispose ();
		}

	}
}
