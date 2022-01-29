using System;
using UnityEngine;

public class Leaf : MonoBehaviour
{
	private void Start()
	{
		ShiftManager.Instance.StateChanged += OnStateChanged;
		OnStateChanged(ShiftManager.Instance.CurrentState);
	}

	private void OnStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			GetComponent<Collider2D>().enabled = false;
		}
		else
		{
			GetComponent<Collider2D>().enabled = true;
		}
	}
}