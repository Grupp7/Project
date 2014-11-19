using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Snake{
	public class ExtendedForm:Form{

		private Timer frameRateTimer;
		private IContainer components;
		private Bitmap backBuffer;
		private int angle;

		public ExtendedForm(){
			this.Show();
			this.components = new System.ComponentModel.Container();

			//Timer init
			this.frameRateTimer = new System.Windows.Forms.Timer(this.components);
			this.frameRateTimer.Enabled = true;
			this.frameRateTimer.Tick += new System.EventHandler(this.rotateAngle);
		}

		private void rotateAngle (object sender, System.EventArgs e){
			angle += 3;
			if(angle > 359){
				angle = 0;
			}
			Invalidate();
		}
			
		private void updateBuffer(){
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

			g.Dispose();

		}
		protected override void OnPaint (PaintEventArgs e){
			updateBuffer();
			//Move/copy the backBuffer off to the screen
			//"The Bitmap" object we have painted on
			e.Graphics.DrawImageUnscaled(backBuffer,0, 0);
		}
		// No background paint
		protected override void OnPaintBackground (PaintEventArgs pevent){
		}

		protected override void OnSizeChanged (EventArgs e){
			if(backBuffer!=null){
				backBuffer.Dispose();
				backBuffer = null;
			}
			base.OnSizeChanged(e);
		}
	}
}

