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
	[SerializeField] private float swimUpDownSpeed = 5f;

	[Header("Audio")]
	[SerializeField] private AudioSource jumpSound;
	[SerializeField] private AudioSource runSound;

	private new Rigidbody2D rigidbody;
	private GroundDetector groundDetector;

	[Header("Readout")]
	public float horizontalInput;
	public float lastNonZeroHorizontalInput;
	public bool jumpInput;
	public bool swimUpHeld;
	public bool swimDownHeld;
	public bool isJumping;
	public bool isJumpingInWater;

	private bool isFrozenInPlace;

	public bool IsFrozenInPlace
	{
		get => isFrozenInPlace;
		set
		{
			isFrozenInPlace = value;
			rigidbody.isKinematic = value;

			if (isFrozenInPlace)
			{
				rigidbody.velocity = Vector2.zero;
				rigidbody.gravityScale = 0f;
			}
			else
			{
				rigidbody.gravityScale = 1f;
			}
		}
	}

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		groundDetector = GetComponentInChildren<GroundDetector>();
	}

	private void Update()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		jumpInput |= Input.GetButtonDown("Jump");
		swimUpHeld = Input.GetButton("Swim Up");
		swimDownHeld = Input.GetButton("Swim Down");

		if (Input.GetButtonDown("Shift World"))
		{
			ShiftManager.Instance.ShiftWorld();
		}

		if (horizontalInput != 0)
			lastNonZeroHorizontalInput = horizontalInput;

		if (groundDetector.isGrounded && Mathf.Abs(rigidbody.velocity.x) > 0.1f)
		{
			if (!runSound.isPlaying)
				runSound.Play();
		}
		else
		{
			runSound.Pause();
		}
	}

	private void FixedUpdate()
	{
		if (IsFrozenInPlace)
			return;

		Vector2 velocity = rigidbody.velocity;

		HandleMovement(ref velocity);
		HandleJumping(ref velocity);

		rigidbody.velocity = velocity;
	}

	private void HandleMovement(ref Vector2 velocity)
	{
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
	}

	private void HandleJumping(ref Vector2 velocity)
	{
		if (groundDetector.isGrounded)
		{
			isJumping = false;
		}

		if (groundDetector.isInWater)
		{
			if (swimUpHeld)
				velocity.y = swimUpDownSpeed;
			else if (swimDownHeld)
				velocity.y = -swimUpDownSpeed;
		}

		if (jumpInput)
		{
			// Always consume the input
			jumpInput = false;

			if (groundDetector.isGrounded && !groundDetector.isInWater)
			{
				if (!isJumping)
				{
					velocity.y += jumpVelocity;
					isJumping = true;

					jumpSound.Play();
				}
			}
		}
	}

	public void DisablePlayer()
	{
		enabled = false;
		horizontalInput = 0f;
		rigidbody.velocity = Vector2.zero;
		runSound.Stop();
	}
}
