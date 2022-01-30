using System;
using UnityEngine;

public class Grass : ShiftBehaviour
{
	[SerializeField] private int damage = 1;
	[SerializeField] private float knockbackForce = 10f;

	private PlayerHealth playerInTrigger;

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.ShadowLand && playerInTrigger)
		{
			HurtPlayer();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
		{
			playerInTrigger = playerHealth;

			if (CurrentWorldState == WorldState.ShadowLand)
			{
				HurtPlayer();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out PlayerHealth _))
		{
			playerInTrigger = null;
		}
	}

	private void HurtPlayer()
	{
		if (playerInTrigger.TakeDamage(damage))
			playerInTrigger.Knockback(transform.position, knockbackForce);
	}
}
