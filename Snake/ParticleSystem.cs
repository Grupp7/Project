﻿using System;

namespace Snake{
	public class ParticleSystem:IGameObject{
		public ParticleSystem(){
		}

		#region IGameObject implementation

		public void passData (GameData newInfo){
			throw new NotImplementedException();
		}

		public void update (double gameTime){
			throw new NotImplementedException();
		}

		public void draw (System.Drawing.Graphics brush){
			throw new NotImplementedException();
		}

		public System.Drawing.Rectangle getRectangle (){
			throw new NotImplementedException();
		}

		#endregion
	}
}

