using System;
using System.Collections.Generic;
using UnityEngine;

public class TransformeUiQte : MonoBehaviour
{
	[SerializeField] private GameObject UiLoc;

	public void UiTransform(Transform obj)
	{
		UiLoc.transform.position = obj.position;
		Debug.Log("JE SUIS LA");
	}
}
