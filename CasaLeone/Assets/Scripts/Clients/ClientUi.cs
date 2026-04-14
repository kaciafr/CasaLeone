using Clients.States;
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

        private void UiChanged(IClientState clientState)
        {
            clientrReflexion.SetActive(clientState is ReflexionState);
            clientrTimer.SetActive(clientState is WaitingForFoodState);
            clientrCheck.SetActive(clientState is CheckingState);
        }
    }
}
