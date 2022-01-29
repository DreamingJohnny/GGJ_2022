using System;
using UnityEngine;

public class Door : ShiftBehaviour
{
	[SerializeField] private Collider2D boulderBlocker;
	[SerializeField] private Collider2D playerBlocker;

	private Rigidbody[] chunks;
	private bool isBroken;

	protected override void Start()
	{
		chunks = GetComponentsInChildren<Rigidbody>();

		base.Start();
	}

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			boulderBlocker.enabled = false;

			if (!isBroken)
				playerBlocker.enabled = true;
		}
		else
		{
			if (!isBroken)
			{
				boulderBlocker.enabled = true;
				playerBlocker.enabled = true;
			}
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

			isBroken = true;
			playerBlocker.enabled = false;
		}
	}
}
