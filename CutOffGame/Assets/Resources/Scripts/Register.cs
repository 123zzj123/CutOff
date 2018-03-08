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

	public Button confirm;
    public GameObject RegisterFail;
	public GameObject NullFail;
	// Use this for initialization
	void Start () {
		confirm.onClick.AddListener(Post);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Post() {
		string username_text = username.text;
		string password_text = password.text;
		if (username_text == "" || password_text == "") {
			NullFail.SetActive (true);
			StartCoroutine (NullDispear ());
		}
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
            StartCoroutine(Dispear());
            RegisterFail.SetActive(true);  
        }  
        else
        {
            User _user = User.CreateFromJSON(postData.text.ToString());
            SSDirector.ID = _user.Username;
            SceneManager.LoadScene("menu");
        }
    }

    public IEnumerator Dispear()
    {
        yield return new WaitForSeconds(1);
        RegisterFail.SetActive(false);
    }

	public IEnumerator NullDispear()
	{
		yield return new WaitForSeconds(2);
		NullFail.SetActive(false);
	}
}
