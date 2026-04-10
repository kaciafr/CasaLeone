using UnityEngine;

namespace Restaurants.QTESysteme
{
	public class SelectedFoodQTE : MonoBehaviour
	{
		[SerializeField] private QTESysteme qteSysteme;
		[SerializeField] private Dish firstSave;

		public void ChooseFood(Dish Dish)
		{
			if (!qteSysteme.qteStart)
			{
				qteSysteme.winGift = Dish;
				qteSysteme.GenerateSequence();
			}
		}
	}
}
