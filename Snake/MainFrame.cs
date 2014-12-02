using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Snake{

	public class MainFrame:Form{

		private IGameModel model;

		public MainFrame(){
			initForm();

			this.KeyPress +=	new KeyPressEventHandler(this.extendedFormKeyPressed);

			model = new GameModel(this.ClientSize);
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
			this.Size = new Size (537, 558);
			this.MaximumSize=new Size (537, 558);
		}

		private void extendedFormKeyPressed (object sender, KeyPressEventArgs e){
			model.updateCurrentKey(e.KeyChar);

		}

		protected override void OnPaint (PaintEventArgs e){
			Invalidate();
			e.Graphics.DrawImageUnscaled(model.getBitmap(), 0, 0);
		}

		protected override void OnSizeChanged (EventArgs e){
			//model.resizeBitmap(this.ClientSize);
			base.OnSizeChanged(e);

		}

		protected override void OnPaintBackground (PaintEventArgs pevent){
		}
	}
}
