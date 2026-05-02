using System;
using System.Collections.Generic;
using Players;
using Players.Interaction;
using Players.Inventories;
using TestCharacterMovement;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Restaurants.QTESysteme
{
	public class QTESysteme : MonoBehaviour
	{
		[SerializeField] private Transform doorPivot;
		public Dish winGift;
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
		[SerializeField] private GlobalPlayer playerInventory;
		public ObjetBaseInteractable objetBaseInteractable;
		public Collider toDestroy;

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
			playerInventory = objetBaseInteractable.currentPlayer;
			
			PlayerInput playersInputs = playerInventory.playerMovement;
			currentInput = playersInputs;

			
			currentInput.SwitchCurrentActionMap("UI");
			currentRound = round;
			if (objetBaseInteractable.lockDoor == true)
			{
				GenerateSequence();
			}
			else
			{
				delay = TimerDelay;
				currentIndex = 0;
				showFood?.Invoke(winGift);
			}

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
			qteStart = true;
			isStarted = true;
			if (winGift == null && objetBaseInteractable.lockDoor == false)
			{
				return;
			}
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
					if (objetBaseInteractable.lockDoor)
					{
						Success();
					}
					else
					{
						Manche();
					}
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
			qteStart = false;
			currentInput.SwitchCurrentActionMap("Player");

			if (objetBaseInteractable.lockDoor)
			{
				Debug.Log("PORTE DEVERROUILLEE");
				toDestroy.enabled = false;
				doorPivot.transform.rotation = Quaternion.Euler(0, 0, 0); 
			}
			else
			{
				Inventory inventory = playerInventory.Inventory;
				if (winGift != null)
				{
					inventory.AddDish(winGift);
					winGift = null;
				}
				onSuccess?.Invoke();
			}
		}

		void Lose()
		{
			qteStart = false;
			isStarted = false;
			currentInput.SwitchCurrentActionMap("Player");
			winGift = null;
			Debug.Log("FAILED");
			
			if (objetBaseInteractable.lockDoor == false)
				onLose?.Invoke();
			
		}
	}
}