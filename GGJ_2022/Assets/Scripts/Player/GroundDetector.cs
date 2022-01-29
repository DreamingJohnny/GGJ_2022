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

	[Header("Readout")]
	public bool isGrounded;
	public bool isInWater;
	public bool isOnMoving;
	public bool isTouchingBoulder;

	private void FixedUpdate()
	{
		isGrounded = groundCheckCollider.IsTouchingLayers(groundLayer);
		isInWater = waterCheckCollider.IsTouchingLayers(waterLayer);
		isOnMoving = groundCheckCollider.IsTouchingLayers(movingLayer);
		isTouchingBoulder = boulderCheckCollider.IsTouchingLayers(boulderLayer);

		//new ContactFilter2D(){}

		// Collider2D[] overlaps = Physics2D.OverlapBoxAll(
		// 	transform.TransformPoint(checkCollider.offset),
		// 	checkCollider.size,
		// 	transform.eulerAngles.z);
		//
		// foreach (Collider2D overlappingCollider in overlaps)
		// {
		// 	if (overlappingCollider.IsTouchingLayers())
		// }
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
