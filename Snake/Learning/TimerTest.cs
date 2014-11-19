using System;
using System.Timers;
using System.Threading;
namespace Snake
{  
	public class ShowDirektion
	{


		private volatile bool shouldStop = false;

		public void snakeDirektion()
		{

			while (!shouldStop)
			{
				Console.Clear();
				switch(DataStuff.Direktion.Key)
				{
				case ConsoleKey.LeftArrow:
					Console.WriteLine("{0} vänster", DataStuff.counter);
					break;
				case ConsoleKey.RightArrow:
					Console.WriteLine("{0} höger", DataStuff.counter);
					break;
				case ConsoleKey.UpArrow:
					Console.WriteLine("{0} upp", DataStuff.counter);
					break;
				default:
					Console.WriteLine("{0} ner", DataStuff.counter) ;
					break;
				}
				Thread.Sleep(20);
			}
			Console.WriteLine("worker thread: terminating gracefully.");
		}
		public void RequestStop()
		{
			shouldStop = true;
		}

	}
	public class WorkerTheadExample
	{

		private static System.Timers.Timer aTimer; // behövde tydligen skriva hela namnet


		public static void testarStuff()
		{

			aTimer = new System.Timers.Timer(200); // behövde tydligen skriva hela namnet
			aTimer.Elapsed += OnTimedEvent;
			aTimer.Enabled = true;


			// Create the thread object. This does not start the thread.
			ShowDirektion showObject = new ShowDirektion(); 
			ReadDirektion readObject = new ReadDirektion();

			Thread snakeThread = new Thread(showObject.snakeDirektion); 
			Thread readThread = new Thread(readObject.readDirektion);
			// Start the worker thread.
			snakeThread.Start();
			readThread.Start ();

			Console.WriteLine("Starting read and snake thread...");

			// Loop until worker thread activates. 
			while (!snakeThread.IsAlive);
			while (!readThread.IsAlive);

			// Put the main thread to sleep for 1 millisecond to 
			// allow the worker thread to do some work:
			Thread.Sleep(20000);

			// Request that the worker thread stop itself:
			showObject.RequestStop();
			readObject.RequestStop();

			// Use the Join method to block the current thread  
			// until the object's thread terminates.
			snakeThread.Join();
			readThread.Join();
			Console.WriteLine("main thread: Worker thread has terminated.");
		}
		private static void OnTimedEvent(Object source, ElapsedEventArgs e)
		{

			DataStuff.counter++;
		}

	}
	public class ReadDirektion
	{

		private volatile bool shouldStop = false;


		public void readDirektion()
		{
			while (!shouldStop)
			{
				DataStuff.Direktion = Console.ReadKey();


			}
			Console.WriteLine("worker thread: terminating gracefully.");
		}
		public void RequestStop()
		{
			shouldStop = true;
		}
	}
	public static class DataStuff // flytta data mella klasser eller något :D
	{
		public static ConsoleKeyInfo Direktion; 
		public static int  counter;
	}
}

