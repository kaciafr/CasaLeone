using System;
using DG.Tweening;
using UnityEngine;


    public class ClimbZone : MonoBehaviour
    {
        public GameObject climbFeedBack;
        private Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);

      

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
            {
                PlayerMovement pm = other.GetComponentInParent<PlayerMovement>();
                if(pm != null)
                    pm.CanClimb = true;

                climbFeedBack.SetActive(true);
                climbFeedBack.transform.DOKill(true);

                climbFeedBack.transform.localScale = Vector3.zero;
                climbFeedBack.transform.DOScale(targetScale, 0.5f).SetEase(Ease.OutBack);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
            {
                PlayerMovement pm = other.GetComponentInParent<PlayerMovement>();
                if(pm != null)
                    pm.CanClimb = false;

                climbFeedBack.transform.DOKill(true);

                Sequence exitSeq = DOTween.Sequence();
                exitSeq.Append(climbFeedBack.transform.DOScale(targetScale * 1.2f, 0.2f).SetEase(Ease.OutBack));
                exitSeq.Append(climbFeedBack.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));
                exitSeq.OnComplete(() => climbFeedBack.SetActive(false));
            }
        }
    }