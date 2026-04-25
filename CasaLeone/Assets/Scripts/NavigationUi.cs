using Restaurants;
using Restaurants.QTESysteme;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NavigationUi :  MonoBehaviour
{
	[SerializeField] private QTESysteme qteSysteme;
	int index = 0;
	public GameObject[] DishsSelected;
	public Dish[] DishsData;
	
	private void Start()
	{
		index = -1;
		if (DishsSelected != null && DishsSelected.Length > 0)
			SelectButton();
	}
	public void MoveRight(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		index++;
		if (index >= DishsSelected.Length)
		{
			index = 0;
		}
		Debug.Log(DishsSelected[index].name);
		SelectButton();
	}

	public void MoveLeft(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		index--;
		if (index <0)
		{
			index = DishsSelected.Length - 1;
		}
			
		Debug.Log(DishsSelected[index].name);
		SelectButton();
	}

	public void OnSubmit(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		if (index >= DishsData.Length || DishsData[index] == null)
		{
			Debug.LogError($"DishsData[{index}] manquant !");
			return;
		}
		qteSysteme.winGift = DishsData[index];
		DishsSelected[index]?.GetComponent<Button>()?.onClick.Invoke();
	}
	private void SelectButton()
	{
		EventSystem.current.SetSelectedGameObject(DishsSelected[index]);
	}
	
}
