using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Snake{

	/// <summary>
	/// Main frame.
	/// </summary>
	public class MainFrame:Form{

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

		public MainFrame(){
			initForm();
			this.components = new System.ComponentModel.Container();
			this.KeyPress +=	new KeyPressEventHandler(this.extendedFormKeyPressed);
			initTestData();
		}

		private void initForm (){
			this.Show();
			//This form is double buffered
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.DoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.UserPaint,
				true);
		}

		private void initTestData (){
		


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

		private void extendedFormKeyPressed (object sender, KeyPressEventArgs e){
			//snakeModel.sendKey (e.KeyChar);
			//test.passData(new GameData(e.KeyChar));
			GameData temp = new GameData();
			temp.key = e.KeyChar;
			temp.point = cameraPosition;
			snakeStuff.passData(temp);
			switch(e.KeyChar){
			case (char)Keys.W:
				cameraPosition.Y -= 10;

				break;
			case (char)Keys.S:
				cameraPosition.Y += 10;

				break;
			case (char)Keys.A:
				cameraPosition.X -= 10;

				break;
			case (char)Keys.D:
				cameraPosition.X += 10;

				break;

			}
		}

		/// <summary>
		/// Tick the specified sender and e.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void tick (object sender, System.EventArgs e){
			snakeStuff.update(5);


		}

		private void rotateAngle (object sender, System.EventArgs e){

			angle += 1;
			if(angle > 359){
				angle = 0;
			}
			test.update(5);


		}

		private void updateBuffer (){
		
			if(backBuffer==null){
				backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			}

			//Init grapichs data
			Graphics g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;
			snakeStuff.draw(g);
			//Matrix 1
			Matrix mx = new Matrix();
			mx.Rotate(angle, MatrixOrder.Append);
			mx.Translate(this.ClientSize.Width / 2 - cameraPosition.X, this.ClientSize.Height / 2 - cameraPosition.Y, MatrixOrder.Append);
			g.Transform = mx;
			g.FillRectangle(Brushes.Red, -100, -100, 200, 200);

			//Matrix 2
			mx = new Matrix();
			mx.Rotate(-angle, MatrixOrder.Append);
			mx.Translate(this.ClientSize.Width / 2, this.ClientSize.Height / 2, MatrixOrder.Append);
			g.Transform = mx;
			g.FillRectangle(Brushes.Green, -75, -75, 149, 149);

			//Matrix 3
			mx = new Matrix();
			mx.Rotate(angle * 2, MatrixOrder.Append);
			mx.Translate(this.ClientSize.Width / 2, this.ClientSize.Height / 2, MatrixOrder.Append);
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

		}

		private void renderUpdate (){
			updateBuffer();
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

		/// <summary>
		/// Raises the paint event.
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnPaint (PaintEventArgs e){
			Console.Clear();
			Invalidate();

			updateGameData();
			renderUpdate();
			putToScreen();
			//Move/copy the backBuffer off to the screen
			//"The Bitmap" object we have painted on

			e.Graphics.DrawImageUnscaled(backBuffer, 0, 0);
		}

		/// <summary>
		/// Raises the size changed event.
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnSizeChanged (EventArgs e){
			if(backBuffer!=null){
				backBuffer.Dispose();
				backBuffer = null;
			}
			base.OnSizeChanged(e);
		}

		// No background paint
		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <param name="pevent">Pevent.</param>
		protected override void OnPaintBackground (PaintEventArgs pevent){
		}
	}
}

