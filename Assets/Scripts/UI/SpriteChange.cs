using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {

    public int nodeIndex;
    public bool Active = false;
    public Button button;
    public Sprite Unadded;
    public Sprite Added;
    public Sprite Locked;
    public List<int> nodeChildren = new List<int>();


    private void Start()
    {
        button.image.sprite = Locked;
        
    }

    //Returns the node's index number
    public int getIndex()
    {
        return nodeIndex;
    }

    //Sets the node's index number
    public void setIndex(int ind)
    {
        nodeIndex = ind;
    }


    public void getTheChildren()
    {
        nodeChildren = GameObject.Find("Web Builder")
            .GetComponent<buildWeb>()
            .getWeb()
            .getChildren(nodeIndex);

        foreach (int child in nodeChildren)
        {
            Global.UnlockedGenes.Add(child);
        }
    }


    public void toggle2()
    {   
        if (Active == false)
        {
            button.image.sprite = Added;
            Global.newGenes.Add(nodeIndex);
            Active = true;
        }
        /*
        else if(button.image.sprite == Added)
        {
            button.image.sprite = Unadded;
        } */
    }


    private void Update()
    {
        if (Global.UnlockedGenes.Contains(nodeIndex) & button.image.sprite != Added)
        {
            button.image.sprite = Unadded;
            button.interactable = true;
        }
    }
}
