using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
	[SerializeField] private float maxDelay = 5;
	[SerializeField] private PlayerInput playerInput;

	private float delay;
	private int currentIndex = 0;
	private bool isStarted = false;
	

	public void StartSequence()
	{
		playerInput.SwitchCurrentActionMap("QTE");
		GenerateSequence();
		isStarted = true;
		delay = maxDelay;
		currentIndex = 0;

		DebugSequence();
	}

	private void Update()
	{
		if (!isStarted) return;

		delay -= Time.deltaTime;

		if (delay <= 0)
		{
			Lose();
		}
	}

	void GenerateSequence()
	{
		sequence.Clear();

		for (int i = 0; i < 4; i++)
		{
			int rand = Random.Range(0, 4);
			sequence.Add((QTEKey)rand);
		}
	}

	public void UpKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		Debug.Log($"Up Key");
		HandleInput(QTEKey.Up);
	}

	public void DownKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		Debug.Log("DownKey");
		HandleInput(QTEKey.Down);
	}

	public void LeftKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		Debug.Log("Left Key");
		HandleInput(QTEKey.Left);
	}

	public void RightKey(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		Debug.Log("RightKey");
		HandleInput(QTEKey.Right);
	}

	void HandleInput(QTEKey input)
	{
		if (!isStarted) return;

		if (sequence[currentIndex] == input)
		{
			Debug.Log("Correct: " + input);

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
	}

	void Lose()
	{
		isStarted = false;
		playerInput.SwitchCurrentActionMap("Player");
		Debug.Log("FAILED");
	}

	void DebugSequence()
	{
		string seq = "";

		foreach (var key in sequence)
		{
			seq += key + " ";
		}

		Debug.Log("Sequence: " + seq);
	}
}