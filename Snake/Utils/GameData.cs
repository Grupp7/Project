using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
	/// <summary>
	/// Game data.
	/// This class contains information that will
	/// be passed in the IGameObject interface
	///  TODO: This design needs to be updated to a better
	///  idea. Its bad right now.
	/// </summary>
	public class GameData
	{
		public char key;
		public int score;
		public Point point;

		public GameState state;
		public GameData(){

		}

		public GameData(GameState state){
			this.state = state;

		}
	}

}

