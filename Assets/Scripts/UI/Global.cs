using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    public static bool cameraLock = false;
    public static bool PreWorld = true;
    public static bool PreWeb = true;
    public static List<int> playerSpeciesGeneList;
    public static List<int> newGenes = new List<int>();
    public static List<int> removeGenes = new List<int>();
    public static int mutationPoints = 0;
    public static bool change = false;
    public static List<int> UnlockedGenes = new List<int>();

    public void killWorld()
    {
        Application.Quit();
    }
}
