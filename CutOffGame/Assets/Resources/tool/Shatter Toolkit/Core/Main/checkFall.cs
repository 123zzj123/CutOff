using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody newRigidbody = this.GetComponent<Rigidbody>();
		if (newRigidbody != null && !newRigidbody.isKinematic)
		{
			newRigidbody.useGravity = true;
			newRigidbody.constraints =  RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
				RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*
	void OnTriggerExit(Collider other) {
		Rigidbody newRigidbody = this.GetComponent<Rigidbody>();
		if (newRigidbody != null && !newRigidbody.isKinematic)
		{
			newRigidbody.useGravity = true;
		}

	}
*/

	void OnTriggerEnter(Collider other) {
		Rigidbody newRigidbody = this.GetComponent<Rigidbody>();
		Transform transform = this.GetComponent<Transform> ();
		if (newRigidbody != null && !newRigidbody.isKinematic)
		{
			newRigidbody.useGravity = false;
			newRigidbody.velocity = new Vector3 (0, 0, 0);
			newRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
				RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}

	}

	void OnTriggerStay(Collider other) {
		Debug.Log ("a");
		Rigidbody newRigidbody = this.GetComponent<Rigidbody>();
		if (newRigidbody != null && !newRigidbody.isKinematic)
		{
			newRigidbody.useGravity = false;
			newRigidbody.velocity = new Vector3 (0, 0, 0);
			newRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
				RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			
		}

	}

	void OnTriggerExit(Collider other) {
		Rigidbody newRigidbody = this.GetComponent<Rigidbody>();
		Transform transform = this.GetComponent<Transform> ();
		if (newRigidbody != null && !newRigidbody.isKinematic)
		{
			newRigidbody.useGravity = false;
			newRigidbody.velocity = new Vector3 (0, 0, 0);
			newRigidbody.constraints =  RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionZ |
			RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
			
		}

	}
}
