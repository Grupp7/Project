using System;
using System.Drawing;

namespace Snake{
	public interface IGameModel{
	
		Bitmap getBitmap();

		void resizeBitmap(Size clientSize);

		void updateCurrentKey(char key);

	}
}

