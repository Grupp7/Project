using System;
using System.Drawing;
using System.Collections.Generic;

namespace Snake{
	/// <summary>
	/// The interface that model 
	/// will use to update every '
	/// specific implementation of 
	/// the gameobjects
	/// </summary>
	public interface IGameObject{

		/// <summary>
		/// Passes the new states and gamedata
		/// to the gameobject
		/// </summary>
		/// <param name="newInfo">New info.</param>
		void passData(GameData newInfo);

		/// <summary>
		/// Update the specified gameTime to the gameObject.
		/// And let it know that time has passed so it 
		/// can do its internal update
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		/// 
		void update(double gameTime);

		/// <summary>
		/// Draw the specified appearance to the brush.
		/// </summary>
		/// <param name="brush">Brush.</param>
		void draw(Graphics brush);

		/// <summary>
		/// Gets the rectangle and its location.
		/// </summary>
		/// <returns>The rectangle.</returns>
		Rectangle getRectangle();

		/// <summary>
		/// Gets the current states from the gameObject.
		/// </summary>
		/// <returns>The states.</returns>
		List<GameState> getStates();

		/// <summary>
		/// Check collsion between the this and
		/// the specified object to test
		/// </summary>
		/// <returns><c>true</c>, if colliding was issued, <c>false</c> otherwise.</returns>
		/// <param name="objectToTest">Object to test.</param>
		bool isColliding(IGameObject objectToTest);
	}
}
