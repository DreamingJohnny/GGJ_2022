using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 50f;
	[SerializeField] private float maxSpeed = 15f;
	[SerializeField] private float deceleration = 0.9f;
	[SerializeField] private float jumpVelocity = 25f;

	private new Rigidbody2D rigidbody;
	private float horizontalInput;
	private bool jumpInput;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		jumpInput |= Input.GetButtonDown("Jump");

		if (Input.GetButtonDown("Shift World"))
		{
			ShiftManager.Instance.ShiftWorld();
		}
	}

	private void FixedUpdate()
	{
		Vector2 velocity = rigidbody.velocity;

		if (horizontalInput != 0)
		{
			float movementDelta = horizontalInput * movementSpeed * Time.deltaTime;
			velocity += new Vector2(movementDelta, 0f);
			velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
		}
		else
		{
			if (Mathf.Abs(velocity.x) > 0.01f)
			{
				velocity.x = Mathf.MoveTowards(velocity.x, 0, Mathf.Abs(velocity.x) * deceleration * Time.deltaTime);
			}
			else
			{
				velocity.x = 0f;
			}
		}

		if (jumpInput)
		{
			velocity.y += jumpVelocity;

			jumpInput = false;
		}

		rigidbody.velocity = velocity;
	}
}
