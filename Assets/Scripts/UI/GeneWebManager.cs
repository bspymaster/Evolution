using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneWebManager : MonoBehaviour {

    void Start()
    {
        StartCoroutine(Example());

    }

    IEnumerator Example()
    {

        yield return new WaitForSeconds(1);
        //Global.PreWeb = false;

    }

    public static void AddNode(int nodeVal)
    {
        Global.newGenes.Add(nodeVal);
    }

    public static void RemoveNode(int nodeVal)
    {
        Global.removeGenes.Add(nodeVal);
    }

    public void DisableButton()
    {
        
        this.GetComponent<Button>().interactable = false;
    }

    public void notifyChange()
    {
        Global.change = true;
    }
}


