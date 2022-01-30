using System;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] private AnimationClip landAnimation;
	[SerializeField] private AnimationClip takeDamageAnimation;

	private Animator animator;
	private PlayerController playerController;
	private GroundDetector groundDetector;
	private SpriteRenderer spriteRenderer;
	private new Rigidbody2D rigidbody;

	private float lastNonZeroHorizontalInput = 1f;
	private float animationWaitTimer = 0f;
	private bool wasJumping;

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
		animationWaitTimer -= Time.deltaTime;
		if (animationWaitTimer > 0f)
			return;

		if (playerController.horizontalInput != 0)
			lastNonZeroHorizontalInput = playerController.horizontalInput;

		Vector2 velocity = rigidbody.velocity;
		bool isMoving = Mathf.Abs(velocity.x) > 0.1f;

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
			if (playerController.isJumping && velocity.y > 0)
				animator.Play("Jump");
			else
				animator.Play("In Air");
		}

		wasJumping = playerController.isJumping;
	}

	private void WaitFor(float seconds)
	{
		animationWaitTimer = seconds;
	}

	public void PlayTakeDamageAnimation()
	{
		animator.Play("Take Damage");
		WaitFor(takeDamageAnimation.length);
	}
}
