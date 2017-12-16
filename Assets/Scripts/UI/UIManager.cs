using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject WorldMakerPrefab;
    public GameObject GeneWebButtonPrefab;
    public GameObject ExitGameButtonPrefab;
    public GameObject MutationPointsBackPrefab;
    public GameObject AlertPrefab;
    public Text MutationPointsTextPrefab;
    public bool helpText = true;
    public GameObject Right;
    public GameObject Left;
    public Text Page1;
    public Text Page2;

    public void BuildUI()
    {
        ExitGameButtonPrefab.SetActive(true);
    }

    // Initial Tile World scene UI
    void Start()
    {        
        if (Global.PreWorld == true)
        {
            GeneWebButtonPrefab.SetActive(false);
            ExitGameButtonPrefab.SetActive(false);
            MutationPointsBackPrefab.SetActive(false);
            MutationPointsTextPrefab.enabled = false;
        }
        else
        {           
            WorldMakerPrefab.SetActive(false);
        }
    }

    // Destroys the tile info window
    public void KillManager()
    {
        Global.cameraLock = false;
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WindowManager");
        foreach(GameObject manager in managers)
        GameObject.Destroy(manager);
    }

    //Removes the world generation button from the UI
    public void KillWorldMaker()
    {     
        Global.PreWorld = false;
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WorldMaker");
        foreach (GameObject manager in managers)
        manager.SetActive(false);
    }

    // Help screen manager
    public void changeHelp()
    {
        if(helpText == true)
        {
            Left.SetActive(true);
            Page2.enabled = true;
            Right.SetActive(false);
            Page1.enabled = false;
            helpText = false;
        }
        else
        {
            Right.SetActive(true);
            Page1.enabled = true;
            Left.SetActive(false);
            Page2.enabled = false;
            helpText = true;
        }
    }
}
