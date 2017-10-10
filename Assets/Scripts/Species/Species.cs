using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour
{
    private string name;  // name of species - for stretch goal, we will want this to be a string that appears like formal latin names, by D3, number will suffice
    private int[] location; // in which tiles this species exists.  Assuming tiles can be simplified to their numerical value
    private int[] genes;  // what genes this species has.  Assuming genes can be simplified to their numerical value

    public void spawn(string n, int loc) { // meaning the new species is made by adding new gene. set species to be made as a copy of evolving species

    }
    public void spawnOnGameStart() { // species creation on world creation on game start
        var rnd = new System.Random();
        // int[] exist = new int[10]; used for later to avoid multiple species on a single tile at game start
        for (int i = 0; i < 10; i++) {
            int loc = rnd.Next(0, 40000);
            spawn(i.ToString(), loc);
            // exist[i] = loc; used for later to avoid multiple species on a single tile at game start
        }
    }
    public string getName() {
        return name;
    }
    public int[] getLocation() {
        return location;
    }
    public int[] getGenes() {
        return genes;
    }
    public void setName(string newName) {
        name = newName;
    }
    public void setLocation(int[] newLocation) {
        location = newLocation;
    }
    public void setGenes(int[] newGenes) {
        genes = newGenes;
    }
}