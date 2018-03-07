using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {
    private static SSDirector instance;
    public static int CurrentMusic = 0;
    public static float Volume = 1;
    public static string CurrentScene;
    public static string ID;
    public static int SceneID = 1;

    public static SSDirector getInstance()
    {
        if (instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }

}
