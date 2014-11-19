using System;
using System.Windows.Forms;
using System.Drawing;

namespace Snake{
	public class MainFrame{
		bool gameIsRunning;
		private Form mainScreen;
		public MainFrame(){
			mainScreen = new Form();
			gameIsRunning = true;
			testDraw();

		}

		private void gameLoop (){
			while(gameIsRunning){
				updateGameData();
				renderData();
				putToScreen();
		
			}

		}

		void updateGameData (){
			throw new NotImplementedException();
		}

		void renderData (){
			throw new NotImplementedException();
		}

		void putToScreen (){
			throw new NotImplementedException();
		}

		public void testDraw (){
			mainScreen.CreateGraphics();

		}

		public Form getMainScreen(){
			return mainScreen;
		}
	}
}

