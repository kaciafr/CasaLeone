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
			PlayAnimation(ui, true);
		}
		private void RemoveCommandUI(Command command)
		{
			if (commandUis.Remove(command, out CommandUI ui))
			{
				PlayAnimation(ui,false);
			}
		}
		private void PlayAnimation(CommandUI moveUI, bool show)
		{
			RectTransform rect = moveUI.GetComponent<RectTransform>();

			if (show)
			{
				rect.pivot = new Vector2(0.5f, 1f);
				rect.localScale = new Vector3(1f, 0f, 1f);
				rect.DOScaleY(1f, 0.8f).SetEase(Ease.OutBounce);
			}
			else
			{
				rect.DOScaleY(0f, 0.4f).SetEase(Ease.InCubic).OnComplete(() => Destroy(rect.gameObject));
			}
		}
	}
}
