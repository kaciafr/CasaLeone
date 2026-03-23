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
	public float TimerDelay;
	[SerializeField] private int maxSequence = 5;
	[SerializeField] private int minSequence = 5;
	[SerializeField] private int round = 2;
	[SerializeField] private PlayerInput playerInput;

	public Action<List<QTEKey>> QTESequence;
	public Action<int> KeyPressed;
	public Action<float> Timer;
	public Action onLose;
	public Action onSuccess;
	private int currentRound;

	[HideInInspector] public float delay;
	private int currentIndex = 0;
	private bool isStarted = false;
	
	public void StartSequence()
	{
		currentRound = round;
		GenerateSequence();
		isStarted = true;
		delay = TimerDelay;
		currentIndex = 0;
	}

	private void Round()
	{
		GenerateSequence();
		currentIndex = 0;
		
	}
	private void Update()
	{
		if (!isStarted) return;
		
		delay -= Time.deltaTime;
		Timer?.Invoke(delay);
		
		if (delay <= 0)
			Lose();
	}

	void GenerateSequence()
	{
		playerInput.SwitchCurrentActionMap("QTE");
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
				Manche();
			}
		}
		else
		{
			Debug.Log("Wrong: " + input);
			Lose();
		}
	}

	private void Manche()
	{
		if(currentRound <= 0)
			Success();
		else
		{
			Round();
			currentRound--;
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