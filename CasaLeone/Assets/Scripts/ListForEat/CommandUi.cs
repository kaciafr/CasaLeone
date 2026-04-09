using Pnj;
using UnityEngine;
using UnityEngine.UI;

public class CommandUi : MonoBehaviour
{
	public Image iconPNJ;
	public Image order;

	public void Init(PnjMove clientType)
	{
		iconPNJ.sprite = clientType.clientData.clientSprite;
		order.sprite = clientType.whatTheyWhant.icon;
	}
}

