using System;
using System.Collections.Generic;
using Inventories;
using ListForEat;
using Players;
using TestCharacterMovement;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace QTESysteme
{
	public class QTESysteme : MonoBehaviour
	{
		[SerializeField] private GlobalPlayer playerInventory;
		public Ingrediente winGift;
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
		[SerializeField] private int maxSequence = 5;
		[SerializeField] private int minSequence = 5;
		[SerializeField] private int round = 2;
		[SerializeField] private PlayerInput playerInput;
		[SerializeField] private TestPlayerController player;

		public Action<List<QTEKey>> QTESequence;
		public Action<int> KeyPressed;
		public Action<float> Timer;
		public Action<QTESysteme> ChooseFoods;
		public Action onLose;
		public Action onSuccess;
		public Action<Ingrediente> showFood;
	
		private int currentRound;
		[HideInInspector] public float delay;
		private int currentIndex = 0;
		private bool isStarted = false;
	
		public void StartSequence()
		{
			playerInput.SwitchCurrentActionMap("UI");
			player.enabled = false;
			currentRound = round;
			
			delay = TimerDelay;
			currentIndex = 0;
			showFood?.Invoke(winGift);
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

		public void GenerateSequence()
		{
			isStarted = true;
			if (winGift == null)
			{
				return;
			}
			qteStart = true;
			
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
			qteStart = false;
			isStarted = false;
			player.enabled = true;
			playerInput.SwitchCurrentActionMap("Player");
			Debug.Log("SUCCESS");
			onSuccess?.Invoke();
			playerInventory.AddDishClent(winGift);
			winGift = null;
		}

		void Lose()
		{
			qteStart = false;
			isStarted = false;
			player.enabled = true;
			playerInput.SwitchCurrentActionMap("Player");
			winGift = null;
			Debug.Log("FAILED");
			onLose?.Invoke();
		}
	}
}