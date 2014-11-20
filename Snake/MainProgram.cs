using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Snake{
	public class MainProgram{

		[STAThread]
		public static void Main (string[] args){

			Form main = new MainFrame().getMainScreen();
			Application.Run(new ExtendedForm());
			WorkerTheadExample.testarStuff ();
		}

	}
}

