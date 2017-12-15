using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneName : MonoBehaviour {

    private bool isOn = false;
    private int nodeIndex;
    public Text textPrefab;

    void Start() {

        // Gets node index from parent game object
        nodeIndex = this.transform.parent.GetComponent<SpriteChange>().getIndex();
        StartCoroutine(Example());
    }

    IEnumerator Example() { 

        yield return new WaitForSeconds(0);

        // Checks Web for the node name to be displayed on each UI gene.
        textPrefab.text = GameObject.Find("Web Builder")
            .GetComponent<buildWeb>()
            .getWeb()
            .getNode(nodeIndex)
            .getName();    
        
    }
}
