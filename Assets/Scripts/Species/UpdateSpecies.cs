using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour {

    public GameObject speciesObject;
    public GameObject playerSpeciesObject;
    //private int DIMENSION = speciesObject.transform.lossyScale.y;
    private int mapSize;
    private Dictionary<GameObject, List<Vector2Int>> speciesDict;   //  Species Object as key, location as value

    /*
    *  COMPLETE
    *  Use this for initialization
    */
    public void GenerateSpecies()
    {
        //  print("GenerateSpecies()");
        speciesDict = new Dictionary<GameObject, List<Vector2Int>>();
        mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize();
        Spawn();
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
    }

    /*
     *  REVAMP NEEDED - TRANSFER DRAW-TO-CANVAS TO TILEDATA.SETLOCALSPECIES()
     *  Spawn() generates 10 game objects as species on game creation
     */
    private void Spawn()
    {
        //  print("Spawn()");
        var rnd = new System.Random();
        int locX = 0;
        int locY = 0;
        Species speciesScript = speciesObject.GetComponent<Species>();
        List<Vector2Int> lctn = new List<Vector2Int>();
        List<int> gns = new List<int>();
        for (int i = 1; i < 11; i++)
        {
            lctn = new List<Vector2Int>();
            gns = new List<int>();
            locX = rnd.Next(0, 100);
            locY = rnd.Next(0, 100);
            lctn.Add(new Vector2Int(locX, locY));
            GameObject newSpeciesObject = Instantiate(speciesObject, new Vector2(-10, -10), Quaternion.identity);
            speciesScript = newSpeciesObject.GetComponent<Species>();
            speciesScript.Init(i.ToString(), i, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
            speciesDict.Add(newSpeciesObject, lctn);
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(lctn[0]).GetComponent<TileData>().setLocalSpecies(i, 10);
            /*
             *  EVOLVE SPECIES RANDOMLY
             */
        }
        locX = rnd.Next(0, 100);
        locY = rnd.Next(0, 100);
        lctn.Add(new Vector2Int(locX, locY));
        GameObject newPlayerSpeciesObject = Instantiate(playerSpeciesObject, new Vector2(-10, -10), Quaternion.identity);
        speciesScript = newPlayerSpeciesObject.GetComponent<Species>();
        speciesScript.Init("0", 0, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
        speciesDict.Add(newPlayerSpeciesObject, lctn);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(lctn[0]).GetComponent<TileData>().setLocalSpecies(0, 10);
    }

    /*
     *  COMPLETE
     *  Have the species in each tile containing species eat
     */
    private void Interact()
    {
        //  print("Interact()");
        HerbivoreMove();
        CarnivoreMove();
    }

    /*
     *  NEEDS POPULATION MODIFIERS
     *  NEEDS MUTATION MODIFIERS
     *  Have the species in each tile reproduce
     */
    private void Reproduce()
    {
        //  print("Reproduce()");
        if (Global.change)
        {
            int index = Global.newGenes.Count;
            for (int i = 0; i < index; i++)
            {
                playerSpeciesObject.GetComponent<Species>().evolve(true, Global.newGenes[i]);
                Global.newGenes.RemoveAt(i);
            }
            Global.change = false;
        }
        int population = 0;
        foreach (KeyValuePair<GameObject, List<Vector2Int>> species in speciesDict)
        {
            for (int i = 0; i < species.Value.Count; i++)
            {
                population = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(species.Value[i]).GetComponent<TileData>().getSpeciesPopulation(species.Key.GetComponent<Species>().getSpeciesID());
                population += population * species.Key.GetComponent<Species>().getLitterSize();
                /*
                 *  NEEDS POPULATION MODIFIERS
                 */
                if (population > species.Key.GetComponent<Species>().getMaxPerTile())
                {
                    Overpopulate(species.Key, species.Value[i]);
                }
                else
                {
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(species.Value[i]).GetComponent<TileData>().setSpeciesPopulation(species.Key.GetComponent<Species>().getSpeciesID(), population);
                }
            }
            /*
             *  NEEDS MUTATION MODIFIERS
             */
            System.Random rnd = new System.Random();
            int mutVarTemp = 0;
            if (rnd.Next(1, 101) <= mutVarTemp)
            {
                Mutate(species.Key.GetComponent<Species>(), (species.Key.GetComponent<Species>().getSpeciesID() == 0));
            }
        }
    }

    /*
     *  ACHIEVEMENTS NEEDED
     *  Parent Species will be copied into new speciesObject (mutatingSpecies) that will evolve once
     */
    private void Mutate(Species parentSpecies, bool isPlayer)
    {
        //  print("Mutate()");
        int newID = speciesDict.Count + 1;
        Species mutatingSpecies = new Species(newID.ToString());
        mutatingSpecies.clone(parentSpecies);
        int nodeIndex = 0;
        if (!isPlayer)
        {
            int geneIndex = mutatingSpecies.getGenes().Count - 1;
            int newGene = mutatingSpecies.getGenes()[geneIndex] + 1;
            if (newGene <= GameObject.Find("Web Builder").GetComponent<buildWeb>().getNumNodes())
            {
                //  addNode for evolve may eventually allow false
                mutatingSpecies.evolve(true, nodeIndex);
                Instantiate(speciesObject, new Vector2(-1, -1), Quaternion.identity);
            }
        }
        else
        {
            Global.mutationPoints += 1;
            Global.playerSpeciesGeneList = parentSpecies.getGenes();
        }
    }

    /*
     *  COMPLETE
     *  Have the species in a given tile migrate to adjacent tile
     */
    private void Overpopulate(GameObject migratingSpecies, Vector2Int tileLocation)
    {
        //  print("Overpopulate()");
        var rnd = new System.Random();
        int receivingTile = rnd.Next(0, 6); //  0 is left tile, 1 is right tile, 2 is top-left tile, 3 is top-right tile, 4 is bottom-left tile, 5 is bottom-right tile
        if (receivingTile == 0)
        {
            Vector2Int target = new Vector2Int(tileLocation.x - 1, tileLocation.y);
            if (target.x < 0)
            {
                target.x += 2;
            }
            Migrate(target, migratingSpecies, tileLocation);
        }
        else if (receivingTile == 1)
        {
            Vector2Int target = new Vector2Int(tileLocation.x + 1, tileLocation.y);
            if (target.x > mapSize)
            {
                target.x -= 2;
            }
            Migrate(target, migratingSpecies, tileLocation);
        }
        else if (receivingTile == 2)
        {
            Vector2Int target = new Vector2Int(tileLocation.x, tileLocation.y + 1);
            if (target.y > mapSize)
            {
                target.y -= 2;
            }
            Migrate(target, migratingSpecies, tileLocation);
        }
        else if (receivingTile == 3)
        {
            Vector2Int target = new Vector2Int(tileLocation.x + 1, tileLocation.y + 1);
            if (target.y > mapSize)
            {
                target.y -= 2;
            }
            if (target.x > mapSize)
            {
                target.x -= 2;
            }
            Migrate(target, migratingSpecies, tileLocation);
        }
        else if (receivingTile == 4)
        {
            Vector2Int target = new Vector2Int(tileLocation.x, tileLocation.y - 1);
            if (target.y < 0)
            {
                target.y += 2;
            }
            Migrate(target, migratingSpecies, tileLocation);
        }
        else {
            Vector2Int target = new Vector2Int(tileLocation.x + 1, tileLocation.y - 1);
            if (target.y < 0)
            {
                target.y += 2;
            }
            if (target.x > mapSize)
            {
                target.x -= 2;
            }
            Migrate(target, migratingSpecies, tileLocation);
        }
    }

    /*
     *  ACHIEVEMENTS NEEDED
     *  Migrates the population for OverPopulation()
     */
    private void Migrate(Vector2Int recievingTile, GameObject migratingSpecies, Vector2Int givingTile)
    {
        //  print("Migrate()");
        int speciesKey = migratingSpecies.GetComponent<Species>().getSpeciesID();
        int pop = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>().getSpeciesPopulation(speciesKey);
        int movingPopulation = (int)(0.3 * pop);
        int stayingPopulation = (int)(0.7 * pop);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(recievingTile).GetComponent<TileData>().setSpeciesPopulation(speciesKey, movingPopulation);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>().setSpeciesPopulation(speciesKey, stayingPopulation);
        migratingSpecies.GetComponent<Species>().addToLocation(recievingTile);
    }

    /*
     *  IMPLEMENTATION NEEDED
     *  Have the herbivore species in each tile containing species eat tile resources
     */
    private void HerbivoreMove()
    {
        //  print("HerbivoreMove()");
        //  int[] validTiles = 'tiles who have herbivores in them'
        //  for each valid tile, get herbivore food sources (tile gets)
        //  for each species in valid tile, species.getHFS()
        //  int[] localSpecies = 'species in valid tile';
        /*  for (int i = 0; i < localSpecies.Length; i++) {
         *      for (int j = 0; j < 4; j++) {
         *          berriesInTile - ( localSpecies[i].getHFS(j) * localSpecies[i].getAmntCalories() );
         *          nutsInTile - ( localSpecies[i].getHFS(j) * localSpecies[i].getAmntCalories() );
         *          grassInTile - ( localSpecies[i].getHFS(j) * localSpecies[i].getAmntCalories() );
         *          leavesInTile - ( localSpecies[i].getHFS(j) * localSpecies[i].getAmntCalories() );
         *      }
         *  }
         */
    }

    /*
     *  IMPLEMENTATION NEEDED
     *  Have the carnivore species in each tile containing species eat other species and tiny species (tile resource)
     */
    private void CarnivoreMove()
    {
        //  print("CarnivoreMove()");
        //  int[] validTiles = 'tiles who have carnivores in them'
        //  for each valid tile, get carnivore food sources (tile gets)
        //  for each species in valid tile, species.getCFS()
        //  int[] localSpecies = 'species in valid tile';
        /*  for (int i = 0; i < localSpecies.Length; i++) {
         *      for (int j = 0; j < 5; j++) {
         *          ambientInTile - ( localSpecies[i].getCFS(j) * localSpecies[i].getAmntCalories() );
         *          smallInTile - ( localSpecies[i].getCFS(j) * localSpecies[i].getAmntCalories() );
         *          mediumInTile - ( localSpecies[i].getCFS(j) * localSpecies[i].getAmntCalories() );
         *          largeInTile - ( localSpecies[i].getCFS(j) * localSpecies[i].getAmntCalories() );
         *          humongousInTile - ( localSpecies[i].getCFS(j) * localSpecies[i].getAmntCalories() );
         *      }
         *  }
         */
    }
}