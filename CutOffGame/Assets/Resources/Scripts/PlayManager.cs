using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    CharacterController controller;
    Animator animator;
    float gravity = 150;
    float speed = 2;
    Vector2 velocity = Vector2.zero;
    Vector3 DesPos = Vector3.zero;
    int isTurnLeft = 1;
    // Use this for initialization
    void Start () {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        DesPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //if(Input.touchCount == 1)
        //{
        //    if(Input.touches[0].phase == TouchPhase.Began)
        //    {
        //        //触屏点击
        //    }
        //    else if(Input.touches[0].phase == TouchPhase.Canceled)
        //    {
        //        Application.Quit();
        //    }
        //}
        velocity.x = 0;
        if (Input.GetMouseButtonDown(0))
        {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log(hit.transform.name);
				if (hit.transform.name.Equals ("Land")) {
					;
				} else {
					return;
				}
				//Debug.Log(hit.transform.tag);
			}

            Vector3 MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));
            MousePos.y = this.transform.position.y;
            MousePos.z = this.transform.position.z;
            if (Vector3.Distance(MousePos, this.transform.position) > 0.1)
            {
                DesPos = MousePos;
                DesPos.y = this.transform.position.y;
                DesPos.z = this.transform.position.z;
            }
        }
        if (DesPos.x < this.transform.position.x)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            isTurnLeft = -1;
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isTurnLeft = 1;
        }

        DesPos.y = this.transform.position.y;
        DesPos.z = this.transform.position.z;
        if (Vector3.Distance(DesPos, this.transform.position) > 0.1)
        {
            animator.SetBool("walk", true);
            velocity.x = speed * isTurnLeft;
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if(!controller.isGrounded)
        {
            velocity.y = -gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }
        controller.Move(new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime);
    }

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Trap") {
			Debug.Log ("You die!");
			return;
		}
		if (hit.rigidbody == null || hit.gameObject.name.Equals ("boat")) {
			Debug.Log ("boat boat");
			return;
		}
		print (hit.gameObject.name);
		Vector3 contactPos = hit.point;
		Vector3 gravity = new Vector3 (0, -1, 0);
		/*
		if ((controller.collisionFlags & CollisionFlags.Below) != 0) {
			hit.rigidbody.AddForceAtPosition (gravity, contactPos);
		}
*/
		if (controller.collisionFlags == CollisionFlags.Below) {
			hit.rigidbody.AddForceAtPosition (gravity, contactPos);
		}
	}
}
