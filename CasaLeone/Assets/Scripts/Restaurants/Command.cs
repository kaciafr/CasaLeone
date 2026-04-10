using System;
using Clients;

namespace Restaurants
{
	[Serializable]
	public class Command
	{
		public ClientController client;
		public Dish dish;
	}
}