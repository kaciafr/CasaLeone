using UnityEngine;

namespace Ending
{
	[CreateAssetMenu(fileName = "End")]
	public class EndScript : ScriptableObject
	{
		public bool fireEnd = false;
		public bool burnOutEnd = false;
		public bool changedEnd = false;
		public bool perroquetEnd = false;
	}
}