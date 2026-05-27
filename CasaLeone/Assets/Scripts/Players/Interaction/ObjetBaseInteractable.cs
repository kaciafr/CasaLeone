using DG.Tweening;
using Outline;
using Players.Inventories;
using Restaurants.QTESysteme;
using Restaurants.QTESysteme.UiQte;
using UnityEngine;

namespace Players.Interaction
{
    public class ObjetBaseInteractable :  OutlineBase, IInteractable
    {
        [Header("UI")]
        [SerializeField] private Transform objectTransform;
        [SerializeField] private GameObject pressE;
        [SerializeField] private Transform endPosition; 
        [SerializeField] private Transform startPosition;
        [SerializeField] private QTESysteme qteSysteme;
        [SerializeField] private TransformeUiQte UiLocQte;
        [SerializeField] private float speedAnim = 0.5f;
        public int Priority => 5;

        private IQteListen currentListener;

        [Header("References")]
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
            
            currentPlayer = globalPlayer;

           
            pressE.transform.DOKill();
            pressE.SetActive(false);

            UiLocQte.UiTransform(objectTransform);
            qteSysteme.interactObj = this;
            qteSysteme.StartSequence(currentListener);
        }

        void IInteractable.OnPlayerEnter(GlobalPlayer player)
        {
	        
	        SetOutline(true );
	        pressE.SetActive(true);
	        pressE.transform.DOKill();
	        pressE.transform.DOMove(endPosition.position, speedAnim).SetEase(Ease.OutBack);
        }

        void IInteractable.OnPlayerExit(GlobalPlayer player)
        {
	        SetOutline(false );
	        pressE.transform.DOKill(); 
	        pressE.transform.DOMove(startPosition.position, speedAnim).OnComplete(() => 
	        {
		        pressE.SetActive(false);
	        });
        }

    }
}