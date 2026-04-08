using System;
using System.Collections.Generic;
using ListForEat;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace QTESysteme
{
	public class QTESysteme : MonoBehaviour
	{
		[SerializeField] private Inventory.Inventory playerInventory;
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

		public Action<List<QTEKey>> QTESequence;
		public Action<int> KeyPressed;
		public Action<float> Timer;
		public Action<QTESysteme> ChooseFoods;
		public Action onLose;
		public Action onSuccess;
	
		private int currentRound;
		[HideInInspector] public float delay;
		private int currentIndex = 0;
		private bool isStarted = false;
	
		public void StartSequence()
		{
			currentRound = round;
			isStarted = true;
			delay = TimerDelay;
			currentIndex = 0;
			GenerateSequence();
		}

		private void ChooseFood(SelectedFoodQTE food)
		{
			
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
			qteStart = true;
			
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
			playerInput.SwitchCurrentActionMap("Player");
			Debug.Log("SUCCESS");
			onSuccess?.Invoke();
			playerInventory.AddIngrediente(winGift);
			winGift = null;
		}

		void Lose()
		{
			qteStart = false;
			isStarted = false;
			playerInput.SwitchCurrentActionMap("Player");
			Debug.Log("FAILED");
			onLose?.Invoke();
		}
	}
}