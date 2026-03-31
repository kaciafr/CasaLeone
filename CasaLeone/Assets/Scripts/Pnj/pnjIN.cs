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
		Debug.Log("un poil chiant");
	}

	public void Leave()
	{
		currentPNJ = null;
		canGoIn = true;
	}
}

