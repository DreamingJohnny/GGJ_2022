using System;
using UnityEngine;

public class Leaf : MonoBehaviour
{

	//So, need way to move... drifting so left to right, but also always downwards.
	//So, is lerping the way to go? Learp times speed time delta time?
	//But also, we want to be able to set the edges or maximum values hm... Should that then be a diff from startpoint?

	[SerializeField] private float xRange;
	[SerializeField] private float fallSpeed;
	[SerializeField] private float totalSpeed;

	private void Start()
	{
		ShiftManager.Instance.WorldStateChanged += OnWorldStateChanged;
		OnWorldStateChanged(ShiftManager.Instance.CurrentWorldState);

		xRange += RandomizedVariance(xRange);
		fallSpeed += RandomizedVariance(fallSpeed);
		totalSpeed += RandomizedVariance(totalSpeed);
	}

	void Update() {
		transform.position += GetWavePostion() * Time.deltaTime;
	}

	/// <summary>
	/// Returns a float that is between 80% and 120% of origvalue;
	/// </summary>
	/// <param name="origValue"></param>
	/// <returns></returns>
	private float RandomizedVariance(float origValue) {
		
	return UnityEngine.Random.Range(-origValue * 0.1f, origValue * 0.1f);
	
	}

	private Vector3 GetWavePostion() {
		float xSpeed = (SinPositionByTime() * xRange);
		 
		return new Vector3(xSpeed,-fallSpeed);
	}

	private float SinPositionByTime() {
		return Mathf.Sin(Time.realtimeSinceStartup);
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