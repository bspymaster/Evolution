using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {

    public int nodeIndex;
    public Button button;
    public Sprite Unadded;
    public Sprite Added;
    public Sprite Locked;
    public GameObject webObject;
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
        nodeChildren = webObject
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
        if(button.image.sprite == Unadded)
        {
            button.image.sprite = Added;
        }
        else if(button.image.sprite == Added)
        {
            button.image.sprite = Unadded;
        }     
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
