using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyManager : MonoBehaviour
{
    public GameObject WorldObject;

    public void KillManager()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WindowManager");
        foreach(GameObject manager in managers)
        GameObject.Destroy(manager);
    }

    public void KillWorldMaker()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WorldMaker");
        foreach (GameObject manager in managers)
        GameObject.Destroy(manager);
    }

    public void killWorld()
    {
        Application.Quit();
    }

    public void disableWorldMaker()
    {
        WorldObject.SetActive(false);
    }
}
