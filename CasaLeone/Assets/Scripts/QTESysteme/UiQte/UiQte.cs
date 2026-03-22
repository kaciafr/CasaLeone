using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UiQte : MonoBehaviour
{
	[Header("Arrow")]
	[SerializeField] private Sprite upArrow;
	[SerializeField] private Sprite downArrow;
	[SerializeField] private Sprite leftArrow;
	[SerializeField] private Sprite rightArrow;
	private List<Image> arrows = new List<Image>();
	
	[Header("References")]
	[SerializeField] private QTESysteme qteSysteme;
	[SerializeField] private Transform arrowContainer;
	[SerializeField] private GameObject arrowPrefab;
	[SerializeField] private AngoisseBar angoisseBar;

	private void Start()
	{
		qteSysteme.QTESequence += GenereteArrow;
		qteSysteme.KeyPressed += UpdateArrow;
		qteSysteme.onLose += LoseUI;
		qteSysteme.onSuccess += WinUI;
	}

	private void GenereteArrow(List<QTESysteme.QTEKey> sequence)
	{
		Debug.Log("Generete arrow");
		foreach (Transform child in arrowContainer)
			Destroy(child.gameObject);
		
		arrows.Clear();
		foreach (QTESysteme.QTEKey key in sequence)
		{
			GameObject go = Instantiate(arrowPrefab, arrowContainer);
			Image arrow = go.GetComponent<Image>();

			arrow.sprite = GetSprite(key);
			
			arrows.Add(arrow);
		}
		
	}

	private void UpdateArrow(int index)
	{
		arrows[index].transform.DOJump(Vector3.up, 5f,1,1);
		angoisseBar.RemoveAnguish(0.1f);
	}

	private void LoseUI()
	{
		foreach (Transform child in arrowContainer)
			Destroy(child.gameObject);
		arrows.Clear();
		angoisseBar.AddAnguish(0.4f);
	}

	private void WinUI()
	{
		foreach (Transform child in arrowContainer)
			Destroy(child.gameObject);
		arrows.Clear();
		
	} 
	Sprite GetSprite (QTESysteme.QTEKey key)
	{
		switch (key)
		{
			case QTESysteme.QTEKey.Up : return upArrow;
			case QTESysteme.QTEKey.Down : return downArrow;
			case QTESysteme.QTEKey.Left : return leftArrow;
			case QTESysteme.QTEKey.Right : return rightArrow;
		}

		return null;
	}
}
