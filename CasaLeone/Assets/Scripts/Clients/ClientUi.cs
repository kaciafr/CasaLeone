using Clients.States;
using DG.Tweening;
using UnityEngine;

namespace Clients
{
    public class ClientUi : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ClientController clientController;
        
        [Header("UISettings")]
        [SerializeField] private GameObject clientrReflexion;
        [SerializeField] private GameObject clientrTimer;
        [SerializeField] private GameObject clientrCheck;

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

        private void UiChanged(IClientState clientState)
        {
            if (currentReflexion != null)
            {
                currentReflexion.OnReady -= OnClientReady;
                currentReflexion = null;
            }

            if (clientState is ReflexionState reflexion)
            {
                currentReflexion = reflexion;
                currentReflexion.OnReady += OnClientReady;
                clientrReflexion.SetActive(true);
                clientrReflexion.transform.localScale = Vector3.zero; 
            }
            else clientrReflexion.SetActive(false);

            if (clientState is WaitingForFoodState)
            {
                clientrTimer.SetActive(true);
                clientrTimer.transform.localScale = Vector3.zero; 
                clientrTimer.transform.DOScale(Vector3.one*0.04f, 0.5f).SetEase(Ease.OutBack);
            }
            else clientrTimer.SetActive(false);

            if (clientState is CheckingState)
            {
                clientrCheck.SetActive(true);
                clientrCheck.transform.localScale = Vector3.zero; 
                clientrCheck.transform.DOScale(Vector3.one*0.04f, 0.5f).SetEase(Ease.OutBack);
            }
            else clientrCheck.SetActive(false);
        }

        private void OnClientReady()
        {
            clientrReflexion.transform.localScale = Vector3.zero;
            clientrReflexion.transform.DOScale(Vector3.one*0.04f, 0.5f).SetEase(Ease.OutBack);
        }
    }
}
