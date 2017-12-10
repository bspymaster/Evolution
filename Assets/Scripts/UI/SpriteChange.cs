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
        if (Global.mutationPoints > 0)
        {
            nodeChildren = GameObject.Find("Web Builder")
            .GetComponent<buildWeb>()
            .getWeb()
            .getChildren(nodeIndex);

            foreach (int child in nodeChildren)
            {
                Global.UnlockedGenes.Add(child);
            }
            Global.mutationPoints--;
        }
        
    }


    public void toggle2()
    {
        if (Global.mutationPoints > 0)
        {
            if (Active == false)
            {
                
                button.image.sprite = Added;
                Global.newGenes.Add(nodeIndex);
                Active = true;
            }
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
