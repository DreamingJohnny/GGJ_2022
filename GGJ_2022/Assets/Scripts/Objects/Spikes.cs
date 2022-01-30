using System;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	[SerializeField] private int damage = 1;
	[SerializeField] private float knockbackForce = 10f;

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.TryGetComponent(out PlayerHealth playerHealth))
		{
			playerHealth.TakeDamage(damage);
			playerHealth.Knockback(col.GetContact(0).point, knockbackForce);
		}
	}
}
