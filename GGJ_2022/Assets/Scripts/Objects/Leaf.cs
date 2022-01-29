using System;
using UnityEngine;

public class Leaf : MonoBehaviour
{
	private void Start()
	{
		ShiftManager.Instance.WorldStateChanged += OnWorldStateChanged;
		OnWorldStateChanged(ShiftManager.Instance.CurrentWorldState);
	}

	private void OnWorldStateChanged(WorldState state)
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