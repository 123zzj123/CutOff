using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    bool menuShow = false;
    public GameObject Menu;
    public GameObject ReloadGame;
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setmenu()
    {
        player.gameObject.GetComponent<PlayManager>().enabled = menuShow;
        menuShow = !menuShow;
        Menu.SetActive(menuShow);
        ReloadGame.SetActive(menuShow);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SSDirector.CurrentScene);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void LoadNextScene()
    {
        SSDirector.SceneID++;
        SSDirector.SceneID %= 4;
        string SceneName = "Scene" + SSDirector.SceneID;
        SSDirector.CurrentScene = SceneName;
    }
}
