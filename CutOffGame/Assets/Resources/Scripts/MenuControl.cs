using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {
    public GameObject SceneChoose;
    public GameObject HelpPanel;
    public GameObject SettingPanel;
    public GameObject RankPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showSceneChoose()
    {
        SceneChoose.SetActive(true);
        HelpPanel.SetActive(false);
        SettingPanel.SetActive(false);
        RankPanel.SetActive(false);
    }

    public void showHelpPanel()
    {
        SceneChoose.SetActive(false);
        HelpPanel.SetActive(true);
        SettingPanel.SetActive(false);
        RankPanel.SetActive(false);
    }

    public void showSettingPanel()
    {
        SceneChoose.SetActive(false);
        HelpPanel.SetActive(false);
        SettingPanel.SetActive(true);
        RankPanel.SetActive(false);
    }

    public void showRankPanel()
    {
        SceneChoose.SetActive(false);
        HelpPanel.SetActive(false);
        SettingPanel.SetActive(false);
        RankPanel.SetActive(true);
    }

    public void ChooseScene(int num)
    {
        string SceneName = "Scene" + num;
        SSDirector.CurrentScene = SceneName;
        SSDirector.SceneID = num;
        SceneManager.LoadScene(SceneName);
    }
}
