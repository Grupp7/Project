using System;

namespace Snake
{
	public interface IModelUpdate
	{

		void update();
		void requestStop();
		double getCounter();
	}
}

