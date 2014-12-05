using System;
using System.Windows.Forms;

namespace Snake{

	/// <summary>
	/// Main program starts the game
	/// Initiate the Form to run on a thread
	/// </summary>
	public class MainProgram{
		[STAThread]
		public static void Main (string[] args){
			Application.Run(new MainFrame ());
		}
	}
}