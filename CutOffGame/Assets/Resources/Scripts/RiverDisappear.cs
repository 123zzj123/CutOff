using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverDisappear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals ("Sun")) {
			Debug.Log ("river disappear");
			Object.DestroyObject (collision.gameObject);
			Object.DestroyObject (this.gameObject);
		}
	}
}
