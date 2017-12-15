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
    private int requiredCalories; // amount of food to survive
    private int creatureSize; // 1 is tiny, 2 - 100 is small, 101 - 200 is medium, 201 - 300 is large, 301 - 400 is humongous
    private int litterSize; // population growth per reproduction
    private int reproductionRate; // reproduction speed
    private int mutationChance; // mutation chance v offspring survivability
    private int carnivorous;    //  0 == can't eat meat
    private int offspringSize;          // size of the offspring (calorie worth and which size class it belongs to)
    private int altitude;    //  how high of a tile this species can enter
    private int canFly;    //  0 == no flight, 1 == flight
    private int dexterity;  //  defensive trait
    private int maxPerTile; // max number of individuals of a species in a given tile, -1 is unlimited
    private int peckingOrder; // determines when the species eats in the eating algorithm
    private int offspringSurvivalChance;    // the chance that any given offspring will survive to adulthood
    private int canSwim;    // 0 == can't swim
    private Vector2Int temperatureTolerance;    //  x is min temperature, y is max

    /*
     *  COMPLETE
     *  Constructor
     */
    public Species(string speciesName)
    {
        this.speciesName = speciesName;
    }

    /*
     *  COMPLETE
     *  Initializer
     */
    public void Init(string speciesName, int speciesID, List<Vector2Int> location, List<int> genes, int[] herbivoreFoodSource, int carnivoreFoodSource,
         int requiredCalories, int creatureSize, int litterSize, int reproductionRate, int mutationChance, int carnivorous, int offspringSize, int altitude,
         int canFly, int dexterity, int maxPerTile, int peckingOrder, int offspringSurvivalChance, int canSwim, Vector2Int temperatureTolerance)
    {
        this.speciesName = speciesName;
        this.speciesID = speciesID;
        this.location = location;
        this.genes = genes;
        this.herbivoreFoodSource = herbivoreFoodSource;
        this.carnivoreFoodSource = carnivoreFoodSource;
        this.requiredCalories = requiredCalories;
        this.creatureSize = creatureSize;
        this.litterSize = litterSize;
        this.reproductionRate = reproductionRate;
        this.mutationChance = mutationChance;
        this.carnivorous = carnivorous;
        this.offspringSize = offspringSize;
        this.altitude = altitude;
        this.canFly = canFly;
        this.dexterity = dexterity;
        this.maxPerTile = maxPerTile;
        this.peckingOrder = peckingOrder;
        this.offspringSurvivalChance = offspringSurvivalChance;
        this.canSwim = canSwim;
        this.temperatureTolerance = temperatureTolerance;
    }

    /*
     *  COMPLETE
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
        this.genes.Add(nodeIndex);
        Node node = GameObject.Find("Web Builder").GetComponent<buildWeb>().getWeb().getNode(nodeIndex);
        // Added a node
        for (int i = 0; i < herbivoreFoodSource.Length; i++)
        {
            herbivoreFoodSource[i] += op * node.getHerbivoreFoodSource()[i];
        }
        carnivoreFoodSource += op * node.getCarnivoreFoodSource();
        requiredCalories += op * node.getRequiredCalories();
        creatureSize += op * node.getCreatureSize();
        litterSize += op * node.getLitterSize();
        reproductionRate += op * node.getReproductionRate();
        mutationChance += op * node.getMutationChance();
        carnivorous += op * node.getCarnivorous();
        offspringSize += op * node.getOffspringSize();
        altitude += op * node.getAltitude();
        canFly += op * node.getCanFly();
        dexterity += op * node.getDexterity();
        maxPerTile += op * node.getMaxPerTile();
        peckingOrder += op * node.getPeckingOrder();
        offspringSurvivalChance += op * node.getOffspringSurvivalChance();
        canSwim += op * node.getCanSwim();
        temperatureTolerance.x += op * node.getTemperatureTolerance().x;
        temperatureTolerance.y += op * node.getTemperatureTolerance().y;
    }

    /*
     *  COMPLETE
     *  Deep copy of species instance of passed species
     */
    public void clone(Species other, int id) // other will be evolved, clone will be parent species
    {
        Init("Species: " + id, id, other.getLocation(), other.getGenes(), other.getHFS(), other.getCFS(), other.getRequiredCalories(),
         other.getCreatureSize(), other.getLitterSize(), other.getReproductionRate(), other.getMutationChance(), other.getCarnivorous(), other.getOffspringSize(), other.getAltitude(),
         other.getCanFly(), other.getDexterity(), other.getMaxPerTile(), other.getPeckingOrder(), other.getOffspringSurvivalChance(), other.getCanSwim(), other.getTemperatureTolerance());
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
    public int getRequiredCalories()
    {
        return requiredCalories;
    }
    public int getCreatureSize()
    {
        return creatureSize;
    }
    public int getLitterSize()
    {
        return litterSize;
    }
    public int getReproductionRate()
    {
        return reproductionRate;
    }
    public int getMutationChance()
    {
        return mutationChance;
    }
    public int getCarnivorous()
    {
        return carnivorous;
    }
    public int getOffspringSize()
    {
        return offspringSize;
    }
    public int getAltitude()
    {
        return altitude;
    }
    public int getCanFly()
    {
        return canFly;
    }
    public int getDexterity()
    {
        return dexterity;
    }
    public int getMaxPerTile()
    {
        return maxPerTile;
    }
    public int getPeckingOrder()
    {
        return peckingOrder;
    }
    public int getOffspringSurvivalChance()
    {
        return offspringSurvivalChance;
    }
    public int getCanSwim()
    {
        return canSwim;
    }
    public Vector2Int getTemperatureTolerance()
    {
        return temperatureTolerance;
    }
    public void addToLocation(Vector2Int additionalLocation)
    {
        location.Add(additionalLocation);
    }
}