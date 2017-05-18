//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Rabbit : MonoBehaviour {
//	public float speed = 50f;
//	public float jumpPower = 150f;
//	public bool grounded;
//
//	private Rigidbody2D rb2d;
//	// Use this for initialization
//	void Start () {
//		rb2d = gameObject.GetComponent<Rigidbody2D>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		float h = Input.GetAxis ("Horizontal");
//		rb2d.AddForce ((Vector2.right * speed) * h);
//	}
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
	public float speed = 1;
	Rigidbody2D myBody = null;
	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate () { //[-1, 1]
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		} 
		SpriteRenderer sr = GetComponent<SpriteRenderer>(); if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
	}
}