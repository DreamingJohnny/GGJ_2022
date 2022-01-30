using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int health = 3;
	[SerializeField] private int maxHealth = 3;
	[SerializeField] public UnityEvent onHealthChanged;

	private Rigidbody2D body;

	public int Health => Mathf.Clamp(health, 0, maxHealth);

	public int MaxHealth => maxHealth;

	private void Awake()
	{
		health = maxHealth;
		body = GetComponent<Rigidbody2D>();
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		onHealthChanged.Invoke();

		if (health <= 0)
		{
			Debug.Log("Dead");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void Knockback(Vector3 sourcePosition, float force)
	{
		Vector2 knockbackDirection = ((Vector2)transform.position - (Vector2)sourcePosition).normalized;
		body.AddForce(knockbackDirection * force, ForceMode2D.Impulse);
	}

	[ContextMenu("Take One Damage")]
	private void TakeOneDamage()
	{
		TakeDamage(1);
	}
}
