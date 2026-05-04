using UnityEngine;

namespace Restaurants.QTESysteme
{
	public class SelectedFoodQTE : MonoBehaviour
	{
		[SerializeField] private QTESysteme qteSysteme;
		[SerializeField] private Dish firstSave;
		[SerializeField] private Cooker cook;

		public void ChooseFood(Dish Dish)
		{
			if (!qteSysteme.qteStart)
			{
				cook.winGift = Dish;
				qteSysteme.GenerateSequence();
			}
		}
	}
}
