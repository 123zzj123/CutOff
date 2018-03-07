using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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

[System.Serializable]
public class RankItem
{
    public List<User> inforlist;
}

public class Utilities : MonoBehaviour {
    public List<Text> Name;
    public List<Text> Score;
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
            Debug.Log(allRank);
            RankItem RankData = JsonUtility.FromJson<RankItem>(allRank);
            for (int i = 0; i < 10; i++)
            {
                if(RankData.inforlist[i].Username == "")
                {
                    break;
                }
                Name[i].text = "Name: " + RankData.inforlist[i].Username;
                Score[i].text = "Point: " + RankData.inforlist[i].Score.ToString();
            }
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
