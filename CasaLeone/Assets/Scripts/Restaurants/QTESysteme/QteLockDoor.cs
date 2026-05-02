using System;
using System.Collections.Generic;
using DefaultNamespace;
using Players;
using Players.Interaction;
using Players.Inventories;
using Restaurants;
using Restaurants.QTESysteme;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class QteLockDoor : MonoBehaviour
{

	public enum QTEKey
	{
		Up,
		Down,
		Left,
		Right
	}

	[SerializeField] private List<QTEKey> sequence = new List<QTEKey>();
	public float TimerDelay;
	public bool qteStart = false;
	public int maxSequence = 5;
	public int minSequence = 5;
	public int round = 2;
	[SerializeField] private ZoneLockDor objetBaseInteractable;

	public event Action<List<QTEKey>> QTESequence;
	public event Action<int> KeyPressed;
	public event Action<float> Timer;
	public event Action<QTESysteme> ChooseFoods;
	public event Action onLose;
	public event Action onSuccess;
	public event Action<Dish> showFood;

	private int currentRound;
	[HideInInspector] public float delay;
	private int currentIndex = 0;
	private bool isStarted = false;
	public PlayerInput currentInput;

	public void StartSequence()
	{
		currentInput.SwitchCurrentActionMap("UI");
		currentRound = round;

		delay = TimerDelay;
		currentIndex = 0;
		GenerateSequence();
	}

	private void Update()
	{
		if (!isStarted) return;

		delay -= Time.deltaTime;
		Timer?.Invoke(delay);

		if (delay <= 0)
			Lose();
	}

	public void GenerateSequence()
	{
		isStarted = true;
		qteStart = true;

		currentInput.SwitchCurrentActionMap("QTE");
		sequence.Clear();

		int randS = Random.Range(minSequence, maxSequence);
		for (int i = 0; i < randS; i++)
		{
			int rand = Random.Range(0, 4);
			sequence.Add((QTEKey)rand);
		}

		QTESequence?.Invoke(sequence);
	}

	public void UpKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		HandleInput(QTEKey.Up);
	}

	public void DownKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		HandleInput(QTEKey.Down);
	}

	public void LeftKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		HandleInput(QTEKey.Left);
	}

	public void RightKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		HandleInput(QTEKey.Right);
	}

	void HandleInput(QTEKey input)
	{
		if (!isStarted) return;

		if (sequence[currentIndex] == input)
		{
			KeyPressed?.Invoke(currentIndex);
			currentIndex++;
			if (currentIndex >= sequence.Count)
			{
				Success();
			}
		}
		else
		{
			Debug.Log("Wrong: " + input);
			Lose();
		}
	}

	void Success()
	{

		qteStart = false;
		isStarted = false;
		currentInput.SwitchCurrentActionMap("Player");
		Debug.Log("SUCCESS");
		onSuccess?.Invoke();
	}

	void Lose()
	{
		qteStart = false;
		isStarted = false;
		currentInput.SwitchCurrentActionMap("Player");
		Debug.Log("FAILED");
		onLose?.Invoke();
	}
}
