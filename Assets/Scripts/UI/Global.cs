using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    public static bool PreWorld = true;
    public static List<int> playerSpeciesGeneList;
    public static List<int> newGenes;
    public static List<int> removeGenes;
    public static int mutationPoints = 0;

    public void killWorld()
    {
        Application.Quit();
    }
}
