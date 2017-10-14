using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileData : MonoBehaviour {
    public int numBerries;
    private int numNuts;
    private int numGrass;
    private int numLeaves;
    private int numAmbientMeat;
    private string tileType;

    public TileData()
    {
        tileType = "Forest";
        numBerries = 0;
        numNuts = 0;
        numGrass = 0;
        numLeaves = 0;
        numAmbientMeat = 0;
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

    
}


    
    





