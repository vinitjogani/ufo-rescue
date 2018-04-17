using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

	public int speedX;
	public int speedY;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = (GetComponent<Rigidbody2D> ());
	}

	void Update (){ 
		rb.velocity = new Vector2 (speedX, speedY);
		rb.angularVelocity = 0;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Background") {
			speedX = -speedX;
			speedY = -speedY;
			rb.transform.Rotate (0, 180, 0);
		}
	}
}
