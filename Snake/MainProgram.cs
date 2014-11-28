using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using Snake;
using System.Collections.Generic;

namespace Snake{

	/// <summary>
	/// Main program.
	/// </summary>
	public class MainProgram{

		[STAThread]
		public static void Main (string[] args){
			//Application.Run(new MainFrame ());
			IPrint klassA = new PrinterA ();
			IPrint klassB = new PrinterB ();


			klassA.printNow ();
		
			klassB.printNow ();
	

			List<IPrint> list = new List<IPrint> ();
			list.Add (klassA);
			list.Add (klassB);

			foreach (var item in list) {
				item.printNow ();
			}
			while (true) {

			}

		}

	}
}

