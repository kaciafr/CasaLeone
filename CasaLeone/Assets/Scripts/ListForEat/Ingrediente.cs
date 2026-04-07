using UnityEngine;

namespace ListForEat
{
	[CreateAssetMenu(fileName = "Ingrediente", menuName = "Scriptable Objects/Ingrediente")]
	public class Ingrediente : ScriptableObject
	{
		public int ID;
		public string name;
	}
}
