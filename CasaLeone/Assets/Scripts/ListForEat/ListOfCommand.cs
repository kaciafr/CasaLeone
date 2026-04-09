using DG.Tweening;
using Pnj;
using PnjWaves;
using UnityEngine;
using UnityEngine.UI;

public class ListOfCommand :Singleton<ListOfCommand>
{
	[SerializeField] private Transform containt;
	
	[SerializeField] private GameObject listPrefab;
	[SerializeField] private RectTransform startPos;
	[SerializeField] private RectTransform endPos;

	public void UpdateVisuel(PnjMove clientType)
	{
		GameObject listCloned = Instantiate(listPrefab, containt);

		CommandUi ui = listCloned.GetComponent<CommandUi>();
		ui.Init(clientType);
	}
}
