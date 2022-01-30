using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour	{

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.GetComponent<PlayerHealth>()) {
			collision.GetComponent<PlayerHealth>().SetNewCheckPoint(this.gameObject);
		}
	}
}
