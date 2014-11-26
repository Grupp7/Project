using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using Snake;

namespace Snake{

	/// <summary>
	/// Main program.
	/// </summary>
	public class MainProgram{

		[STAThread]
		public static void Main (string[] args){
			Application.Run(new MainFrame ());
		}

	}
}

