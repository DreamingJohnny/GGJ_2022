using System;
using UnityEngine;

public class Door : ShiftBehaviour
{
	[SerializeField] private Collider2D d2Collider;

	private Rigidbody[] chunks;

	protected override void Start()
	{
		chunks = GetComponentsInChildren<Rigidbody>();

		base.Start();
	}

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			d2Collider.enabled = false;
		}
		else
		{
			d2Collider.enabled = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (CurrentWorldState == WorldState.RealWorld &&
			other.gameObject.layer == LayerMask.NameToLayer("Boulder"))
		{
			foreach (Rigidbody chunk in chunks)
			{
				chunk.isKinematic = false;
			}
		}
	}
}
