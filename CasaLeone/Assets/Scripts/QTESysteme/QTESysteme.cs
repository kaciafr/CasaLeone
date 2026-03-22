using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class QTESysteme : MonoBehaviour
{
	public enum QTEKey
	{
		Up,
		Down,
		Left,
		Right
	}

	[SerializeField] private List<QTEKey> sequence = new List<QTEKey>();
	[SerializeField] private float TimerDelay = 5;
	[SerializeField] private int maxSequence = 5;
	[SerializeField] private int minSequence = 5;
	[SerializeField] private PlayerInput playerInput;

	public Action<List<QTEKey>> QTESequence;
	public Action<int> KeyPressed;
	public Action onLose;
	public Action onSuccess;

	private float delay;
	private int currentIndex = 0;
	private bool isStarted = false;
	

	public void StartSequence()
	{
		playerInput.SwitchCurrentActionMap("QTE");
		GenerateSequence();
		isStarted = true;
		delay = TimerDelay;
		currentIndex = 0;
	}

	private void Update()
	{
		if (!isStarted) return;
		
		delay -= Time.deltaTime;
		
		if (delay <= 0)
			Lose();
	}

	void GenerateSequence()
	{
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
		isStarted = false;
		playerInput.SwitchCurrentActionMap("Player");
		Debug.Log("SUCCESS");
		onSuccess?.Invoke();
	}

	void Lose()
	{
		isStarted = false;
		playerInput.SwitchCurrentActionMap("Player");
		Debug.Log("FAILED");
		onLose?.Invoke();
	}
}