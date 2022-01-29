using System;
using UnityEngine;

public class FindMainCameraForCanvas : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}
}
