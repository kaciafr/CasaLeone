using DG.Tweening;
using Item;
using TMPro;
using UnityEngine;

namespace ListForEat
{
	public class ListMove: Singleton<ListMove>
	{
		[SerializeField] private GameObject Uibutton;
		[SerializeField] private GameObject listOfEat;
		[SerializeField] private GameObject startPosition;
		[SerializeField] private GameObject endPosition;
		[SerializeField] private InfoList list;
	
		[SerializeField] private TextMeshProUGUI countPizza;
		[SerializeField] private TextMeshProUGUI countPasta;
		[SerializeField] private TextMeshProUGUI countSalad;
	
		private int salad = 0;
		private int pizza = 0;
		private int pasta = 0;

		private bool flipFlop = true;
		public void ButtonClick()
		{
			list.ClosesInfo();
			flipFlop = !flipFlop;
			if (!flipFlop)
			{
				listOfEat.transform.DOMove(endPosition.transform.position, 0.2f);
				Uibutton.transform.eulerAngles = new Vector3(0, 0, 90);
				
			}
			else
			{
				listOfEat.transform.DOMove(startPosition.transform.position, 0.2f);
				Uibutton.transform.eulerAngles = new Vector3(0, 0, -90);
			}
		}

		private void Start()
		{
			countPasta.text = "x";
			countPizza.text = "x";
			countSalad.text = "x";
		}
		public void UpdateList(int rand)
		{
			if (rand == 0)
			{
				pizza++;
				countPizza.text = "x" + pizza;
			}

			if (rand == 1)
			{
				salad++;
				countSalad.text = "x"+ salad;
			}

			if (rand == 2)
			{
				pasta++;
				countPasta.text = "x" + pasta ;
			}
		}

		public void Remove(Ingrediente ingrediente)
		{
			if (ingrediente.ID == 0)
			{
				pizza--;
				countPizza.text = "x" + pizza;
			}
			if (ingrediente.ID == 1)
			{
				salad--;
				countSalad.text = "x" + salad;
			}

			if (ingrediente.ID == 2)
			{
				pasta--;
				countPasta.text = "x" + pasta;
			}
		}
	}
}
