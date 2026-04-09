using Clients;
using UnityEngine;
using UnityEngine.UI;

public class CommandUi : MonoBehaviour
{
	public ClientMovement clientMovement;
	public Image iconPNJ;
	public Image order;

	public void Init(ClientMovement clientType)
	{
		clientMovement = clientType;
		iconPNJ.sprite = clientType.clientData.clientSprite;
		order.sprite = clientType.whatTheyWhant.icon;
	}
}

