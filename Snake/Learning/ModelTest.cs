using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace Snake{
	public class ModelTest:IGameModel{
		#region ModelConfig
		private Bitmap backBuffer;
		private IContainer components;
		private System.Timers.Timer tickTimer;
		private char keyPressed;
		private Size clientSize;
		#endregion
		#region IGameObjects
		private List<IGameObject> gameObjects;
		private IGameObject square;
		private IGameObject snake;
		#endregion
	
		public ModelTest(Size clientSize){
			this.components = new System.ComponentModel.Container();
			tickTimer = new  System.Timers.Timer(32);
			tickTimer.Elapsed += tick;
			tickTimer.Enabled = true;
			this.clientSize = clientSize;
			gameObjects = new List<IGameObject>();
		    initTestData();
		}
			
		private void initTestData (){
			square = new RotateSquare();
			snake = new Snake();
			gameObjects.Add(snake);
			gameObjects.Add(square);
			GameData temp = new GameData();
			temp.point.X = 700;
			temp.point.Y = 700;
			square.passData(temp);
		}

		public void updateCurrentKey(char key){
			GameData temp = new GameData();
			keyPressed = key;
			temp.key = key;
			snake.passData(temp);
		}
	
		private void tick (object sender, System.EventArgs e){
			foreach(var item in gameObjects){
				item.update(5);
			}
		}

		public Bitmap getBitmap (){
			if(backBuffer==null){
				backBuffer = new Bitmap(this.clientSize.Width, this.clientSize.Height);
			}
			renderGameObjects();
			return backBuffer;
		}

		void renderGameObjects (){
			Graphics g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			foreach(var item in gameObjects){
				item.draw(g);
			}
			g.Dispose();
		}

		public void resizeBitmap (Size clientSize){
			if(backBuffer!=null){
				backBuffer.Dispose();
				backBuffer = null;
			}
			this.clientSize = clientSize;
		}
	}
}

