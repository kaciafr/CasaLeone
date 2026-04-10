using System;
using System.Collections.Generic;
using Clients;
using DG.Tweening;
using PnjWaves;
using UnityEngine;

namespace Restaurants.UI
{
	public class CommandsListUI : MonoBehaviour
	{
		[SerializeField] 
		private Transform container;
	
		[SerializeField] 
		private CommandUI prefab;
		
		private Dictionary<Command, CommandUI> commandUis;

		private ClientTypeSO client;

		private void Awake()
		{
			commandUis =  new Dictionary<Command, CommandUI>();
		}

		private void OnEnable()
		{
			Restaurant.Instance.OnCommandAdded += AddCommandUI;
			Restaurant.Instance.OnCommandRemoved += RemoveCommandUI;
		}

		private void OnDisable()
		{
			Restaurant.Instance.OnCommandAdded -= AddCommandUI;
			Restaurant.Instance.OnCommandRemoved -= RemoveCommandUI;
		}

		private void AddCommandUI(Command command)
		{
			CommandUI ui = Instantiate(prefab, container);
			ui.Init(command);
			commandUis[command] = ui;
		}
		
		private void RemoveCommandUI(Command command)
		{
			if (commandUis.Remove(command, out CommandUI ui))
			{
				Destroy(ui.gameObject);
			}
		}

	}
}
