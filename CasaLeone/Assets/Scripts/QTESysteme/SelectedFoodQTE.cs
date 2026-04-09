using ListForEat;
using UnityEngine;

namespace QTESysteme
{
	public class SelectedFoodQTE : MonoBehaviour
	{
		[SerializeField] private QTESysteme qteSysteme;
		[SerializeField] private Ingrediente firstSave;

		public void ChooseFood(Ingrediente ingrediente)
		{
			if (!qteSysteme.qteStart)
			{
				qteSysteme.winGift = ingrediente;
				qteSysteme.GenerateSequence();
			}
		}
	}
}
