using ListForEat;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NavigationUi :  MonoBehaviour
{
	[SerializeField] private QTESysteme.QTESysteme qteSysteme;
	int index = 0;
	public GameObject[] ingredientesSelected;
	public Ingrediente[] ingredientesData;
	
	private void Start()
	{
		index = 0;
		if (ingredientesSelected != null && ingredientesSelected.Length > 0)
			SelectButton();
	}
	public void MoveRight(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		index++;
		if (index >= ingredientesSelected.Length)
		{
			index = 0;
		}
		Debug.Log(ingredientesSelected[index].name);
		SelectButton();
	}

	public void MoveLeft(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		index--;
		if (index <0)
		{
			index = ingredientesSelected.Length - 1;
			//index = 3;
		}
			
		Debug.Log(ingredientesSelected[index].name);
		SelectButton();
	}

	public void OnSubmit(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		if (index >= ingredientesData.Length || ingredientesData[index] == null)
		{
			Debug.LogError($"ingredientesData[{index}] manquant !");
			return;
		}
		qteSysteme.winGift = ingredientesData[index];
		ingredientesSelected[index]?.GetComponent<Button>()?.onClick.Invoke();
	}
	private void SelectButton()
	{
		EventSystem.current.SetSelectedGameObject(ingredientesSelected[index]);
	}
	
}
