using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Snake
{

	public class Snake:IGameObject
	{

		GraphicsState state;
		private LinkedList<Point> snakeBody;
		private int currentDirection = 1;
		private Point newDirection =new Point (100,100);
		private int turnCounter;
		private bool grow;
		private volatile bool isAvailable;

		public Snake ()
		{
			snakeBody = new LinkedList<Point> ();

			snakeBody.AddFirst ( new Point (100, 140));
			snakeBody.AddFirst ( new Point (100, 120));
			snakeBody.AddFirst ( new Point (100, 100));

			isAvailable = true;

		}


		#region IGameObject implementation

		/// <summary>
		/// The current key pressed that will be handeled
		/// </summary>
		/// <param name="newInfo">New info.</param>
		public void passData (GameData newInfo)
		{
			switch(newInfo.key){
			case (char)Keys.W:
				currentDirection =4;

				break;
			case (char)Keys.S:
				currentDirection =3;

				break;
			case (char)Keys.A:
				currentDirection =2;

				break;
			case (char)Keys.D:
				currentDirection =1;
			
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
			lock(snakeBody){
				
		
			//if(isAvailable){
				//isAvailable = false;
				snakeBody.RemoveFirst ();
				getNewDirection ();
				snakeBody.AddLast(newDirection);
				turnCounter++;
				if(turnCounter >10){
					if(currentDirection ==4){
						currentDirection = 0;
					}
					currentDirection++;
					turnCounter = 0;
				}
			//}
		
			//isAvailable = true;

			}
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
			return new Rectangle(0,0,1,1);
		}

		#endregion

		private void getNewDirection()
		{
			int lenght = snakeBody.Count;
			switch (currentDirection) 
			{
			case 1:
				//Console.WriteLine ("snakeMoveRight");
				newDirection = new Point (snakeBody.Last.Value.X + 20, snakeBody.Last.Value.Y);
				break;
			case 2:
				//Console.WriteLine ("SnakeMoveLeft");
				newDirection =  new Point (snakeBody.Last.Value.X - 20, snakeBody.Last.Value.Y);
				break;
			case 3:
				//Console.WriteLine ("snakeMoveUp");
				newDirection =  new Point (snakeBody.Last.Value.X, snakeBody.Last.Value.Y + 20);
				break;
			case 4:
				//snakeMoveDown
				newDirection =  new Point (snakeBody.Last.Value.X, snakeBody.Last.Value.Y - 20);
				break;
			default:
				newDirection =  new Point (0, 0);
				break;
			}
		}
		void setupTransform (Graphics brush){
			state=brush.Save();		

		}

		void renderObject (Graphics brush){
			lock(snakeBody){
				//if(isAvailable){
					//isAvailable = false;
					SolidBrush myBrush = new SolidBrush(Color.Green);


//			for (int i = 0; i < snakeBody.Count; i++) 
//			{
//				//Rectangle cir = new Rectangle (snakeBody[i].X,snakeBody[i].Y, 20, 20);
//				//brush.FillEllipse (myBrush, cir);
//
//			}
					foreach(var item in snakeBody){

						Rectangle cir = new Rectangle(item.X, item.Y, 20, 20);
						brush.FillEllipse(myBrush, cir);
					}
				//}
				//isAvailable = true;
			}
		}

		void restoreTransform (Graphics brush){
			brush.Restore(state);
		}
	}
}

