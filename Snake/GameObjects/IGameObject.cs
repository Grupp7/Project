using System;
using System.Drawing;

namespace Snake{
	/// <summary>
	/// I game object.
	/// </summary>
	public interface IGameObject{

		/// <summary>
		/// Passes the data.
		/// </summary>
		/// <param name="newInfo">New info.</param>
		void passData(GameData newInfo);

		/// <summary>
		/// Update the specified gameTime.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		/// 
		void update(double gameTime);
		/// <summary>
		/// Draw the specified brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
		void draw(Graphics brush);

		/// <summary>
		/// Gets the rectangle.
		/// </summary>
		/// <returns>The rectangle.</returns>
		Rectangle getRectangle();
	}
}

