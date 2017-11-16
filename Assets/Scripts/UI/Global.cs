using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    public static bool PreWorld = true;

    public void killWorld()
    {
        Application.Quit();
    }
}
