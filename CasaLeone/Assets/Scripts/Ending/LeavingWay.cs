using DG.Tweening;
using UnityEngine;

namespace Ending
{
	public class LeavingWay: MonoBehaviour
	{
		[SerializeField] private GameObject leavingWayUi;

		private void Start()
		{
			leavingWayUi.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
				leavingWayUi.SetActive(true);
				Time.timeScale = 0;
			}
		}
	}
}