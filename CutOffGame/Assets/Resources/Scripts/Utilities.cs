using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using UnityEngine;

[SerializeField]
public class User
{
    public string Username;
    public string Password;
    public int Score;

    public static User CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<User>(json);
    }
}

public class Utilities : MonoBehaviour {
	string host_url = "http://120.79.220.178:2222";
	string version = "/v1";
	string allRank;
	string addScoreResult;

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (email_text + password_text);
	}

	public void getAllRank() {
		//Debug.Log ("Get Rank");
		string action = "/rank";
		string url = host_url + version + action;
		StartCoroutine(SendGet (url,action));
		return ;
	}

	public string addScore(string username) {
		string action = "/addScore?username=" + username;
		string url = host_url + version + action;
		StartCoroutine (SendGet (url,action));
		return addScoreResult;
	}

	IEnumerator SendGet(string _url,string action)  
	{  
		//Debug.Log ("Send Rank Request");
		WWW getData = new WWW(_url);  
		yield return getData;  
		if(getData.error != null)  
		{  
			Debug.Log(getData.error);  
		}  
		else  
		{  
			if (action == "/rank") {
				allRank = getData.text;
			} else {
				addScoreResult = getData.text;
			}
            Debug.Log(getData.text);
            User[] Rankdata = new User[10];
            Rankdata = JsonUtility.FromJson<User[]>(allRank);
            Debug.Log(Rankdata[0].Score);
            Debug.Log(Rankdata[1].Score);
            Debug.Log(Rankdata[9].Score);
        }  
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
}
