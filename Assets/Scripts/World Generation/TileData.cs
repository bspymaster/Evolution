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
    private Dictionary<int, int> localSpecies;

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
        localSpecies = new Dictionary<int, int>();
    }
    
    public int getSpeciesPopulation(int key)
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
    public Dictionary<int, int> getLocalSpecies()
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
    //  Tile will draw its own info about cohabitable/competing species or playerspecies
    public void setLocalSpecies(int speciesID, int pop)
    {
        localSpecies.Add(speciesID, pop);
    }

 //       float dimeny = (float)9.9999999999999;
 //       float dimenx = (Mathf.Sqrt(3) / 2) * dimeny;
 //       float DIMENSION = speciesObject.transform.lossyScale.y;
 //       //  print("Spawn()");
 //       var rnd = new System.Random();
 //       int locX = 0;
 //       int locY = 0;
 //       Species speciesScript = speciesObject.GetComponent<Species>();
 //       List<Vector2Int> lctn = new List<Vector2Int>();
 //       List<int> gns = new List<int>();
 //       //Web speciesWeb = speciesObject.GetComponent<Web>();
 //       for (int i = 1; i < 11; i++)
 //       {
 //           lctn = new List<Vector2Int>();
 //           gns = new List<int>();
 //           locX = rnd.Next(0, 100);
 //           locY = rnd.Next(0, 100);
 //           lctn.Add(new Vector2Int(locX, locY));
 //           Vector2 holder = GameObject.Find("Generator").GetComponent<generate>().FindHexagonLocation(locX, locY);
 //           holder.x = holder.x * (dimenx - (float)2.5);
 //           holder.y = holder.y * (dimeny - (float)2);
 //           GameObject newSpeciesObject = Instantiate(speciesObject, holder, Quaternion.identity);
 //           speciesScript = newSpeciesObject.GetComponent<Species>();
 //           speciesScript.Init(i.ToString(), i, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
 //           // set parameters
 //           for (int j = 0; j < 11; j++)
 //           {
 //               gns.Add(j);
 //               speciesScript.evolve(true, j);
 //           }
 //           speciesDict.Add(newSpeciesObject, lctn);
 //           Dictionary<int, int> localSpecies = new Dictionary<int, int>();
 //           localSpecies.Add(i, 10);
 //           GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().setLocalSpecies(localSpecies);
 //       }
 //       locX = rnd.Next(0, 100);
 //       locY = rnd.Next(0, 100);
 //       lctn.Add(new Vector2Int(locX, locY));
 //       Vector2 hold = GameObject.Find("Generator").GetComponent<generate>().FindHexagonLocation(locX, locY);
 //       hold.x = hold.x * (dimenx - (float)2.5);
 //       hold.y = hold.y * (dimeny - (float)2);

 //       GameObject newPlayerSpeciesObject = Instantiate(playerSpeciesObject, hold, Quaternion.identity);
 //       speciesScript = newPlayerSpeciesObject.GetComponent<Species>();
 //       speciesScript.Init("0", 0, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
 //       speciesDict.Add(newPlayerSpeciesObject, lctn);
 //       Dictionary<int, int> localPlayerSpecies = new Dictionary<int, int>();
 //       localPlayerSpecies.Add(0, 10);
 //       GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().setLocalSpecies(localPlayerSpecies);

    public void setSpeciesPopulation(int key, int population)
    {
        localSpecies[key] = population;
    }
    public void setTemperature(int temperature)
    {
        this.temperature = temperature;
    }
    public void setAltitude(int altitude)
    {
        this.altitude = altitude;
    }
}
