using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrail : MonoBehaviour {
	private LineRenderer line;  
	private bool isCutting;
	//带有LineRender物体  
	void Start () {  
		line=this.gameObject.GetComponent<LineRenderer>();  
		isCutting = false;
	}  

	// Update is called once per frame  
	void Update () {  
		if (Input.GetMouseButtonDown (0)) { 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject != null && hit.collider.gameObject.tag == "Sky") {
					isCutting = true;
					line.SetPosition(0,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5))); 
				}
			}
		}  
		if(Input.GetMouseButton(0) && isCutting)  
		{  
			//设置顶点位置(顶点的索引，将鼠标点击的屏幕坐标转换为世界坐标)  
			line.SetPosition(1,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5)));  

		}  
		if (Input.GetMouseButtonUp (0)) {
			isCutting = false;
		}
	}  

}
