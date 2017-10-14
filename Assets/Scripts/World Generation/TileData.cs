using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData:MonoBehaviour {
    private int numBerries;
    private int numNuts;
    private int numGrass;
    private int numLeaves;
    private int numAmbientMeat;
    private string tileType;
    private Dictionary<string, int> localSpecies;

    public TileData()
    {
        tileType = "Forest";
        numBerries = 0;
        numNuts = 0;
        numGrass = 0;
        numLeaves = 0;
        numAmbientMeat = 0;
        localSpecies = new Dictionary<string, int>();
    }

    public int getSpeciesPopulation(string key)
    {
        return localSpecies[key];
    }
    public string getTileType()
    {
        return tileType;
    }
    public int getNumBerries()
    {
        return numBerries;
    }
    public int getNumNuts()
    {
        return numNuts;
    }
    public int getNumGrass()
    {
        return numGrass;
    }
    public int getNumLeaves()
    {
        return numLeaves;
    }
    public int getNumAmbientMeat()
    {
        return numAmbientMeat;
    }
    public Dictionary<string, int> getLocalSpecies()
    {
        return localSpecies;
    }

    public void setTileType(string tileType)
    {
        this.tileType = tileType;
    }
    public void setNumBerries(int numBerries)
    {
        this.numBerries = numBerries;
    }
    public void setNumNuts(int numNuts)
    {
        this.numNuts = numNuts;
    }
    public void setNumGrass(int numGrass)
    {
        this.numGrass = numGrass;
    }
    public void setNumLeaves(int numLeaves)
    {
        this.numLeaves = numLeaves;
    }
    public void setNumAmbientMeat(int numAmbientMeat)
    {
        this.numAmbientMeat = numAmbientMeat;
    }
    public void setLocalSpecies(Dictionary<string, int> localSpecies)
    {
        this.localSpecies = localSpecies;
    }
    public void setSpeciesPopulation(string key, int population)
    {
        localSpecies[key] = population;
    }
}
