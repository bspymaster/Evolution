using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour {

    public void Start()
    {
        
        DontDestroyOnLoad(this);
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        GameObject.FindGameObjectWithTag("WebCanvas").transform.position = new Vector2(0, 0);

    }


}
