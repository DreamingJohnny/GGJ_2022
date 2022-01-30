using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
	[SerializeField] private LayerMask groundLayer = 1 << 6;
	[SerializeField] private LayerMask waterLayer = 1 << 4;
	[SerializeField] private LayerMask movingLayer = 1 << 7;
	[SerializeField] private LayerMask boulderLayer = 1 << 8;

	[SerializeField] private Collider2D groundCheckCollider;
	[SerializeField] private Collider2D waterCheckCollider;
	[SerializeField] private Collider2D boulderCheckCollider;

	[Header("Audio")]
	[SerializeField] private AudioSource waterSplash;
	[SerializeField] private AudioSource underwaterLoop;

	[Header("Readout")]
	public bool isGrounded;
	public bool isInWater;
	public bool isOnMoving;
	public bool isTouchingBoulder;

	private void FixedUpdate()
	{
		bool wasInWater = isInWater;

		isGrounded = groundCheckCollider.IsTouchingLayers(groundLayer);
		isInWater = waterCheckCollider.IsTouchingLayers(waterLayer);
		isOnMoving = groundCheckCollider.IsTouchingLayers(movingLayer);
		isTouchingBoulder = boulderCheckCollider.IsTouchingLayers(boulderLayer);

		if (!wasInWater && isInWater)
		{
			waterSplash.Play();
		}

		if (isInWater)
		{
			if (!underwaterLoop.isPlaying)
				underwaterLoop.Play();
		}
		else
		{
			underwaterLoop.Stop();
		}
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Moving")) {
			transform.parent.SetParent(collision.gameObject.transform);
		} 
	}

	public void OnTriggerExit2D(Collider2D collision) {
		if(collision.gameObject.layer == LayerMask.NameToLayer("Moving")) {
			transform.parent.SetParent(null);
		}
	}
}
