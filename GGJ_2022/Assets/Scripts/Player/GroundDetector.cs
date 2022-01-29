using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
	[SerializeField] private LayerMask groundLayer = 1 << 6;
	[SerializeField] private LayerMask waterLayer = 1 << 4;
	[SerializeField] private Collider2D groundCheckCollider;
	[SerializeField] private Collider2D waterCheckCollider;

	[Header("Readout")]
	public bool isGrounded;
	public bool isInWater;

	private void FixedUpdate()
	{
		isGrounded = groundCheckCollider.IsTouchingLayers(groundLayer);
		isInWater = waterCheckCollider.IsTouchingLayers(waterLayer);

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
}
