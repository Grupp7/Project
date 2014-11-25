using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Snake{
	public class ModelTest{
		#region Forms and timers
		//Form specific stuff like graphics
		private volatile Bitmap backBuffer;
		private IContainer components;
		//Specifik name needed for init
		//Wondering if we only need one timer and
		//put local speed in each gameobject
		//that would make more sense to
		// have one timer and method for each object
		private System.Timers.Timer rotateUpdateTimer;
		private System.Timers.Timer snakeUpdateTimer;
		private System.Timers.Timer snakeTimer;

		public char keyPressed;
		public Size clientSize;
		#endregion

		//GameControllers
		private System.Windows.Forms.Timer keyInputTimer;
		private double timeElapsed;
		private Point cameraPosition;

		//IGameObjects
		RotateSquare test = new RotateSquare();
		Snake snakeStuff = new Snake();

		//Testdata and stuff
		private float angle;

		public ModelTest(Size clientSize){
			this.components = new System.ComponentModel.Container();
			this.clientSize = clientSize;

			initTestData();
		}


		private void initTestData (){
			cameraPosition.X = snakeStuff.cameraPosition.X;
			cameraPosition.Y = snakeStuff.cameraPosition.Y;



			//Init frameTimer
			rotateUpdateTimer = new System.Timers.Timer(32); // behövde tydligen skriva hela namnet
			rotateUpdateTimer.Elapsed += rotateAngle;
			rotateUpdateTimer.Enabled = true;
			test.location.X = 0;
			test.location.Y = 0;
			//			test.angle = 0;
			GameData temp = new GameData();
			temp.point.X = 700;
			temp.point.Y = 700;
			test.passData(temp);
			snakeTimer = new  System.Timers.Timer(400);
			snakeTimer.Elapsed += tick;
			snakeTimer.Enabled = true;
			//Init frameTimer
			snakeUpdateTimer = new System.Timers.Timer(32); // behövde tydligen skriva hela namnet
			snakeUpdateTimer.Enabled = true;
			//
			//			snakeModel = new SnakeModel ();
			//			snakeUpdateTimer.Elapsed += snakeModel.update;
			//
			//keyInputTimer init
			this.keyInputTimer = new System.Windows.Forms.Timer(this.components);
			this.keyInputTimer.Enabled = true;
			this.keyInputTimer.Tick += new System.EventHandler(this.rotateAngle);
			//keyInputTimer.Interval = 1;

		}

		public void updateCurrentKey(char key){
			GameData temp = new GameData();
			keyPressed = key;
			temp.key = key;
			snakeStuff.passData(temp);

		}
	
		/// <summary>
		/// Tick the specified sender and e.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void tick (object sender, System.EventArgs e){
			snakeStuff.update(5);

			cameraPosition.X = snakeStuff.cameraPosition.X;
			cameraPosition.Y = snakeStuff.cameraPosition.Y;

		}

		private void rotateAngle (object sender, System.EventArgs e){

			angle += 1;
			if(angle > 359){
				angle = 0;
			}
			test.update(5);


		}

		public Bitmap getBitmap (){

			if(backBuffer==null){
				backBuffer = new Bitmap(this.clientSize.Width, this.clientSize.Height);
			}

			//Init grapichs data
			Graphics g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			snakeStuff.draw(g);
			//Matrix 1
			Matrix mx = new Matrix();
			mx.Rotate(angle, MatrixOrder.Append);
			mx.Translate(this.clientSize.Width / 2 - cameraPosition.X, this.clientSize.Height / 2 - cameraPosition.Y, MatrixOrder.Append);
			g.Transform = mx;
			g.FillRectangle(Brushes.Red, -100, -100, 200, 200);

			//Matrix 2
			mx = new Matrix();
			mx.Rotate(-angle, MatrixOrder.Append);
			mx.Translate(this.clientSize.Width / 2, this.clientSize.Height / 2, MatrixOrder.Append);
			g.Transform = mx;
			g.FillRectangle(Brushes.Green, -75, -75, 149, 149);

			//Matrix 3
			mx = new Matrix();
			mx.Rotate(angle * 2, MatrixOrder.Append);
			mx.Translate(this.clientSize.Width / 2, this.clientSize.Height / 2, MatrixOrder.Append);
			g.Transform = mx;
			g.FillRectangle(Brushes.Blue, -50, -50, 100, 100);

			//			//Matrix 3
			//			mx = new Matrix();
			//			mx.Rotate((-angle * 4), MatrixOrder.Append);
			//			float temp = this.ClientSize.Width / 2 +snakeModel.getPos().X ;
			//			float temp2 = this.ClientSize.Height / 2  +snakeModel.getPos().Y;
			//			mx.Translate(temp, temp2, MatrixOrder.Append);
			//			g.Transform = mx;
			//			g.FillRectangle(Brushes.PowderBlue, -50, -50, 100, 100);
			//
			//
			//			// Create string to draw.
			//			String drawString = string.Format ("M: {0} S: {1}", timeElapsed, snakeModel.getCounter ());

			// Create font and brush.
			Font drawFont = new Font("Arial", 16);
			SolidBrush drawBrush = new SolidBrush(Color.Black);

			// Create rectangle for drawing. 
			float x = 0;
			float y = 0;
			float width = 200.0F;
			float height = 50.0F;
			RectangleF drawRect = new RectangleF(x, y, width, height);

			// Draw rectangle to screen.
			Pen blackPen = new Pen(Color.Black);
			g.DrawRectangle(blackPen, x, y, width, height);

			// Set format of string.
			StringFormat drawFormat = new StringFormat();
			drawFormat.Alignment = StringAlignment.Center;

			// Draw string to screen.
			//			g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);


			test.draw(g);
			g.Dispose();
			return backBuffer;
		

		}



		public void resizeBitmap (Size clientSize){
			if(backBuffer!=null){
				backBuffer.Dispose();
				backBuffer = null;
			}
			this.clientSize = clientSize;
		}

		private void renderUpdate (){
			getBitmap();
			Console.WriteLine("RENDER BITMAP");
		}

		private void putToScreen (){

			Console.WriteLine("Off to SCREEN");
			//			Console.WriteLine ("MAINFRAMECOUNTER: " + timeElapsed + " SNAKECOUNTER: " + snakeModel.getCounter ());
		}

		private void updateGameData (){
			timeElapsed++;
			Console.WriteLine("Update gameData: " + timeElapsed);
		}

	}
}

