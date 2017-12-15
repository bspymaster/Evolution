using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneWebManager : MonoBehaviour {

    void Start()
    {
        if (Global.PreWeb == true)
        {
            Global.UnlockedGenes.Add(0);
            Global.PreWeb = false;
        }
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(0);
    }

    // Adds node to the list for Upadte Species to add
    public static void AddNode(int nodeVal)
    {
        Global.newGenes.Add(nodeVal);
    }

    public static void RemoveNode(int nodeVal)
    {
        Global.removeGenes.Add(nodeVal);
    }

    // Disables a button object
    public void DisableButton()
    {       
        this.GetComponent<Button>().interactable = false;
    }

    // Alerts UpdateSpecies whenever the player evolves genes
    public void notifyChange()
    {
        if(Global.mutationPoints > 0)
        {
            Global.change = true;
        }
        
    }

    // Hides the Gene Web UI when not in the Gene Web scene.
    public void disableCanvas()
    {
        GameObject.FindGameObjectWithTag("WebCanvas").transform.position = new Vector2(0, -5000);
    }
}


