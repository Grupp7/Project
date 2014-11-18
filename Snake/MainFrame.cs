using System;
using System.Windows.Forms;
namespace Snake
{
	public class MainFrame
	{
		bool gameIsRunning;
	
		public MainFrame ()
		{
		}

		private void gameLoop(){
			while(gameIsRunning){
				updateGameData ();
				renderData ();
				putToScreen ();
		
			}

		}

		void updateGameData ()
		{
			throw new NotImplementedException ();
		}

		void renderData ()
		{
			throw new NotImplementedException ();
		}

		void putToScreen ()
		{
			throw new NotImplementedException ();
		}
	}
}

