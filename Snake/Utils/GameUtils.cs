using System;
using System.Drawing;

namespace Snake
{
	/// <summary>
	/// Game utils
	/// Contains useful methods to use
	/// in various ocations
	/// </summary>
	public static class GameUtils
	{
		// Global random utility
		public static Random random = new Random();

		/// <summary>
		/// Check whether two objects is colliding with each others 
		/// </summary>
		/// <returns><c>true</c>, if colliding was issued, <c>false</c> otherwise.</returns>
		/// <param name="objA">Object a.</param>
		/// <param name="objB">Object b.</param>
		public static bool isColliding(IGameObject objA,IGameObject objB){

			Rectangle otherRectangle = objB.getRectangle();
			Rectangle thisRectangle = objA.getRectangle();
			bool hasCollided = false;

			bool checkThisRectanglePosWidth = thisRectangle.X < otherRectangle.X + otherRectangle.Width;
			bool checkOtherRectanglePosWidth =thisRectangle.X + thisRectangle.Width > otherRectangle.X;
			bool checkThisRectanglePosHeight = thisRectangle.Y < otherRectangle.Y + otherRectangle.Height;
			bool checkOtherRectanglePosHeight= thisRectangle.Height + thisRectangle.Y > otherRectangle.Y;

			//Checks the collision in the x and y lines
			if ( checkThisRectanglePosWidth  &&
				 checkOtherRectanglePosWidth &&
				 checkThisRectanglePosHeight &&
				 checkOtherRectanglePosHeight) {

				hasCollided = true;
			}
			return hasCollided;
		}

		/// <summary>
		/// Gets the random standard snake food object.
		/// </summary>
		/// <returns>The random snake food object.</returns>
		public static SnakeFoodObject getRandomSnakeFoodObject(){
			int temp = random.Next(20, 150);
			return new SnakeFoodObject (new Rectangle (new Point (random.Next(50,465), random.Next(50,465)), new Size (new Point (temp,temp))));
		}

		/// <summary>
		/// Gets the a specified block object.
		/// </summary>
		/// <returns>The specified block object.</returns>
		/// <param name="locX">Location x.</param>
		/// <param name="locY">Location y.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public static BlockObject getBlockObject(int locX, int locY, int width, int height){

			return new BlockObject (new Rectangle (new Point (locX, locY), new Size (width, height)));
		}
	}

}
