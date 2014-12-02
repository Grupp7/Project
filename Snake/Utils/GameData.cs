using System;
using System.Drawing;
using System.Windows.Forms;

//TODO: Design better interface for alla gameobjects properties that we want 
// to use
namespace Snake
{
	public class GameData
	{
		public char key;

		public Point point;

		public GameState state;
		public GameData(){

		}
		public GameData(GameState state){
			this.state = state;

		}
	}

}

