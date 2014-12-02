using System;
using System.Collections;

namespace Snake{
	public class GameObjectCollection:CollectionBase{
		public GameObjectCollection(){

		}
		public void Add(IGameObject gameObject)
		{
			List.Add(gameObject);
		}

		public void Remove(IGameObject gameObject)
		{
			List.Remove(gameObject);
		}

		public IGameObject this[int index]
		{
			get{return (IGameObject)List[index];}
			set{List[index]=value;}
		}
	}
}

