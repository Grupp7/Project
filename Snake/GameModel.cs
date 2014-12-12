using System;
using System.Drawing;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Snake {

	/// <summary>
	/// This class holds every information about the game
	/// Its purpose is to control the rules and update corresponding gameobejects it holds
	/// It is responsible to update the Bitmap with the correct data from the gameobjects
	/// so the MainFrame can draw it to its Form
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
		private const string POWERUP_SOUND = "Powerup.wav";
		private const char UP = 'w';
		private const char DOWN = 's';
		private const char LEFT = 'a';
		private const char RIGHT = 'd';
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

		//The sound for when the snake dies
		private SoundPlayer playerDead;

		//The sound for when the snakes shrinks
		private SoundPlayer playerPowerUp;

		// Keeping track of the current possible high scores
		private int tempHighScore;

		// The highest score during the game sessions
		private int highScore;

		// How many foods the snake need to 
		// eat before it will shrink
		private int tempScoreCounter;

		// keep track of points earned
		private int score;

		// How many points every food gives
		private int points;

		// How often a green food spawns
		private int greenSnakeFoodCounter;

		// Saving value for going back and forth
		// between game and mainMenu
		private bool userWantsNewGame;

		#endregion

		/// <summary>
		/// Creates a GameModel
		/// </summary>
		/// <param name="The specified size of the BitMap for the model to draw">Client size.</param>
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

			//Initiate the sound for the death sound
			playerDead = new SoundPlayer(DEAD_SOUND);
			playerDead.Load ();

			//Initiate the sound for the powerup sound
			playerPowerUp = new SoundPlayer (POWERUP_SOUND);
			playerPowerUp.Load ();

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

			//Create a block for our background
			int backGroundPosX = 0;
			int backGroundPosY = 0;

			int backGroundWidth = 600;
			int backGroundHeigth = 600;

			//Get custom blockobject from the utils
			mainMenuBackground = GameUtils.getBlockObject (backGroundPosX, backGroundPosY, backGroundWidth, backGroundHeigth);
			mainMenuBackground.passData (new GameData (GameState.None));

			//Create snakefood

			int snakeFoodPosX = 300;
			int snakeFoodPosY = 350;

			Point snakeFoodPoint = new Point (snakeFoodPosX, snakeFoodPosY);

			int snakeFoodWidth = 100;
			int snakeFoodHeigth = 100;
			Size snakeFoodSize = new Size (snakeFoodWidth, snakeFoodHeigth);

			Rectangle snakeFoodRect = new Rectangle (snakeFoodPoint,snakeFoodSize);

			SnakeFoodObject snakeFood = new SnakeFoodObject (snakeFoodRect);

			//Add snakeFood to foodlist
			gameSnakeFood.Add (snakeFood);


			//Init gameScoreOBject

			int gameScorePosX = 0;
			int gameScorePosY = 200;

			Point gameScorePoint = new Point (gameScorePosX, gameScorePosY);

			int gameScoreWidth = 20;
			int gameScoreHeight = 20;

			Size gameScoreSize = new Size (gameScoreWidth, gameScoreHeight);

			Rectangle gameScoreRect = new Rectangle (gameScorePoint, gameScoreSize);

			ShowScoreObject gameScore = new ShowScoreObject (gameScoreRect);

			//Update the highscore if possible
			if (highScore < tempHighScore) {
				highScore = tempHighScore;
			}

			//Update the current highscore to the graphical object that draws highscore
			gameScore.highScore = highScore;

			//Reset all counters and scores
			tempHighScore = 0;
			score = 0;
			greenSnakeFoodCounter = 0;
			tempScoreCounter = 0;
			points = 1;

			//The new gameScore drawer that was initiated
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
			int boardLeftTopCornerPosX = 0;
			int boardRightTopCornerPosY = 0;

			//Create the right side of the board
			for (int i = 0; i <= fieldWidth; i += blockWidth) {
				gameObstacles.Add (GameUtils.getBlockObject (fieldWidth, i, blockWidth, blockHeight));
			}

			//Create the left side of the board
			for (int i = 0; i <= fieldWidth; i += blockWidth) {
				BlockObject temp = GameUtils.getBlockObject (boardLeftTopCornerPosX, i, blockWidth, blockHeight);
				temp.passData (new GameData (GameState.Grey));
				gameObstacles.Add (temp);
			}

			//Create the top side of the board
			for (int i = 0; i <= fieldHeigth; i += blockWidth) {
				BlockObject temp = GameUtils.getBlockObject (i, boardRightTopCornerPosY, blockWidth, blockHeight);
				temp.passData (new GameData (GameState.Grey));
				gameObstacles.Add (temp);
			}

			//Create the bot side of the board
			for (int i = 0; i <= fieldHeigth; i += blockWidth) {
				BlockObject temp = GameUtils.getBlockObject (i, fieldHeigth, blockWidth, blockHeight);
				temp.passData (new GameData (GameState.Grey));
				gameObstacles.Add (temp);
			}

		}

		#region ImodelInterface methods

		/// <summary>
		/// Gets the current Modelstates.
		/// </summary>
		/// <returns>The states.</returns>
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

				//Pass the new state 
				GameData newStateFromKey = new GameData (getKeyState (key));
				mainMenuSelectionBox.passData (newStateFromKey);

				if (userPressedConfirmKey && selectionBoxcontainsRunGame) {
					//Run the game again
					modelState = GameState.RunGame;

					if(userWantsNewGame){

						//Reset the time, since user wants new game
						gameTime.passData (new GameData (GameState.Reset));
						gameTime.passData (new GameData (GameState.Start));
						userWantsNewGame = false;
					}
				}
				else if(userPressedConfirmKey && !selectionBoxcontainsRunGame){
					//Prepare the game to exit
					modelState = GameState.ExitGame;
				}
				break;
			case GameState.RunGame:

				GameState getNewSnakeStateFromKeyboardInput = getKeyState (key);

				if (getNewSnakeStateFromKeyboardInput != GameState.None) {
					//Pass the new direction to the snake
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

		/// <summary>
		/// Gets the state of the corresponding key
		/// entered
		/// </summary>
		/// <returns>The key state.</returns>
		/// <param name="key">Key.</param>
		private GameState getKeyState (char key)
		{
			GameState state = GameState.None;

			switch (key) {
			case UP:
				state = GameState.Up;
				break;
			case DOWN:
				state = GameState.Down;

				break;
			case RIGHT:
				state = GameState.Right;

				break;
			case LEFT:
				state = GameState.Left;
				break;
			//User confirms current selection in menu
			case (char)Keys.Enter:
				state = GameState.Confirm;
				gameTime.passData (new GameData (GameState.Start));
				mainMenuSelectionBox.passData (new GameData (GameState.UnPause));
				break;
			//User pause the game while game running
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
		/// Updates things for the menu
		/// </summary>
		private void menUpdateRunning ()
		{
			//Nothing needed yet in this version
			//For future uses
		}

		/// <summary>
		/// Games the update running.
		/// </summary>
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
						greenSnakeFoodCounter++;

						if(item.getStates().Contains(GameState.Green)){
							//Pass info about that the snake has eaten a green food
							gameSnake.passData (new GameData (GameState.Green));
						}
						else{
							//Pass info about that the snake has eaten a normal food
							gameSnake.passData (new GameData (GameState.None));
						}

						//Play sound since the snake has eaten a food
						playerFood.Play ();
						// Tell the snake to grow
						gameSnake.passData (new GameData (GameState.Grow));
						//Tell the snake to speed up if possible
						gameSnake.passData (new GameData (GameState.SpeedUp));
						//Remove the eaten food
						gameSnakeFood.Clear ();

						int spawnRate = 4;
						if(greenSnakeFoodCounter>spawnRate){
							//Reset the counter
							greenSnakeFoodCounter = 0;

							//Spawn a green food in the game
							SnakeFoodObject temp = GameUtils.getRandomSnakeFoodObject();
							temp.passData(new GameData(GameState.Green));
							gameSnakeFood.Add(temp);
						}
						else{
							//Spawn normal food in the game
							gameSnakeFood.Add (GameUtils.getRandomSnakeFoodObject ());
						}

						//Update the score to the graphical objects
						GameData scoreInfo = new GameData();
						scoreInfo.score =points;
						gameScore.passData (scoreInfo);

						//Keep count of the scores for the model
						score+=points;
						tempScoreCounter++;
					}
				}
			
			}

			int snakeShrinkRate = 19;

			if(tempScoreCounter>snakeShrinkRate){
				tempScoreCounter = 0;
				points++;
				//Tell the snake to break a part
				gameSnake.passData(new GameData (GameState.Break));

				//Play the fance powerup sound
				playerPowerUp.Play ();
			}

			//Update the new highscore if possible
			if (tempHighScore < score) {
				tempHighScore = score;

			}

			//Tell the snake that some time has passed
			gameSnake.update (gameUpdateSpeed);
			//Tell the scoreObject that some time has passed
			gameScore.update (gameUpdateSpeed);

			//Check if the snake is alive
			if (gameSnake.getStates ().Contains (GameState.Dead)) {
				//Play dead sound if the snake is dead
				playerDead.Play ();
				//Pause the gameTime object
				gameTime.passData (new GameData (GameState.Pause));

				//Restart the game
				initGameData ();
				initMainMenu ();

				//Go the the menu
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

		/// <summary>
		/// Renders all relevant objects for the menu 
		/// to the Bitmap
		/// In the correct order (layering)
		/// </summary>
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
			gameTime.draw (g);
			gameScore.draw (g);
			g.Dispose ();
		}

		/// <summary>
		/// Renders all relevant object for the game
		/// to the Bitmap
		/// </summary>
		private void gameRenderRunning ()
		{
			Graphics g = Graphics.FromImage (backBuffer);

			g.Clear (Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			mainMenuBackground.draw (g);
			lock (gameObstacles) {
				foreach (var item in gameObstacles) {
					item.draw (g);
				}
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
