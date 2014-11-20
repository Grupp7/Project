using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
	public class SnakeModel:IModelUpdate
	{

		private double timeElapsed;

		private Point point;

		private Char key;

		public SnakeModel ()
		{


		}

		#region IModelUpdate implementation

		public void update (object sender, System.Timers.ElapsedEventArgs e)
		{

				timeElapsed++;
			switch(this.key){
			case (char)Keys.W:
				point.Y -= 10;

				break;
			case (char)Keys.S:
				point.Y += 10;
			
				break;
			case (char)Keys.A:
				point.X -= 10;

				break;
			case (char)Keys.D:
				point.X += 10;

				break;
			}


		}



		public double getCounter ()
		{
			return timeElapsed;

		}

		public void setPos (Point point)
		{
			this.point = point;
		}

		public System.Drawing.Point getPos ()
		{
			return point;
		}

		public void sendKey (Char key)
		{
			this.key = key;
		}
		#endregion
	}

}