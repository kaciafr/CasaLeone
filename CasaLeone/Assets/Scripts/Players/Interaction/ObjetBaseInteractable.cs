using DG.Tweening;
using Restaurants.QTESysteme;
using Restaurants.QTESysteme.UiQte;
using UnityEngine;

namespace Players.Interaction
{
    public class ObjetBaseInteractable : MonoBehaviour, IInteractable
    {
        [Header("UI")]
        [SerializeField] private Transform objectTransform;
        [SerializeField] private GameObject pressE;
        [SerializeField] private Transform endPosition; 
        [SerializeField] private Transform startPosition;
        [SerializeField] private QTESysteme qteSysteme;
        [SerializeField] private TransformeUiQte UiLocQte;
        [SerializeField] private float speedAnim = 0.5f;

        private IQteListen currentListener;

        [Header("References")]
        public GlobalPlayer playerInventory; 
        public GlobalPlayer currentPlayer;

        private void Start()
        {
            currentListener = GetComponent<IQteListen>();
            
           
            if (pressE != null && startPosition != null)
                pressE.transform.position = startPosition.position;
            
           
            if (qteSysteme == null) Debug.LogError("QTESysteme manquant sur " + gameObject.name);
        }

        public void Interact(GlobalPlayer globalPlayer)
        {
            
            if (qteSysteme.isStarted || qteSysteme.qteStart) return;

            playerInventory = globalPlayer;
            currentPlayer = globalPlayer;

           
            pressE.transform.DOKill();
            pressE.SetActive(false);

            UiLocQte.UiTransform(objectTransform);
            qteSysteme.interactObj = this;
            qteSysteme.StartSequence(currentListener);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                pressE.SetActive(true);
                pressE.transform.DOKill();
                pressE.transform.DOMove(endPosition.position, speedAnim).SetEase(Ease.OutBack);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                pressE.transform.DOKill(); 
                pressE.transform.DOMove(startPosition.position, speedAnim).OnComplete(() => 
                {
                    pressE.SetActive(false);
                });
            }
        }
    }
}