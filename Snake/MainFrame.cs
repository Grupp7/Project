using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Snake
{
	public class MainFrame:Form
	{

		//Specifik name needed for init
		private System.Timers.Timer rotateUpdateTimer;
		private System.Timers.Timer snakeUpdateTimer;
		private IModelUpdate snakeModel;
		private double timeElapsed;
		private System.Windows.Forms.Timer keyInputTimer;
		private float angle;
		private IContainer components;

		private volatile Bitmap backBuffer;

		public MainFrame ()
		{
			this.Show ();
			this.components = new System.ComponentModel.Container();
			//Init frameTimer
			rotateUpdateTimer = new System.Timers.Timer (32); // behövde tydligen skriva hela namnet
			rotateUpdateTimer.Elapsed += rotateAngle;
			rotateUpdateTimer.Enabled = true;

			//Init frameTimer
			snakeUpdateTimer = new System.Timers.Timer (32); // behövde tydligen skriva hela namnet
			snakeUpdateTimer.Enabled = true;

			snakeModel = new SnakeModel ();
			snakeUpdateTimer.Elapsed += snakeModel.update;

			//keyInputTimer init
			this.keyInputTimer = new System.Windows.Forms.Timer(this.components);
			this.keyInputTimer.Enabled = true;
			this.keyInputTimer.Tick += new System.EventHandler(this.rotateAngle);
			//keyInputTimer.Interval = 1;
			this.KeyPress +=	new KeyPressEventHandler(this.extendedFormKeyPressed);

			//This form is double buffered
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.DoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.UserPaint,
				true);

		}

		private void extendedFormKeyPressed (object sender, KeyPressEventArgs e){
			snakeModel.sendKey (e.KeyChar);


		}




		private void rotateAngle (object sender, System.EventArgs e){

			angle += 1;
			if(angle > 359){
				angle = 0;
			}
		

		}
		private void updateBuffer (){
		
			if(backBuffer==null){
				backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			}

			//Init grapichs data
			Graphics g = Graphics.FromImage(backBuffer);
			g.Clear(Color.White);      
			g.SmoothingMode = SmoothingMode.AntiAlias;

			//Matrix 1
			Matrix mx = new Matrix();
			mx.Rotate(angle, MatrixOrder.Append);
			mx.Translate(this.ClientSize.Width / 2, this.ClientSize.Height / 2, MatrixOrder.Append);
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

			//Matrix 3
			mx = new Matrix();
			mx.Rotate((-angle * 4), MatrixOrder.Append);
			float temp = this.ClientSize.Width / 2 +snakeModel.getPos().X ;
			float temp2 = this.ClientSize.Height / 2  +snakeModel.getPos().Y;
			mx.Translate(temp, temp2, MatrixOrder.Append);
			g.Transform = mx;
			g.FillRectangle(Brushes.PowderBlue, -50, -50, 100, 100);


			// Create string to draw.
			String drawString = "M: " + timeElapsed + " S: " + snakeModel.getCounter ();

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
			g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);
			g.Dispose();

		}
		private void renderUpdate ()
		{
			updateBuffer ();
			Console.WriteLine ("RENDER BITMAP");
		}

		private void putToScreen ()
		{

			Console.WriteLine ("Off to SCREEN");
			Console.WriteLine ("MAINFRAMECOUNTER: " + timeElapsed + " SNAKECOUNTER: " + snakeModel.getCounter ());
		}

		private void updateGameData ()
		{
			timeElapsed++;
			Console.WriteLine ("Update gameData: "+timeElapsed);
		}
		protected override void OnPaint (PaintEventArgs e){
			Console.Clear ();
			Invalidate();

			updateGameData ();
			renderUpdate ();
			putToScreen ();
			//Move/copy the backBuffer off to the screen
			//"The Bitmap" object we have painted on

			e.Graphics.DrawImageUnscaled(backBuffer, 0, 0);
		}
		protected override void OnSizeChanged (EventArgs e){
			if(backBuffer!=null){
				backBuffer.Dispose();
				backBuffer = null;
			}
			base.OnSizeChanged(e);
		}
		// No background paint
		protected override void OnPaintBackground (PaintEventArgs pevent){
		}


	
	}
}

