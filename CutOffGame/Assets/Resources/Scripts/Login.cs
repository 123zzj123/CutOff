using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
	string host_url = "http://47.91.158.181:444";
	string version = "/v1";

	public Text username_;
	public Text password_;

	public Button login_;
	public Button register_;

	string username_text;
	string password_text;
	// Use this for initialization

	void Start () {
		InitialButton();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (email_text + password_text);
	}

	void InitialButton(){
		login_.onClick.AddListener (Get);
		register_.onClick.AddListener (JumpToRegister);
	}

	void Get() {
		username_text = username_.text;
		password_text = password_.text;
		string action = "/auth";
		string url = host_url + version + action + "?username="+username_text+"&password="+password_text;
		StartCoroutine (SendGet (url));
	}
		
	IEnumerator SendGet(string _url)  
	{  
		WWW getData = new WWW(_url);  
		yield return getData;  
		if(getData.error != null)  
		{  
			Debug.Log(getData.error);  
		}  
		else  
		{  
			Debug.Log(getData.text);  
		}  
	}

	void JumpToRegister() {
		SceneManager.LoadScene ("Register");
	}
}