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
		playerInTrigger.TakeDamage(damage);

		Rigidbody2D body = playerInTrigger.GetComponent<Rigidbody2D>();
		Vector2 knockbackDirection = ((Vector2)playerInTrigger.transform.position - (Vector2)transform.position).normalized;
		body.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
	}
}
