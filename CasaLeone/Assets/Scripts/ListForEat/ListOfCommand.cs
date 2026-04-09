using System.Collections.Generic;
using DG.Tweening;
using Pnj;
using PnjWaves;
using UnityEngine;
using UnityEngine.UI;

public class ListOfCommand :Singleton<ListOfCommand>
{
	[SerializeField] private Transform containt;
	
	[SerializeField] private GameObject listPrefab;
	[SerializeField] private List<CommandUi> commandUis;

	private ClientTypeSO client;

	public void UpdateVisuel(PnjMove clientType)
	{
		GameObject listCloned = Instantiate(listPrefab, containt);
		CommandUi ui = listCloned.GetComponent<CommandUi>();
		ui.Init(clientType);
		
		commandUis.Add(ui);
	}

	public void Remove(PnjMove clientType)
	{
		CommandUi removed = commandUis.Find(x =>x.pnjMove.clientData ==  clientType.clientData);
		commandUis.Remove(removed);
		Destroy(removed.gameObject);
	}
}
