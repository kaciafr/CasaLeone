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
		public int maxSequence;
		public int minSequence;
		
		public ObjetBaseInteractableQte interactObj;
		

		public event Action<List<QTEKey>> QTESequence;
		public event Action<int> KeyPressed;
		public event Action<float> Timer;
		public event Action<QTESysteme> ChooseFoods;
		public event Action onLose;
		public event Action onSuccess;
		
	
		private int currentRound;
		[HideInInspector] public float delay;
		private int currentIndex = 0;
		public bool isStarted = false;
		public PlayerInput currentInput;
		public IQteListen currentQteListen;

		public void StartSequence(IQteListen listener)
		{
			currentQteListen = listener;
			var currentInv = interactObj.playerInventory;
			currentInv = interactObj.currentPlayer;

			PlayerInput playersInputs = currentInv.playerMovement;
			currentInput = playersInputs;
			currentInput.SwitchCurrentActionMap("UI");
			
			
			currentRound = listener.QteRound();
			listener.OnQteStart();
			
			Debug.Log("No lock door");
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

		public void GenerateSequence()
		{
			qteStart = true;
			isStarted = true;
			currentInput.SwitchCurrentActionMap("QTE");
			Debug.Log(currentInput.currentActionMap);
			
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
			qteStart = false;
			currentInput.SwitchCurrentActionMap("Player");

			GlobalPlayer currentPlayer = interactObj.currentPlayer;
			currentQteListen.OnQteSucces(currentPlayer);
			onSuccess?.Invoke();

		}

		void Lose()
		{
			qteStart = false;
			isStarted = false;
			currentInput.SwitchCurrentActionMap("Player");
			currentQteListen.OnQteFail();
			onLose?.Invoke();
			
		}
	}
}