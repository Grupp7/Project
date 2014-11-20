using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace Snake
{
	public class MainFrame
	{

		//Specifik name needed for init
		private System.Timers.Timer frameUpdateTimer;
		private IModelUpdate snakeModel;
		private double timeElapsed;

		public MainFrame ()
		{
			//Init frameTimer
			frameUpdateTimer = new System.Timers.Timer (16); // behövde tydligen skriva hela namnet
			frameUpdateTimer.Elapsed += renderToBitmap;
			frameUpdateTimer.Enabled = true;

			snakeModel = new SnakeModel ();
			Thread snakeThread = new Thread (snakeModel.update); 

			// Start the worker thread for snake
			snakeThread.Start ();
		

			// Loop until worker snakeThread activates. 
			while (!snakeThread.IsAlive);


			// Put the main thread to sleep for 1 millisecond to 
			// allow the worker thread to do some work:
			Thread.Sleep (2000);

			// Request that the worker thread stop itself:
			snakeModel.requestStop ();
		

			// Use the Join method to block the current thread  
			// until the object's thread terminates.
			snakeThread.Join ();

			frameUpdateTimer.Enabled = false;
			Console.WriteLine ("main thread: Worker MAINThread has terminated.");
			Console.ReadLine ();
		}





		private void renderToBitmap (object sender, System.Timers.ElapsedEventArgs e)
		{
			Console.Clear ();
			updateGameData ();
			renderUpdate ();
			putToScreen ();
		}

		private void renderUpdate ()
		{
			Console.WriteLine ("RENDER BITMAP");
		}

		private void putToScreen ()
		{
			Console.WriteLine ("Off to SCREEN");
			Console.WriteLine ("MAINFRAMECOUNTER: " + timeElapsed + " SNAKECOUNTER: " + snakeModel.getCounter ());
		}

		private void updateGameData ()
		{
			timeElapsed++;
			Console.WriteLine ("Update gameData: "+timeElapsed);
		}
	}
}

