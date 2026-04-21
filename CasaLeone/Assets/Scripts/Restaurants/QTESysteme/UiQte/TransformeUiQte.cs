using DG.Tweening;
using UnityEngine;

namespace Restaurants.QTESysteme.UiQte
{
	public class TransformeUiQte : MonoBehaviour
	{
		[SerializeField] private GameObject UiLoc;

		public void UiTransform(Transform obj)
		{
			UiLoc.transform.position = obj.position;
		}
	}
}
