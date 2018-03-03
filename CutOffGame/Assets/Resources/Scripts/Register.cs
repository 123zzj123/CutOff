using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour {
	string host_url = "http://120.79.220.178:2222";
	string version = "/v1";

	public Text username;
	public Text password;
	public Text confirm_password;

	public Button confirm;
	public Button cancel;
	// Use this for initialization
	void Start () {
		confirm.onClick.AddListener(Post);
		cancel.onClick.AddListener(JumpToLogin);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Post() {
		string username_text = username.text;
		string password_text = password.text;
		string confirm_password_text = confirm_password.text;
		string action = "/users";
		string url = host_url + version + action;
		WWWForm form = new WWWForm();
		form.AddField("username", username_text);
		form.AddField("password", password_text);
		StartCoroutine(SendPost(url, form));
		//SceneManager.LoadScene("Login");
	}

	IEnumerator SendPost(string _url, WWWForm _wForm)  
    {  
        WWW postData = new WWW(_url, _wForm);  
        yield return postData;  
        if (postData.error != null)
        {  
            Debug.Log(postData.error);  
        }  
        else
        {  
            Debug.Log(postData.text);  
        }
    }

    void JumpToLogin() {
		SceneManager.LoadScene("Login");
    }
}
