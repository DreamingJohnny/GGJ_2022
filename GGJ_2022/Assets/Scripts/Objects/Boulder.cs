using System;
using UnityEngine;

public class Boulder : ShiftBehaviour
{
	[SerializeField] private float normalMass = 10;
	[SerializeField] private float shadowWorldMass = 1;
	[SerializeField] private Transform modelToRotate;
	[SerializeField] private AudioSource rollingSound;
	[SerializeField] private Vector2 volumeBasedOnSpeed = new Vector2(0.05f, 5f);

	private new CircleCollider2D collider;
	private Rigidbody2D body;

	protected override void Start()
	{
		collider = GetComponent<CircleCollider2D>();
		body = GetComponent<Rigidbody2D>();

		base.Start();
	}

	private void Update()
	{
		float angle = (transform.position.x / collider.radius) * Mathf.Rad2Deg;
		modelToRotate.eulerAngles = new Vector3(0f, 0f, -angle);
	}

	private void FixedUpdate()
	{
		Vector2 velocity = body.velocity;

		if (collider.IsTouchingLayers(1 << LayerMask.NameToLayer("Ground")) && Mathf.Abs(velocity.x) > 0.05f)
		{
			if (!rollingSound.isPlaying)
				rollingSound.Play();

			float volume = Mathf.InverseLerp(volumeBasedOnSpeed.x, volumeBasedOnSpeed.y, Mathf.Abs(velocity.x));
			rollingSound.volume = volume;
		}
		else
		{
			rollingSound.Stop();
		}
	}

	protected override void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			GetComponent<Rigidbody2D>().mass = normalMass;
		}
		else
		{
			GetComponent<Rigidbody2D>().mass = shadowWorldMass;
		}
	}
}
