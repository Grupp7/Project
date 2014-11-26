using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace Snake{
	public class GameModel:IGameModel{
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

		public GameModel(Size clientSize){
			this.components = new System.ComponentModel.Container();
			tickTimer = new  System.Timers.Timer(10);
			tickTimer.Elapsed += tick;
			tickTimer.Enabled = true;
			this.clientSize = clientSize;
			gameObjects = new List<IGameObject>();
			initTestData();
		}

		private void initTestData (){
			square = new RotateSquare();
			snake = new Snake();
			//gameObjects.Add(snake);
			//gameObjects.Add(square);
			GameData temp = new GameData();
			temp.point.X = 700;
			temp.point.Y = 700;
			square.passData(temp);

			for (int i = 0; i < 500; i+=50) {
				gameObjects.Add (new BlockObject (new Rectangle (new Point (0, i), new Size (50, 20))));
				gameObjects.Add (new BlockObject (new Rectangle (new Point (500, i), new Size (50, 20))));
			}
			for (int i =0; i < 500; i+=20) {
				gameObjects.Add (new BlockObject (new Rectangle (new Point (i, 0), new Size (20, 20))));
				gameObjects.Add (new BlockObject (new Rectangle (new Point (i,500), new Size (20, 20))));

			}
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
				if(GameUtils.isColliding(item,snake)){
					GameData temp = new GameData();
					temp.key = (char)Keys.P;
					snake.passData (temp);
					//	gameObjects.Add(new BlockObject (new Rectangle (new Point (160, 160), new Size (20, 20))));
					//Console.WriteLine ("as");
				}

			}
			snake.update (7);
		}

		public Bitmap getBitmap (){
			if(backBuffer==null){
				backBuffer = new Bitmap(this.clientSize.Width, this.clientSize.Height);
			}
			renderGameObjects();
			return backBuffer;
		}

		private void renderGameObjects (){
			Graphics g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			foreach(var item in gameObjects){
				item.draw(g);
			}
			snake.draw (g);
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
