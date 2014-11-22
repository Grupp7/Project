using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
	public class GameData
	{
		public char key;

		public Point point;

		public GameData(){

		}
		public GameData(char keyChar){
		
			switch(keyChar){
			case (char)Keys.W:
				point.Y -= 5;

				break;
			case (char)Keys.S:
				point.Y += 5;

				break;
			case (char)Keys.A:
				point.X -= 5;

				break;
			case (char)Keys.D:
				point.X += 5;

				break;
			}
		}
	}

}

