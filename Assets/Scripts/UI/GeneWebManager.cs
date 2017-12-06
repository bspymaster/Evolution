using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneWebManager : MonoBehaviour {

    List<int> UnlockedGenes = new List<int>();

    void Start()
    {

        
        StartCoroutine(Example());

    }

    IEnumerator Example()
    {

        yield return new WaitForSeconds(0);
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

    public void disableCanvas()
    {
        GameObject.FindGameObjectWithTag("WebCanvas").transform.position = new Vector2(0, -500);
    }
}


