using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyManager : MonoBehaviour
{

    public void KillManager()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WindowManager");
        foreach(GameObject manager in managers)
        GameObject.Destroy(manager);
    }
}
