using Clients;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurants.UI
{
	public class CommandUI : MonoBehaviour
	{
		public ClientController clientController;
		public Image iconPNJ;
		public Image order;

		public void Init(ClientController controller, Dish dish)
		{
		}

		public void Init(Command command)
		{
			var controller = command.client;
			var dish = command.dish;
			clientController = controller;
			iconPNJ.sprite = controller.ClientData.clientSprite;
			order.sprite = dish.icon;
		}
	}
}

