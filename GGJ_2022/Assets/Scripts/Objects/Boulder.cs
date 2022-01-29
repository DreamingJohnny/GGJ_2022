using System;
using UnityEngine;

public class Boulder : ShiftBehaviour
{
	[SerializeField] private float normalMass = 10;
	[SerializeField] private float shadowWorldMass = 1;
	[SerializeField] private Transform modelToRotate;

	private new CircleCollider2D collider;

	protected override void Start()
	{
		collider = GetComponent<CircleCollider2D>();

		base.Start();
	}

	private void Update()
	{
		float angle = (transform.position.x / collider.radius) * Mathf.Rad2Deg;
		modelToRotate.eulerAngles = new Vector3(0f, 0f, -angle);
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
