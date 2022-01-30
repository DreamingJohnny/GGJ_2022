using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
	[SerializeField] private UnityEvent onTriggerEnter;
	[SerializeField] private bool playerOnly = true;


	private void OnTriggerEnter2D(Collider2D col)
	{
		if (playerOnly && !col.gameObject.CompareTag("Player"))
			return;

		onTriggerEnter.Invoke();
	}
}
