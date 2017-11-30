using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneName : MonoBehaviour {


    
    public GameObject geneNode;
    int nodeIndex;
    public Text textPrefab;

    // Use this for initialization
    void Start() {

        //    Node node = GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getNode(nodeIndex);
        //    string GeneName = node.getName();

        //    textPrefab.text = GeneName;
        //      Text gene = Instantiate(textPrefab, new Vector3(0, 0, -5), Quaternion.identity) as Text;
        StartCoroutine(Example());

    }

    IEnumerator Example() { 

        yield return new WaitForSeconds(1);

        textPrefab.text = GameObject.Find("Web Builder")
            .GetComponent<buildWeb>()
            .getWeb()
            .getNode(nodeIndex)
            .getName();
    }
	
}
