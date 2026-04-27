using Clients.States;
using DG.Tweening;
using DialogueSystem.Runtime;
using UnityEngine;

namespace Clients
{
    public class ClientUi : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ClientController clientController;
        [SerializeField] private GameObject bubblePrefab;
        public Vector3 bubbleOffset = new Vector3(2f, 4f, 0f);
        private WorldSpaceBubble currentBubble;
        [Header("UISettings")]
        [SerializeField] private GameObject clientrReflexion;
        [SerializeField] private GameObject clientrTimer;
        [SerializeField] private GameObject clientrCheck;
        
        [SerializeField]
        private float zoom = 1;

        private void Start()
        {
            clientrReflexion.SetActive(false);
            clientrTimer.SetActive(false);
            clientrCheck.SetActive(false);
        }

        private void OnEnable()
        {
            clientController.OnStateChanged += UiChanged;
        }

        private void OnDisable()
        {
            clientController.OnStateChanged -= UiChanged;
        }

        private ReflexionState currentReflexion;
        private WaitingForFoodState currentWaitingForFood;
        private LeavingState leavingStates;

        private void UiChanged(IClientState clientState)
        {
            if (currentReflexion != null)
            {
                currentReflexion.OnReady -= OnClientReady;
                currentReflexion = null;
            }
            
            if (currentWaitingForFood != null)
            {
                currentWaitingForFood.Bored -= BoredLine;
                currentWaitingForFood = null;
            }

            if (clientState is ReflexionState reflexion)
            {
                currentReflexion = reflexion;
                currentReflexion.OnReady += OnClientReady;
                clientrReflexion.SetActive(true);
                clientrReflexion.transform.localScale = Vector3.zero; 
            }
            else clientrReflexion.SetActive(false);
            

            if (clientState is CheckingState)
            {
                clientrCheck.SetActive(true);
                clientrCheck.transform.localScale = Vector3.zero; 
                clientrCheck.transform.DOScale(Vector3.one*zoom, 0.5f).SetEase(Ease.OutBack);
            }
            else clientrCheck.SetActive(false);

            if (clientState is LeavingState leavingState)
            {
                leavingStates = leavingState;
                leavingStates.ReplicaLine += ReplicaLine;
            }
            
            if (clientState is WaitingForFoodState waitingForFoodState)
            {
                currentWaitingForFood = waitingForFoodState;

                clientrTimer.SetActive(true);
                clientrTimer.transform.localScale = Vector3.zero;
                clientrTimer.transform.DOScale(Vector3.one*zoom, 0.5f).SetEase(Ease.OutBack);

                currentWaitingForFood.Bored += BoredLine;
            }
            else
            {
                clientrTimer.SetActive(false);
            }
        }

        private void BoredLine()
        {
            GameObject bubble = Instantiate(bubblePrefab, clientController.transform);
            bubble.transform.localPosition = bubbleOffset;

            currentBubble = bubble.GetComponent<WorldSpaceBubble>();

            string line = clientController.ClientData.PickImpatientLine();

            DialogueSystem.DATA.DialogueNode node = new DialogueSystem.DATA.DialogueNode { dialogueText = line, autoAdvanceDelay = 2f };

            currentBubble.Display(node);
            
            Destroy(bubble, 7f);
        }

        private void ReplicaLine()
        {
            GameObject bubble = Instantiate(bubblePrefab, clientController.transform);
            bubble.transform.localPosition = bubbleOffset;

            currentBubble = bubble.GetComponent<WorldSpaceBubble>();

            string line;

            if (leavingStates.IsAngry)
            {
                line = clientController.ClientData.PickAngryLine();
            }
            else
            {
                line = clientController.ClientData.PickSatisfiedLine();
            }

            DialogueSystem.DATA.DialogueNode node = new DialogueSystem.DATA.DialogueNode { dialogueText = line, autoAdvanceDelay = 2f };

            currentBubble.Display(node);
            
            Destroy(bubble, 7f);
        }

        private void OnClientReady()
        {
            clientrReflexion.transform.localScale = Vector3.zero;
            clientrReflexion.transform.DOScale(Vector3.one*zoom, 0.5f).SetEase(Ease.OutBack);
        }
    }
}
