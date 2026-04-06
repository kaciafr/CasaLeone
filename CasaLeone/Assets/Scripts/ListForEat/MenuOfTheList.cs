using UnityEngine;

public class MenuOfTheList : MonoBehaviour
{
	[Header("List")]
	[SerializeField] private GameObject eatList;
	[SerializeField] private GameObject itemList;
	[SerializeField] private GameObject missionList;
	[SerializeField] private GameObject optionsList;
	private MoveButtonFront button;
	private void Start()
	{
		button = GetComponent<MoveButtonFront>();
		button.OnClick += Click;
	}

	private void Click(MoveButtonFront obj)
	{
		Debug.Log(obj.name);
	}
}
