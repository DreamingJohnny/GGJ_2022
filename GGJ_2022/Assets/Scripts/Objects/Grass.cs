using System;
using UnityEngine;

public class Grass : ShiftBehaviour
{
	[SerializeField] private int damage = 1;

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (CurrentWorldState == WorldState.ShadowLand &&
		    col.gameObject.TryGetComponent(out PlayerHealth playerHealth))
		{
			playerHealth.TakeDamage(damage);
		}
	}
}
