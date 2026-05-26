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
        public int Priority => 4;
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

        private void OnTriggerEnter(Collider other)
        {
	        if (other.tag == "Player")
	        {
		        itemIcon.transform.DOScale(new Vector3(x, y, z), 0.3f).SetEase(Ease.OutBounce);
	        }
        }

        private void OnTriggerExit(Collider other)
        {
	        if (other.tag == "Player")
	        {
		        itemIcon.transform.DOScale(Vector3.zero, 0.3f);
	        }
        }

        public void Interact(GlobalPlayer globalPlayer)
        {
            if (!seeOneTime)
            {
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
        }
    }
}