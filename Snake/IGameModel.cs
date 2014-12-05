using System;
using System.Drawing;
using System.Collections.Generic;

namespace Snake{

	/// <summary>
	/// The interface will make it possible to change to 
	/// a new implemntation of the model without making any changes 
	/// to the MainFrame
	/// </summary>
	public interface IGameModel{

		/// <summary>
		/// Gets the bitmap from the model
		/// </summary>
		/// <returns>The bitmap.</returns>
		Bitmap getBitmap();

		/// <summary>
		/// Change the size of the bitmap that it will draw in the model
		/// </summary>
		/// <param name="clientSize">Client size.</param>
		void resizeBitmap(Size clientSize);

		/// <summary>
		/// Updates the current key to the model
		/// </summary>
		/// <param name="key">Key.</param>
		void updateCurrentKey(char key);

		List<GameState> getStates();
	}
}