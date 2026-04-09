using System.Collections.Generic;
using Clients;
using DG.Tweening;
using PnjWaves;
using UnityEngine;
using UnityEngine.UI;

public class ListOfCommand :Singleton<ListOfCommand>
{
	[SerializeField] private Transform containt;
	
	[SerializeField] private GameObject listPrefab;
	[SerializeField] private List<CommandUi> commandUis;

	private ClientTypeSO client;

	public void UpdateVisuel(ClientMovement clientType)
	{
		GameObject listCloned = Instantiate(listPrefab, containt);
		CommandUi ui = listCloned.GetComponent<CommandUi>();
		ui.Init(clientType);
		
		commandUis.Add(ui);
	}

	public void Remove(ClientMovement clientType)
	{
		CommandUi removed = commandUis.Find(x =>x.clientMovement.clientData ==  clientType.clientData);
		commandUis.Remove(removed);
		Destroy(removed.gameObject);
	}
}
