using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneNum : MonoBehaviour {

    public int geneNum;


    public void Update()
    {
        GeneDetails.geneNumber = geneNum;
    }
}
