using System;
using UnityEngine;

public class Door : ShiftBehaviour
{
	[SerializeField] private Collider2D boulderBlocker;
	[SerializeField] private Collider2D playerBlocker;
	[SerializeField] private AudioSource breakSound;

	[SerializeField] private float shakeIntensity = 5f;
	[SerializeField] private float shakeDuration = 0.4f;

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
		if (isBroken)
			return;

		if (CurrentWorldState == WorldState.RealWorld &&
			other.gameObject.layer == LayerMask.NameToLayer("Boulder"))
		{
			foreach (Rigidbody chunk in chunks)
			{
				chunk.isKinematic = false;
			}

			isBroken = true;
			playerBlocker.enabled = false;
			breakSound.Play();

			CameraShake.Instance.Shake(shakeIntensity, shakeDuration);
		}
	}
}
