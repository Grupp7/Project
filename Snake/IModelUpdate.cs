using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
	public interface IModelUpdate
	{

		void update(object sender, System.Timers.ElapsedEventArgs e);

		double getCounter();

		void setPos(Point p);

		Point getPos();

		void sendKey(Char key);
	}
}

