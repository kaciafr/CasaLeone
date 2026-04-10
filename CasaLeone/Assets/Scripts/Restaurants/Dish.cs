using UnityEngine;

namespace Restaurants
{
	[CreateAssetMenu(fileName = "Dish", menuName = "Scriptable Objects/Dish")]
	public class Dish : ScriptableObject
	{
		public int ID;
		public Sprite icon;
		public string name;
	}
}
