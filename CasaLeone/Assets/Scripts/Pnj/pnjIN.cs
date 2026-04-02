using System;
using UnityEngine;

public class pnjIN : MonoBehaviour
{
	public bool canGoIn;
	private GameObject currentPNJ;
	public void Reserve(GameObject pnj)
	{
		canGoIn = false;
		currentPNJ = pnj;
		Debug.Log(pnj);
	}

	public void Leave()
	{
		currentPNJ = null;
		canGoIn = true;
	}
}

