using System;
using System.Drawing;

namespace Snake{
	public interface IGameObject{

		void passData(GameData newInfo);
		void update(double gameTime);
		void draw(Graphics brush);
		Rectangle getRectangle();
	}
}

