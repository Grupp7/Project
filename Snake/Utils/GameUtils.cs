using System;
using System.Drawing;

namespace Snake
{
	public static class GameUtils
	{


		public static bool isColliding(IGameObject objA,IGameObject objB){

			Rectangle otherRectangle = objB.getRectangle();
			Rectangle thisRectangle = objA.getRectangle();
			bool hasCollided = false;

			if (thisRectangle.X < otherRectangle.X + otherRectangle.Width &&
				thisRectangle.X + thisRectangle.Width > otherRectangle.X &&
				thisRectangle.Y < otherRectangle.Y + otherRectangle.Height &&
				thisRectangle.Height + thisRectangle.Y > otherRectangle.Y) {
				hasCollided = true;
			}
			return hasCollided;
		}
	}
}
