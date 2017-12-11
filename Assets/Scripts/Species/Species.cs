using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour
{
    private string speciesName;  // name of species - for stretch goal, we will want this to be a string that appears like formal latin names, by D3, number will suffice
    private int speciesID;  //  number of species
    private List<Vector2Int> location; // in which tiles this species exists.  Assuming tiles can be simplified to their numerical value
    private List<int> genes;  // what genes this species has.  Assuming genes can be simplified to their numerical value
    private int[] herbivoreFoodSource; // i == 0 berries, i == 1 nuts, i == 2 grass, i == 3 leaves, value of 0 at any index (default) means speceis cannot eat food type at given index
    private int carnivoreFoodSource; // integer between 1 and 500 that limits what size prey you can eat, -1 (default) means species cannot eat meat
    private int amntCalories; // amount of food to survive
    private int creatureSize; // 1 is tiny, 2 - 100 is small, 101 - 200 is medium, 201 - 300 is large, 301 - 400 is humongous
    private int maxPerTile; // max number of individuals of a species in a given tile, -1 is unlimited
    private int litterSize; // population growth per reproduction
    private int matingFrequency; // reproduction speed
    private int mateAttachment; // mutation chance v offspring survivability
    private int peckingOrder; // determines when the species eats in the eating algorithm
    private Vector2Int temperatureTolerance;    //  x is min temperature, y is max
    private int maxAltitude;    //  how high of a tile this species can enter
    private int canFly;    //  0 == no flight, 1 == flight

    /*
     *  COMPLETE
     *  Constructor
     */
    public Species(string speciesName)
    {
        this.speciesName = speciesName;
    }

    /*
     *  NEEDS ALL MODIFIERS
     *  Initializer
     */
    public void Init(string sN, int sID, List<Vector2Int> lctn, List<int> gns, int[] hFS, int cFS, int aC, int cS, int mPT, int lS, int mF, int mA, int pO, Vector2Int tT, int maxA, int canF)
    {
        speciesName = sN;
        speciesID = sID;
        location = lctn;
        genes = gns;
        herbivoreFoodSource = hFS;
        carnivoreFoodSource = cFS;
        amntCalories = aC;
        creatureSize = cS;
        maxPerTile = mPT;
        litterSize = lS;
        matingFrequency = mF;
        mateAttachment = mA;
        peckingOrder = pO;
        temperatureTolerance = tT;
        maxAltitude = maxA;
        canFly = canF;
        /*
         *  NEEDS ALL MODIFIERS
         */
    }

    /*
     *  NEEDS ALL MODIFIERS
     *  Take boolean to determine if adding/subtracting node in evolutionary web, and takes index of that node to modify species instance accordingly with -1 being random node
     */
    public void evolve(bool addNode, int nodeIndex)
    {
        int op;
        if (addNode)
        {
            op = 1;
        }
        else
        {
            op = -1;
        }
        //  random evolution based on children
        if (nodeIndex == -1)
        {
            List<int> children = new List<int>();
            foreach (int nodeI in genes)
            {   //  iterate through species' genes
                if (GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getChildren(nodeI).Count > 0)
                {   //  check if given node has children
                    for (int i = 0; i < GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getChildren(nodeI).Count; i++)
                    {   //  iterate through given node's children
                        if (!genes.Contains(GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getChildren(nodeI)[i]))
                        {   //  if given node's child is not already in species' genes, add it to children list
                            children.Add(GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getChildren(nodeI)[i]);
                        }
                    }
                }
            }
            //  randomly choose one of the eligible nodes
            if (children.Count > 0)
            {   //  check if there was any eligible node
                var rnd = new System.Random();
                nodeIndex = rnd.Next(0, children.Count);
            }
        }
        if (nodeIndex == -1)
        {   //  species has all possible nodes
            return;
        }
        Node node = GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getNode(nodeIndex);
        // Added a node
        for (int i = 0; i < herbivoreFoodSource.Length; i++)
        {
            herbivoreFoodSource[i] += op * node.getHerbivoreFoodSource()[i];
        }
        carnivoreFoodSource += op * node.getCarnivoreFoodSource();
        amntCalories += op * node.getRequiredCalories();
        creatureSize += op * node.getCreatureSize();
        maxPerTile += op * node.getMaxPerTile();
        litterSize += op * node.getLitterSize();
        matingFrequency += op * node.getMutationChance();
        mateAttachment += op * node.getReproductionRate();
        peckingOrder += op * node.getPeckingOrder();
        /*
         *  NEEDS ALL MODIFIERS
         */
    }

    /*
     *  NEEDS ALL MODIFIERS
     *  Deep copy of species instance of passed species
     */
    public void clone(Species other) // other will be evolved, clone will be parent species
    {
        Init(other.getSpeciesName(), other.getSpeciesID(), other.getLocation(), other.getGenes(), other.getHFS(), other.getCFS(), other.getAmntCalories(), 
            other.getCreatureSize(), other.getMaxPerTile(), other.getLitterSize(), other.getMatingFrequency(), getMateAttachment(), other.getPeckingOrder(),
            other.getTemperatureTolerance(), getMaxAltitude(), other.getCanFly());
    }

    /*
     *  COMPLETE
     *  Get methods for attributes of Species
     */
    public string getSpeciesName()
    {
        return speciesName;
    }
    public int getSpeciesID()
    {
        return speciesID;
    }
    public List<Vector2Int> getLocation()
    {
        return location;
    }
    public List<int> getGenes()
    {
        return genes;
    }
    public int[] getHFS()
    {
        return herbivoreFoodSource;
    }
    public int getCFS()
    {
        return carnivoreFoodSource;
    }
    public int getAmntCalories()
    {
        return amntCalories;
    }
    public int getCreatureSize()
    {
        return creatureSize;
    }
    public int getMaxPerTile()
    {
        return maxPerTile;
    }
    public int getLitterSize()
    {
        return litterSize;
    }
    public int getMatingFrequency()
    {
        return matingFrequency;
    }
    public int getMateAttachment()
    {
        return mateAttachment;
    }
    public int getPeckingOrder()
    {
        return peckingOrder;
    }
    public Vector2Int getTemperatureTolerance()
    {
        return temperatureTolerance;
    }
    public int getMaxAltitude()
    {
        return maxAltitude;
    }
    public int getCanFly()
    {
        return canFly;
    }
    public void addToLocation(Vector2Int additionalLocation)
    {
        location.Add(additionalLocation);
    }
}