using Interaction;
using UnityEngine;

namespace Item
{
	public class Item : MonoBehaviour, IInteract
	{
		[SerializeField] private ItemData itemData;
		[SerializeField] private ItemList itemList;
		public void Interact()
		{
			itemList.UpdateList(itemData);
			Destroy(gameObject);
		}

		public void EndInteraction()
		{
		}
	}
}
