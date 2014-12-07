using System;

namespace Snake{
	/// <summary>
	/// All the different game states that will be used in
	/// IGameObject, GameData, IGameModel and controls
	/// </summary>
	public enum GameState{
		Dead,Alive,Up,Down,Left,Right,Grow,None,SpeedUp,Score,Score2,
		Menu,RunGame,ExitGame,Confirm,NewGame,Pause,Reset,Start,Black,
		Grey,UnPause,Break,Green
	}
}

