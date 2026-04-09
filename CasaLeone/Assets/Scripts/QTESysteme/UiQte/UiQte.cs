using System.Collections.Generic;
using DG.Tweening;
using ListForEat;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace QTESysteme.UiQte
{
	public class UiQte : MonoBehaviour
	{
		[Header("Arrow")]
		[SerializeField] private Sprite upArrow;
		[SerializeField] private Sprite downArrow;
		[SerializeField] private Sprite leftArrow;
		[SerializeField] private Sprite rightArrow;
		private List<Image> arrows = new List<Image>();
	
		[Header("References")]
		[SerializeField] private Image timerBar;
		[SerializeField] private GameObject timerBars;
		[SerializeField] private QTESysteme qteSysteme;
		[SerializeField] private Transform arrowContainer;
		[SerializeField] private GameObject arrowPrefab;
		[SerializeField] private AngoisseBar.AngoisseBar angoisseBar;
		[SerializeField] private GameObject endPosition;
		
		
		[Header("FoodToChoose")]
		[SerializeField] private GameObject pizza;
		[SerializeField] private GameObject salade;
		[SerializeField] private GameObject pates;
	

		private void Start()
		{
			timerBars.SetActive(false);
			pizza.SetActive(false);
			salade.SetActive(false);
			pates.SetActive(false);
		}

		private void OnEnable()
		{
			qteSysteme.QTESequence += GenereteArrow;
			qteSysteme.KeyPressed += UpdateArrow;
			qteSysteme.onLose += LoseUI;
			qteSysteme.onSuccess += WinUI;
			qteSysteme.Timer += Timers;
			qteSysteme.showFood += ChooseFood;
		}
		private void OnDisable()
		{
			qteSysteme.QTESequence -= GenereteArrow;
			qteSysteme.KeyPressed -= UpdateArrow;
			qteSysteme.onLose -= LoseUI;
			qteSysteme.onSuccess -= WinUI;
			qteSysteme.Timer -= Timers;
			qteSysteme.showFood -= ChooseFood;
		}

		private void ChooseFood(Ingrediente food)
		{
			Debug.Log("ChooseFood");
			pizza.SetActive(true);
			salade.SetActive(true);
			pates.SetActive(true);
		}


		private void GenereteArrow(List<QTESysteme.QTEKey> sequence)
		{
			pizza.SetActive(false);
			salade.SetActive(false);
			pates.SetActive(false);
			
			timerBars.SetActive(true);

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
			arrows[index].transform.DOMove(endPosition.transform.position, 1f);
			angoisseBar.RemoveAnguish(0.1f);
		}

		private void LoseUI()
		{
			foreach (Transform child in arrowContainer)
				Destroy(child.gameObject);
			arrows.Clear();
			timerBars.SetActive(false);

			angoisseBar.AddAnguish(0.4f);
		}

		private void WinUI()
		{
			foreach (Transform child in arrowContainer)
				Destroy(child.gameObject);
			timerBars.SetActive(false);
			arrows.Clear();
		
		}
		private void Timers(float time)
		{
			float ration = time /qteSysteme.TimerDelay;
			timerBar.fillAmount = ration ;
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
}
