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
	public class MainFrameTest:Form{

		private ModelTest model;

		public MainFrameTest(ModelTest model){
			initForm();
			this.model = model;
			model = new ModelTest(this.ClientSize);
			this.KeyPress +=	new KeyPressEventHandler(this.extendedFormKeyPressed);
		}
		public MainFrameTest(){
			initForm();
			model = new ModelTest(this.ClientSize);
			this.KeyPress +=	new KeyPressEventHandler(this.extendedFormKeyPressed);
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

		private void extendedFormKeyPressed (object sender, KeyPressEventArgs e){
			model.updateCurrentKey(e.KeyChar);
		}

		/// <summary>
		/// Raises the paint event.
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnPaint (PaintEventArgs e){
			Invalidate();
			e.Graphics.DrawImageUnscaled(model.getBitmap(), 0, 0);
		}

		/// <summary>
		/// Raises the size changed event.
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnSizeChanged (EventArgs e){
			model.resizeBitmap(this.ClientSize);
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

