using System;
using UnityEngine;

public class Water : ShiftBehaviour
{
	private new Collider2D collider;

	private void Awake()
	{
		collider = GetComponent<Collider2D>();
	}

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			collider.isTrigger = true;
			collider.usedByEffector = true;
		}
		else
		{
			collider.isTrigger = false;
			collider.usedByEffector = false;
		}
	}
}
