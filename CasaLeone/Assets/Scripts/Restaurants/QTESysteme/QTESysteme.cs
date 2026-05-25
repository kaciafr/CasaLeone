using System;
using System.Collections.Generic;
using Players;
using Players.Interaction;
using Players.Inventories;
using Sound;
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
		
		public ObjetBaseInteractable interactObj;
		

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
			if(isStarted || qteStart) return;
			Debug.Log(listener);
			isStarted =  true;
			currentQteListen = listener;
			var currentInv = interactObj.currentPlayer;

			PlayerInput playersInputs = currentInv.playerMovement;
			currentInput = playersInputs;
			currentInput.SwitchCurrentActionMap("UI");
			
			
			listener.OnQteStart();
			currentRound = listener.QteRound();
			
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
				SoundManager.Instance.PlaySFX(SoundType.Qte);
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
			SoundManager.Instance.StopSFX();
			SoundManager.Instance.PlaySFX(SoundType.QteSucces);
			currentInput.SwitchCurrentActionMap("Player");

			GlobalPlayer currentPlayer = interactObj.currentPlayer;
			currentQteListen.OnQteSucces(currentPlayer);
			isStarted = false;
			qteStart = false;
			interactObj.currentPlayer = null;
			onSuccess?.Invoke();

		}

		// ReSharper disable Unity.PerformanceAnalysis
		void Lose()
		{
			SoundManager.Instance.StopSFX();
			SoundManager.Instance.PlaySFX(SoundType.QteFail);
			currentInput.SwitchCurrentActionMap("Player");
			interactObj.currentPlayer = null;
			qteStart = false;
			isStarted = false;
			currentQteListen.OnQteFail();
			onLose?.Invoke();
			
		}
	}
}