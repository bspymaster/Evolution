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
    private int altitude;
    private int temperature;
    private Dictionary<int, Species> localSpecies;  // key is species id, value is the species
    private Dictionary<int, int> speciesPopulation; //  key is species id, value is their population
    private Dictionary<int, int> speciesRelation;  //  key is species id, value is their relation to player species; 0 == player, 1 == cohabitable, 2 == competitive
    public GameObject player;
    public GameObject competitive;
    public GameObject cohabitable;

    public TileData()
    {
        tileType = "Forest";
        numBerries = 0;
        numNuts = 0;
        numGrass = 0;
        altitude = 0;
        temperature = 0;
        numLeaves = 0;
        numAmbientMeat = 0;
        localSpecies = new Dictionary<int, Species>();
        speciesPopulation = new Dictionary<int, int>();
        speciesRelation = new Dictionary<int, int>();
}
    
    public void enablePlayer()
    {
        player.SetActive(true);
    }
    public void enableCohabitable()
    {
        cohabitable.SetActive(true);
    }
    public void enableCompetitive()
    {
        competitive.SetActive(true);
    }
    public void disablePlayer()
    {
        player.SetActive(false);
    }
    public void disableCohabitable()
    {
        cohabitable.SetActive(false);
    }
    public void disableCompetitive()
    {
        competitive.SetActive(false);
    }

    public int getSpeciesPopulation(int key)
    {
        return speciesPopulation[key];
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
    public Dictionary<int, Species> getLocalSpecies()
    {
        return localSpecies;
    }
    public int getTemperature()
    {
        return temperature;
    }
    public int getAltitude()
    {
        return altitude;
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
    public void setSpeciesPopulation(int speciesKey, int population)
    {
        speciesPopulation[speciesKey] = population;
    }
    public void setTemperature(int temperature)
    {
        this.temperature = temperature;
    }
    public void setAltitude(int altitude)
    {
        this.altitude = altitude;
    }
    public void setLocalSpecies(Species sp, int pop, Species playerS)
    {
        localSpecies.Add(sp.getSpeciesID(), sp);
        speciesPopulation.Add(sp.getSpeciesID(), pop);
        if (sp.getSpeciesID() == 0)
        {
            enablePlayer();
            speciesRelation[sp.getSpeciesID()] = 0;
        }
        else
        {
            bool isCohabitable = true;
            if (playerS.getCFS() > sp.getCreatureSize())
            {   //  sp can eat player species
                isCohabitable = false;
            }
            else
            {
                for (int i = 0; i < sp.getHFS().Length; i++)
                {   //  check if sp eats any of the same herbivore food sources as player species
                    if (playerS.getHFS()[i] > 0 & sp.getHFS()[i] > 0)
                    {
                        isCohabitable = false;
                    }
                }
            }
            if (isCohabitable)
            {
                enableCohabitable();
                speciesRelation[sp.getSpeciesID()] = 1;
            }
            else
            {
                enableCompetitive();
                speciesRelation[sp.getSpeciesID()] = 2;
            }
        }
    }
    /*
     *  Returns a list of each local species' data as int[8]; 0 == ID, 1 == size, 2 == berries, 3 == nuts, 4 == grass, 5 == leaves, 6 == carnivoreFoodSize, 7 == relation
     */
    public List<int[]> getSpeciesData()
    {
        List<int[]> speciesData = new List<int[]>();
        foreach (KeyValuePair<int, Species> sp in localSpecies)
        {   //  iterates through all species in this tile
            int[] data = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            data[0] = sp.Value.getSpeciesID();
            data[1] = sp.Value.getCreatureSize();
            data[2] = sp.Value.getHFS()[0];
            data[3] = sp.Value.getHFS()[1];
            data[4] = sp.Value.getHFS()[2];
            data[5] = sp.Value.getHFS()[3];
            data[6] = sp.Value.getCFS();
            data[7] = speciesRelation[sp.Value.getSpeciesID()];
        }
        return speciesData;
    }
}
