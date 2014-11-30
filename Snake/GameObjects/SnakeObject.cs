using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Media;

namespace Snake
{

	public class Snake:IGameObject
	{

		GraphicsState state;
		private SoundPlayer player = new SoundPlayer("Pickup.wav");
		private string lastDirection = "LEFT";
		private string currentDirection = "RIGHT";
		private IGameObject newDirection;
		private int speed;
		private int tickCounter;
	
		private bool snakeDead;

		private LinkedList<IGameObject> snakeParts;
		private LinkedList<IGameObject> snakeFood;

		public Snake ()
		{
			snakeParts = new LinkedList<IGameObject> ();
			snakeFood = new LinkedList<IGameObject> ();
			newDirection = new SnakePartObject (new Rectangle (new Point (100, 140), new Size (new Point (20, 20))));
			snakeParts.AddFirst(new SnakePartObject(new Rectangle(new Point(100,140),new Size(new Point(20,20)))));
			snakeParts.AddFirst(new SnakePartObject(new Rectangle(new Point(100,120),new Size(new Point(20,20)))));
			snakeParts.AddFirst(new SnakePartObject(new Rectangle(new Point(100,100),new Size(new Point(20,20)))));
			player.Load();
			snakeFood.AddFirst(new SnakeFoodObject(new Rectangle(new Point(100,200),new Size(new Point(20,20)))));
			snakeDead = false;

			speed = 300;
			tickCounter = 0;
		}


		#region IGameObject implementation

		public bool isColliding (IGameObject objectToTest)
		{
			return GameUtils.isColliding (this, objectToTest);
		}

		/// <summary>
		/// The current key pressed that will be handeled
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{	
			switch(newInfo.key){
			case (char)Keys.W:
				if(lastDirection != "UP")
				{
					currentDirection = "UP";
				}
				break;
			case (char)Keys.S:
				if(lastDirection != "DOWN" )
				{
					currentDirection = "DOWN";
				}

				break;
			case (char)Keys.A:
				if(lastDirection != "LEFT" )
				{
					currentDirection = "LEFT";
				}

				break;
			case (char)Keys.D:
				if(lastDirection != "RIGHT" )
				{
					currentDirection = "RIGHT";
				}

				break;
			case (char)Keys.P:
				speed = 500000;

				if(!snakeDead)
					using (SoundPlayer player = new SoundPlayer("Death.wav"))
					{
						player.PlaySync();
						snakeDead = true;
					}
			

				break;
			case (char)Keys.O:
				speed = 200;
				snakeDead = false;
				break;

			}

		}

		/// <summary>
		/// The updatetimer tick here ,upda  100ms
		/// e snake movement
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void update (double gameTime)
		{
			tickCounter++;
			if (speed < tickCounter * 10) {
				lock (snakeParts) {
					getNewDirection ();

					if (collision ()) {
						player.Play();
						snakeFood.RemoveFirst ();
						Random rand = new Random();
						snakeFood.AddFirst (new SnakeFoodObject (new Rectangle (new Point (rand.Next(50,500), rand.Next(5,500)), new Size (new Point (rand.Next(1,50), rand.Next(1,50))))));


					} else {
						snakeParts.RemoveFirst ();
					}
					if(snakeCollide()){
						speed = 1000000;
					

					}

					snakeParts.AddLast (newDirection);
				}
				tickCounter = 0;
			}
			switch (currentDirection) {
			case "UP":

				break;
			case "DOWN":

				break;
			case "RIGHT":

				break;
			case "LEFT":

				break;
			}
		}

		private bool collision ()
		{	bool foodCollided = false;
			lock (snakeFood) {
				lock (snakeParts) {


					if (snakeFood.First.Value.isColliding (snakeParts.Last.Value)) {
						foodCollided = true;

					}



				}
			}
			return foodCollided;

		}

		private bool snakeCollide(){
			bool snakeIsDead = false;
			lock (snakeParts) {
				foreach (var item in snakeParts) {
					if (newDirection.isColliding(item) ){
						snakeIsDead = true;
					}
				}
			}
			return snakeIsDead;

		}

		/// <summary>
		/// Draw the snakeparts here with the brush 60x
		/// </summary>
		/// <param name="brush">Brush.</param>
		public void draw (System.Drawing.Graphics brush)
		{


			//setupTransform(brush);
			renderObject(brush);
			//restoreTransform(brush);

		}

		public System.Drawing.Rectangle getRectangle ()
		{
			return snakeParts.Last.Value.getRectangle ();
		}

		#endregion

		private void getNewDirection()
		{
			int lenght = snakeParts.Count;
			int xPos = snakeParts.Last.Value.getRectangle().X;
			int yPos = snakeParts.Last.Value.getRectangle().Y;

			switch (currentDirection) 
			{

			case "RIGHT":
				//Console.WriteLine ("snakeMoveRight");
				newDirection = getWrapperGameObject(new Point (xPos + 20, yPos));
				lastDirection = "LEFT";
				break;
			case "LEFT":
				//Console.WriteLine ("SnakeMoveLeft");
				newDirection =  getWrapperGameObject(new Point (xPos - 20, yPos));
				lastDirection = "RIGHT";
				break;
			case "DOWN":
				//Console.WriteLine ("snakeMoveUp");
				newDirection =  getWrapperGameObject(new Point (xPos, yPos + 20));
				lastDirection = "UP";
				break;
			case "UP":
				//snakeMoveDown
				newDirection = getWrapperGameObject( new Point (xPos, yPos - 20));
				lastDirection = "DOWN";
				break;
			default:
				newDirection = getWrapperGameObject( new Point (0, 0));
				break;
			}
		}

		private IGameObject getWrapperGameObject(Point point){
			return new SnakePartObject(new Rectangle(point,new Size(new Point(20,20))));
		}
		void setupTransform (Graphics brush){
			state=brush.Save();		

		}

		void renderObject (Graphics brush){
			lock(snakeParts){

				foreach (var item in snakeFood) {
					item.draw (brush);
				}
				foreach (var item in snakeParts) {
					item.draw (brush);
				}
			}
		}

		void restoreTransform (Graphics brush){
			brush.Restore(state);
		}
	}
}
