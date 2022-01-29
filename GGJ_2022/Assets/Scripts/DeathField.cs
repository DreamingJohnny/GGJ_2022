using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathField : MonoBehaviour {

	const int ultimateDeathDamage = 100;

	public void OnTriggerEnter2D(Collider2D other) {
		if(other.GetComponent<PlayerHealth>()) {
			other.GetComponent<PlayerHealth>().TakeDamage(ultimateDeathDamage);
		}
	}
}