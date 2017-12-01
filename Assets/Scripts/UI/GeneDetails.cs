using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneDetails : MonoBehaviour {

    public static int geneNumber = 0;

    public int getGeneNum()
    {
        return geneNumber;
    }

    public void setGeneNum(int newVal)
    {
        geneNumber = newVal;
    }
}
