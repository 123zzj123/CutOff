using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addGravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionStay(Collision collisionInfo) {
		if (collisionInfo.rigidbody == null)
			return;
		print (collisionInfo.rigidbody.name);
		ContactPoint contact = collisionInfo.contacts [0];
		Vector3 contactPos = contact.point;
		Vector3 gravity = new Vector3 (0, 10, 0);
		collisionInfo.rigidbody.AddForceAtPosition (gravity, contactPos);
	}
}
