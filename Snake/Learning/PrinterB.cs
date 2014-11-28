using System;

namespace Snake
{
	public class PrinterB:IPrint
	{
		public PrinterB ()
		{
		}

		public void printNow(){

			Console.WriteLine ("Klass B skriker");

		}

		public void printFasterNow(){
			Console.WriteLine ("Klass B skriker SNABBT!!!");

		}
	}
}

