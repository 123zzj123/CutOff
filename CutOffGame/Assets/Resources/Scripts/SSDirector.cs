using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {
    private static SSDirector instance;
    public static int CurrentMusic;
    public static float Volume;
    public static string CurrentScene;
    public static string ID;

    public static SSDirector getInstance()
    {
        if (instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }

}
