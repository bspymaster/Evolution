using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

    public void ChangeToScene(int sceneToChangeTo)
    {
        if(sceneToChangeTo == 1)
        {
            //GameObject.Find("WebCanvas").SetActive(true);
        }
        Application.LoadLevel(sceneToChangeTo);
    }
}
