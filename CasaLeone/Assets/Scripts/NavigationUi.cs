using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NavigationUi :  MonoBehaviour
{
	int index = 0;
	public GameObject[] buttons;

	private void Start()
	{
		foreach (GameObject button in buttons)
		{
			button.SetActive(false);
		}
	}
	
	public void Navigate(InputAction.CallbackContext context)
	{
		Vector2 input = context.ReadValue<Vector2>();

		if (input.x > 0)
		{
			MoveRight();
		}
		else if (input.x < 0)
		{
			MoveLeft();
		}
	}

	void MoveRight()
	{
		index++;
		if (index >= buttons.Length)
			index = 0;
		Debug.Log("aaaaaaaaaaaaaaaaaa");
		SelectButton();
	}

	void MoveLeft()
	{
		index--;
		if (index < 0)
			index = buttons.Length - 1;
		Debug.Log("dddddddddddddd");
		SelectButton();
	}

	public void OnSubmit(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			buttons[index].GetComponent<Button>().onClick.Invoke();
		}
	}
	private void SelectButton()
	{
		EventSystem.current.SetSelectedGameObject(buttons[index]);
	}
	
}
