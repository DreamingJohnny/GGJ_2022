using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int health = 3;
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private float iFrameTime = 0.5f;
	[SerializeField] public UnityEvent onHealthChanged;
	[SerializeField] private GameObject currentCheckPoint;

	private Rigidbody2D body;
	private float iFrameTimer;

	public int Health => Mathf.Clamp(health, 0, maxHealth);

	public int MaxHealth => maxHealth;

	public void SetNewCheckPoint(GameObject newCheckPoint) {
		currentCheckPoint = newCheckPoint;
		Debug.Log("CheckPoint is set!");
	}

	private void Awake()
	{
		health = maxHealth;

		body = GetComponent<Rigidbody2D>();
		currentCheckPoint = null;

	}

	private void Update()
	{
		iFrameTimer -= Time.deltaTime;
	}

	public bool TakeDamage(int amount)
	{
		if (iFrameTimer > 0f)
			return false;

		health -= amount;
		onHealthChanged.Invoke();

		if (health <= 0)
		{
			Debug.Log("Dead");
			
			if(currentCheckPoint == null) {
				Debug.Log("Game Restarts");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else {
				transform.position = currentCheckPoint.transform.position;
				health = maxHealth;
			}
		}

		iFrameTimer = iFrameTime;
		return true;
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