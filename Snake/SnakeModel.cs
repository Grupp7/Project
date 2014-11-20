using System;
using System.Threading;

namespace Snake
{
	public class SnakeModel:IModelUpdate
	{
		private volatile bool shouldStop;
		private double timeElapsed;
		public SnakeModel ()
		{
			shouldStop = false;

		}

		#region IModelUpdate implementation

		public void update ()
		{
			while (!shouldStop) {
				timeElapsed++;


				Thread.Sleep (20);
			}
			Console.WriteLine ("worker Snakethread: terminating gracefully.");
		}

		public void requestStop ()
		{
			shouldStop = true;
		}


		public double getCounter ()
		{
			return timeElapsed;

		}
		#endregion
	}
}

