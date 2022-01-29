using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour {

	[SerializeField] private GameObject leaf;

	[SerializeField] float maxTimeSpan = 4.0f;
	float currentTime;

	void Start() {
		currentTime = 0f;
	}

	void Update() {

		if (currentTime < maxTimeSpan) {
			currentTime += Time.deltaTime;
		}
		else {
			currentTime = 0f;
			Instantiate<GameObject>(leaf,transform);
		}
	}
}
