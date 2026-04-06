using System;
using UnityEngine;

public class PnjIn : MonoBehaviour
{
	public bool canGoIn;
	private GameObject currentPnj;
	public void Reserve(GameObject pnj)
	{
		canGoIn = false;
		currentPnj = pnj;
		Debug.Log(pnj);
	}

	public void Leave()
	{
		currentPnj = null;
		canGoIn = true;
	}
}

