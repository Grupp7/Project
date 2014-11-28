using System;

namespace Snake
{
	public class PrinterA:IPrint
	{
		public PrinterA ()
		{
		}
		public void printNow(){
			Console.WriteLine ("Klass A skriker");

		}

		public void printFaster(){
			Console.WriteLine ("Klass A skriker SNABBT!!!");

		}
	}
}

