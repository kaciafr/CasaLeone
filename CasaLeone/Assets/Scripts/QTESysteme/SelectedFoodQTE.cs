using ListForEat;
using UnityEngine;

namespace QTESysteme
{
	public class SelectedFoodQTE : MonoBehaviour
	{
		[SerializeField] private GameObject pizzaButton;
		[SerializeField] private GameObject pastaButton;
		[SerializeField] private GameObject saladeButton;
	
		[SerializeField] private QTESysteme qteSysteme;
		[SerializeField] private Ingrediente firstSave;

		private void Start()
		{
			qteSysteme.winGift = firstSave;
		}

		public void Pizza(Ingrediente ingrediente)
		{
			if (!qteSysteme.qteStart)
				qteSysteme.winGift = ingrediente;
		}

		public void Pasta(Ingrediente ingrediente)
		{
			if (!qteSysteme.qteStart)
				qteSysteme.winGift = ingrediente;
		}

		public void Salade(Ingrediente ingrediente)
		{
			if (!qteSysteme.qteStart)
				qteSysteme.winGift = ingrediente;
		}
	}
}
