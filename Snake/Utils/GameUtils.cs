using System;
using System.Drawing;

namespace Snake
{
	/// <summary>
	/// Game utils.
	/// </summary>
	public static class GameUtils
	{
		public static Random random = new Random();

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

		public static SnakeFoodObject getRandomSnakeFoodObject(){
			int temp = random.Next(20, 150);
			return new SnakeFoodObject (new Rectangle (new Point (random.Next(50,465), random.Next(50,465)), new Size (new Point (temp,temp))));
		}

		public static BlockObject getBlockObject(int locX, int locY, int width, int height){

			return new BlockObject (new Rectangle (new Point (locX, locY), new Size (width, height)));
		}
	}

}
