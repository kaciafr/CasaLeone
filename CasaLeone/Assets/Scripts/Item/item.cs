using DG.Tweening;
using DialogueSystem.DATA;
using Players;
using Players.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class Item : MonoBehaviour, IInteractable
    {
	    int IInteractable.Priority => 4;
        [SerializeField] private ItemData itemData;
        [SerializeField] private ItemList itemList;
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;
       
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Image icon;
        [SerializeField] private SpriteRenderer itemIcon;
        [SerializeField] private TextMeshProUGUI description;
        private bool seeOneTime = false;
       
        [Tooltip("La DialogueConversation du NPC à qui rendre cet objet")]
        public DialogueConversation linkedConversation; 

        private void Start()
        {
            itemPrefab.SetActive(false);
            itemIcon.transform.localScale = Vector3.zero;
        }

        void IInteractable.Interact(GlobalPlayer globalPlayer)
        {
	        if (seeOneTime) 
		        return;
	        
	        Time.timeScale = 0;
	        icon.sprite = itemData.icon;
	        description.text = itemData.description;
	        itemPrefab.SetActive(true);
	        itemList.UpdateList(itemData);
	        gameObject.SetActive(false);
	        seeOneTime = true;
	        
	        if (linkedConversation != null)
		        linkedConversation.itemCollected = true;
        }

        void IInteractable.OnPlayerEnter(GlobalPlayer player)
        {
	        var iconTransform = itemIcon.transform;
	        iconTransform.DOKill(true);
	        iconTransform.DOScale(new Vector3(x, y, z), 0.3f).SetEase(Ease.OutBounce);
        }

        void IInteractable.OnPlayerExit(GlobalPlayer player)
        {
	        var iconTransform = itemIcon.transform;
	        iconTransform.DOKill(true);
	        iconTransform.DOScale(Vector3.zero, 0.3f);
        }

    }
}