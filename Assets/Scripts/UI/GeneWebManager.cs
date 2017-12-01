using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneWebManager : MonoBehaviour {


    public static void AddNode(int nodeVal)
    {
        Global.newGenes.Add(nodeVal);
    }

    public static void RemoveNode(int nodeVal)
    {
        Global.removeGenes.Remove(nodeVal);
    }

    public void DisableButton()
    {
        
        this.GetComponent<Button>().interactable = false;
    }

    /*public void ReadStats()
    {
        foreach(var gene in Global.newGenes)
        {
            print(Global.newGenes[gene]);
        }   
    }*/

    public void notifyChange()
    {
        Global.change = true;
    }
}


