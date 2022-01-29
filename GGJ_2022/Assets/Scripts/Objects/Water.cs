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
			gameObject.layer = LayerMask.NameToLayer("Water");
		}
		else
		{
			collider.isTrigger = false;
			collider.usedByEffector = false;
			gameObject.layer = LayerMask.NameToLayer("Ground");
		}
	}
}
