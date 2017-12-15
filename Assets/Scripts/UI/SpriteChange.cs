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

    // Sets default gene node appearance
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

    // Checks the children of a node and unlocks them on the UI Gene Web
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

    //Activates the node when the gene is selected on the web
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

    //Checks which nodes can now be selected on the web
    private void Update()
    {
        if (Global.UnlockedGenes.Contains(nodeIndex) & button.image.sprite != Added)
        {
            button.image.sprite = Unadded;
            button.interactable = true;
        }
    }
}
