using System;
using System.Windows.Forms;
using System.Drawing;


namespace Snake{

	/// <summary>
	/// Main frame.
	/// The responsibility for this class is simply drawing the Bitmap from the 
	/// current GameModel to the screen
	/// </summary>
	public class MainFrame:Form{

		// The current gameModel that it will get the Bitmap from
		private IGameModel model;

		/// <summary>
		/// Creates the form and sets up the keyeventhandler
		/// Create the specified GameModel
		/// </summary>
		public MainFrame(){
			initForm();

			this.KeyPress +=	new KeyPressEventHandler(this.extendedFormKeyPressed);

			model = new GameModel(this.ClientSize);
		}

		/// <summary>
		/// Inits the settings for this Form.
		/// </summary>
		private void initForm (){
			this.Show();

			//This form is double buffered
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.DoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.UserPaint,
				true);

			//The specified size
			this.Size = new Size (537, 558);
			//Set min and max Size for the game
			this.MaximumSize=new Size (537, 558);
			this.MinimumSize=new Size (537, 558);

			//TODO:Make it possible to set the specified size of the screen 
		}

		/// <summary>
		/// Extendeds the form key pressed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void extendedFormKeyPressed (object sender, KeyPressEventArgs e){
			model.updateCurrentKey(e.KeyChar);

		}

		/// <summary>
		/// Raises the paint event.
		/// Draws the Bitmap from the current IGameModel
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnPaint (PaintEventArgs e){
			Invalidate();
			//Gets the Bitmap from the current GameModel
			e.Graphics.DrawImageUnscaled(model.getBitmap(), 0, 0);
			if(model.getStates().Contains(GameState.ExitGame)){
				this.Dispose ();
				this.Close ();
			}
		}

		/// <summary>
		/// Raises the size changed event.
		/// Updates the size of the bitmap to the model
		/// </summary>
		/// <param name="e">E.</param>
		protected override void OnSizeChanged (EventArgs e){
			//TODO: needs to implement proper sizes for the model'
			//Mainframe
			//Some bug occured when resizing implicit
			//model.resizeBitmap(this.ClientSize);
			base.OnSizeChanged(e);

		}

		/// <summary>
		/// Raises the paint background event.
		/// Draws nothing since we want to have a clear
		/// background
		/// </summary>
		/// <param name="pevent">Pevent.</param>
		protected override void OnPaintBackground (PaintEventArgs pevent){
		}
	}
}
