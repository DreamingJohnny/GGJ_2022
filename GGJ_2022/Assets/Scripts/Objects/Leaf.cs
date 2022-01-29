using System;
using UnityEngine;

public class Leaf : MonoBehaviour
{

	//So, need way to move... drifting so left to right, but also always downwards.
	//So, is lerping the way to go? Learp times speed time delta time?
	//But also, we want to be able to set the edges or maximum values hm... Should that then be a diff from startpoint?

	[SerializeField] private float maxXDiff;
	[SerializeField] private float fallSpeed;

	private void Start()
	{
		ShiftManager.Instance.WorldStateChanged += OnWorldStateChanged;
		OnWorldStateChanged(ShiftManager.Instance.CurrentWorldState);
	}

	private void FixedUpdate() {
		transform.position += new Vector3(0, (fallSpeed * Time.deltaTime));
		//new Vector3(0, fallSpeed * Time.deltaTime);
	}

	private void OnWorldStateChanged(WorldState state)
	{
		if (state == WorldState.RealWorld)
		{
			GetComponent<Collider2D>().enabled = false;
		}
		else
		{
			GetComponent<Collider2D>().enabled = true;
		}
	}
}