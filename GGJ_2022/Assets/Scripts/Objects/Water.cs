using System;
using UnityEngine;

public class Water : ShiftBehaviour
{
	[SerializeField] private Collider2D waterTrigger;
	[SerializeField] private Collider2D groundCollider;

	private PlayerController capturedPlayer;
	private bool isPlayerFrozen;

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			if (capturedPlayer)
			{
				capturedPlayer.IsFrozenInPlace = false;
				isPlayerFrozen = false;
			}

			waterTrigger.enabled = true;
			groundCollider.enabled = false;
			gameObject.layer = LayerMask.NameToLayer("Water");
		}
		else
		{
			if (capturedPlayer)
			{
				capturedPlayer.IsFrozenInPlace = true;
				isPlayerFrozen = true;
			}

			waterTrigger.enabled = false;
			groundCollider.enabled = true;
			gameObject.layer = LayerMask.NameToLayer("Ground");
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (isPlayerFrozen)
			return;

		if (other.TryGetComponent(out PlayerController playerController))
		{
			capturedPlayer = playerController;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (isPlayerFrozen)
			return;

		if (other.TryGetComponent(out PlayerController playerController) && capturedPlayer == playerController)
		{
			capturedPlayer = null;
		}
	}
}
