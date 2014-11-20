using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;

namespace Snake{
	public class MainProgram{

		[STAThread]
		public static void Main (string[] args){


			Application.Run(new MainFrame ());
			//WorkerTheadExample.testarStuff ();

		}

	}
}

