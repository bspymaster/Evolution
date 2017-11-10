using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject WorldMakerPrefab;
    public GameObject GeneWebButtonPrefab;
    public GameObject ExitGameButtonPrefab;

    public void BuildUI()
    {
        GameObject geneButton = Instantiate(GeneWebButtonPrefab, new Vector3(650, 180, 0), Quaternion.identity) as GameObject;
        geneButton.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        GameObject exitButton = Instantiate(ExitGameButtonPrefab, new Vector3(650, 260, 0), Quaternion.identity) as GameObject;
        exitButton.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    void Start()
    {
        if (Global.PreWorld == true)
        {
            GameObject worldButton = Instantiate(WorldMakerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            worldButton.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        }else
        {
            BuildUI();
        }
    }

    public void KillManager()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WindowManager");
        foreach(GameObject manager in managers)
        GameObject.Destroy(manager);
    }

    public void KillWorldMaker()
    {
        Global.PreWorld = false;
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WorldMaker");
        foreach (GameObject manager in managers)
        GameObject.Destroy(manager);
    }

    

}
