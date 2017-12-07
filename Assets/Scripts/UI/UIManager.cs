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
    public Text MutationPointsTextPrefab;

    public void BuildUI()
    {

        ExitGameButtonPrefab.SetActive(true);
        /*
        GameObject geneButton = Instantiate(GeneWebButtonPrefab) as GameObject;
        geneButton.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        GameObject exitButton = Instantiate(ExitGameButtonPrefab) as GameObject;
        exitButton.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        GameObject pointsBack = Instantiate(MutationPointsBackPrefab) as GameObject;
        pointsBack.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Text pointsText = Instantiate(MutationPointsTextPrefab) as Text;
        pointsText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        */
    }

    void Start()
    {
        if (Global.PreWorld == true)
        {

            GeneWebButtonPrefab.SetActive(false);
            ExitGameButtonPrefab.SetActive(false);
            MutationPointsBackPrefab.SetActive(false);
            MutationPointsTextPrefab.enabled = false;
            //GameObject.FindGameObjectWithTag("Canvas").transform.position = new Vector2(0, 30);

            //GameObject worldButton = Instantiate(WorldMakerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            //worldButton.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        }
        else
        {
            
            WorldMakerPrefab.SetActive(false);
        }
    }

    public void KillManager()
    {
        Global.cameraLock = false;
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WindowManager");
        foreach(GameObject manager in managers)
        GameObject.Destroy(manager);
    }

    public void KillWorldMaker()
    {
        
        Global.PreWorld = false;
        GameObject[] managers = GameObject.FindGameObjectsWithTag("WorldMaker");
        foreach (GameObject manager in managers)
        manager.SetActive(false);
    }

    

}
