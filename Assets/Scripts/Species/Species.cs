using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour
{
    private string name;  // name of species - for stretch goal, we will want this to be a string that appears like formal latin names, by D3, number will suffice
    private int[] location; // in which tiles this species exists.  Assuming tiles can be simplified to their numerical value
    private int[] genes;  // what genes this species has.  Assuming genes can be simplified to their numerical value

    public void spawn(bool evolve) { // make new object for species in unity - must work with Kyle's procedural generation for D3; for D4, must also allow for new species from evolution
        if (evolve) { // meaning the new species is made by adding new gene

        }
        else { // meaning the species spawned is from world creation (starting species)

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