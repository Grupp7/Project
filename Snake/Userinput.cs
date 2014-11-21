using System;

namespace Snake
{

	public class Userinput
	{
		// Current keypress for move
		int currentKey = 3;
		//current key pressed
		//int key = keyPress;
		// Map Coordinates
		public int x = 0;
		public int y = 0;
		// Main

		// program
		public Userinput ()
		{
			keyPress();
			if (snakeDirection() == true) {


			} else {
			//	currentKey = key;
			}
			move ();

		}
		// check what direction
		public bool snakeDirection()
		{
			bool Direction = false;

//
//			if (key == 0 || key == currentKey) {
//				Direction = true;
//			} else {
//				Direction = false;
//			}

			return Direction;
		}


		/// <summary>
		/// Keies the press.
		/// </summary>
		/// <returns>The press.</returns>
		public int keyPress()
		{
			string keypressed;
			int key= 0;
			const int rightKey = 1 , leftKey= 2 , upKey= 3 , downKey= 4;

			keypressed = Console.ReadLine ();
//
//			switch(keypressed)
//			{
//			case ConsoleKey.LeftArrow:
//				key = leftKey;
//				break;
//			case ConsoleKey.RightArrow:
//				key = rightKey;
//				break;
//			case ConsoleKey.UpArrow:
//				key = upKey;
//				break;
//			case ConsoleKey.DownArrow:
//				key = downKey;
//				break;
//			}

			return key;
		}
		// move snake
		public void move()
		{
			switch (currentKey) {
			case 1:
				Console.WriteLine ("snakeMoveRight");
				x = x + 1;
				break;
			case 2:
				Console.WriteLine ("SnakeMoveLeft");
				x = x - 1;
				break;
			case 3:
				Console.WriteLine ("snakeMoveUp");
				y = y + 1;
				break;
			case 4:
				Console.WriteLine ("snakeMoveDown");
				y = y - 1;
				break;
			}

		}
	}
}

