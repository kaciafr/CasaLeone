using DG.Tweening;
using UnityEngine;

public class AnimationList : MonoBehaviour
{
	[SerializeField] private GameObject listPrefab;
	[SerializeField] private float duration;
	[SerializeField] private float mooveY;

	public void PlayAnim(GameObject listP)
	{
		listPrefab = listP;
		Debug.Log(listP);
		RectTransform rt = listP.GetComponent<RectTransform>();
		rt.DOAnchorPosY(this.transform.position.y+ mooveY, duration).SetEase(Ease.Linear);
	}
}
