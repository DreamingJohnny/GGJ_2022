using System;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class PlayerAnimationController : MonoBehaviour
{
	private Animator animator;
	private PlayerController playerController;
	private GroundDetector groundDetector;
	private SpriteRenderer spriteRenderer;
	private new Rigidbody2D rigidbody;

	private float lastNonZeroHorizontalInput = 1f;

	private void Start()
	{
		animator = GetComponent<Animator>();
		playerController = GetComponent<PlayerController>();
		groundDetector = GetComponentInChildren<GroundDetector>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (playerController.horizontalInput != 0)
			lastNonZeroHorizontalInput = playerController.horizontalInput;

		bool isMoving = Mathf.Abs(rigidbody.velocity.x) > 0.05f;

		spriteRenderer.flipX = lastNonZeroHorizontalInput < 0;

		if (groundDetector.isInWater)
		{
			if (isMoving)
				animator.Play("Swim Forward");
			else
				animator.Play("Swim Up");
		}
		else if (groundDetector.isGrounded)
		{
			if (isMoving)
			{
				if (groundDetector.isTouchingBoulder)
					animator.Play("Push");
				else
					animator.Play("Run");
			}
			else
			{
				animator.Play("Idle");
			}
		}
		else
		{
			if (playerController.isJumping)
				animator.Play("Jump");
			else
				animator.Play("In Air");
		}
	}
}
