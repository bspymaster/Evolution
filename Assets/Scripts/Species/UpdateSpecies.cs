using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour {

    public GameObject speciesObject;
    public GameObject playerSpeciesObject;
    private static int DIMENSION = 10;
    private int mapSize;
    private List<GameObject> speciesArray;

    // Use this for initialization
    public void GenerateSpecies()
    {
        speciesArray = new List<GameObject>();
        mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize();
        Spawn();
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
        InvokeRepeating("Mutate", 40f, 40f);
    }

    /*
     * Spawn() generates 10 game objects as species on game creation
     */
    private void Spawn()
    {
        var rnd = new System.Random();
        int locX = 0;
        int locY = 0;
        Species speciesScript = speciesObject.GetComponent<Species>();
        List<Vector2Int> lctn = new List<Vector2Int>();
        List<int> gns = new List<int>();
        //Web speciesWeb = speciesObject.GetComponent<Web>();
        for (int i = 1; i < 11; i++)
        {
            lctn = new List<Vector2Int>();
            gns = new List<int>();
            locX = rnd.Next(0, 100);
            locY = rnd.Next(0, 100);
            lctn.Add(new Vector2Int(locX, locY));
            GameObject newSpeciesObject = Instantiate(speciesObject, new Vector2(DIMENSION * locX, DIMENSION * locY), Quaternion.identity);
            speciesScript = newSpeciesObject.GetComponent<Species>();
            speciesScript.Init(i.ToString(), i, lctn, gns, new int[4], 0, 0, 0, 0, 0, 0, 0, 0);
            // set parameters
            for (int j = 0; j < 11; j++)
            {
                gns.Add(j);
                speciesScript.evolve(true, j);
            }
            speciesArray.Add(newSpeciesObject);
            Dictionary<string, int> localSpecies = new Dictionary<string, int>();
            localSpecies.Add(i.ToString(), 10);
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().setLocalSpecies(localSpecies);
        }
        locX = rnd.Next(0, 100);
        locY = rnd.Next(0, 100);
        lctn.Add(new Vector2Int(locX, locY));
        GameObject newPlayerSpeciesObject = Instantiate(playerSpeciesObject, new Vector2(DIMENSION * locX, DIMENSION * locY), Quaternion.identity);
        speciesScript = newPlayerSpeciesObject.GetComponent<Species>();
        speciesScript.Init("0", 0, lctn, gns, new int[4], 0, 0, 0, 0, 0, 0, 0, 0);
        speciesArray.Add(newPlayerSpeciesObject);
        Dictionary<string, int> localPlayerSpecies = new Dictionary<string, int>();
        localPlayerSpecies.Add("0", 10);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().setLocalSpecies(localPlayerSpecies);
    }

    /*
     *  Parent Species will be copied into new speciesObject (mutatingSpecies) that will evolve once
     */
    private void Mutate(Species parentSpecies, bool isPlayer)
    {
        int newName = int.Parse(parentSpecies.getSpeciesName()) + 100;  //  100 should be replaced by number of existing speciesObjects
        Species mutatingSpecies = new Species(newName.ToString());
        mutatingSpecies.clone(parentSpecies);
        bool addNode = false;
        int nodeIndex = 0;
        if (!isPlayer)
        {
            addNode = true; //  may change this later on to allow bots to change both ways
            int geneIndex = mutatingSpecies.getGenes().Count - 1;
            int newGene = mutatingSpecies.getGenes()[geneIndex] + 1;
            if (newGene > 30)
            {
                return;
            }
        }
        else
        {
            Global.mutationPoints += 1;
            Global.playerSpeciesGeneList = parentSpecies.getGenes();
            return;
        }
        mutatingSpecies.evolve(addNode, nodeIndex);
        Instantiate(speciesObject, new Vector2(-1, -1), Quaternion.identity);
    }

    /*
     *  Have the species in a given tile migrate to adjacent tile
     */
    private void Overpopulation(Species migratingSpecies, bool isPlayer, Vector2Int tileLocation)
    {
        var rnd = new System.Random();
        int receivingTile = rnd.Next(0, 3); //  0 is left tile, 1 is bottom tile, 2 is right tile, 3 is above tile
        switch (receivingTile)
        {
            case 0:
                {
                    Vector2Int target = new Vector2Int(tileLocation.x - 1, tileLocation.y);
                    if (target.x < 0)
                    {
                        target.x += 2;
                    }
                    Migrate(target, isPlayer, migratingSpecies.getSpeciesName(), tileLocation);
                    break;
                }
            case 1:
                {
                    Vector2Int target = new Vector2Int(tileLocation.x, tileLocation.y + 1);
                    if (target.y > mapSize)
                    {
                        target.y -= 2;
                    }
                    Migrate(target, isPlayer, migratingSpecies.getSpeciesName(), tileLocation);
                    break;
                }
            case 2:
                {
                    Vector2Int target = new Vector2Int(tileLocation.x + 1, tileLocation.y);
                    if (target.x > mapSize)
                    {
                        target.x -= 2;
                    }
                    Migrate(target, isPlayer, migratingSpecies.getSpeciesName(), tileLocation);
                    break;
                }
            case 3:
                {
                    Vector2Int target = new Vector2Int(tileLocation.x, tileLocation.y - 1);
                    if (target.y < 0)
                    {
                        target.y += 2;
                    }
                    Migrate(target, isPlayer, migratingSpecies.getSpeciesName(), tileLocation);
                    break;
                }
        }
    }

    /*
     *  Migrates the population for OverPopulation()
     */
    private void Migrate(Vector2Int recievingTile, bool isPlayer, string speciesKey, Vector2Int givingTile)
    {
        int x = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>().getSpeciesPopulation(speciesKey);
        int movingPopulation = (int)(0.3 * x);
        int stayingPopulation = (int)(0.7 * x);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(recievingTile).GetComponent<TileData>().setSpeciesPopulation(speciesKey, movingPopulation);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>().setSpeciesPopulation(speciesKey, stayingPopulation);
    }

    /*
     *  Have the species in each tile containing species eat
     */
    private void Interact()
    {
        HerbivoreMove();
        CarnivoreMove();
    }

    /*
     *  Have the species in each tile reproduce
     */
    private void Reproduce()
    {
        if (Global.change)
        {
            for (int i = 0; i < Global.newGenes.Count; i++)
            {
                playerSpeciesObject.GetComponent<Species>().evolve(true, Global.newGenes[i]);
                print(playerSpeciesObject.GetComponent<Species>().getGenes());
            }
        }
        List<Vector2Int> occupiedTile = new List<Vector2Int>();
        List<Vector2Int> location = new List<Vector2Int>();
        //  get each occupied tile
        for (int i = 0; i < speciesArray.Count; i++)
        {   //  iterate through species
            location = speciesArray[i].GetComponent<Species>().getLocation();
            for (int j = 0; j < location.Count; j++)
            {   //  iterate through species[i]'s location
                if (!occupiedTile.Contains(location[j]))
                {
                    occupiedTile.Add(location[j]);
                }
            }
            //  get mutation variables from species to figure out chance - right now just random
            System.Random rnd = new System.Random();
            int mutVarTemp = 100;
            if (rnd.Next(1, 101) <= mutVarTemp)
            {
                Mutate(speciesArray[i].GetComponent<Species>(), (speciesArray[i].GetComponent<Species>().getSpeciesID() == 0) );
            }
        }
        int population = 0;
        //  add population to each of the species in each occupiedTile
        for (int i = 0; i < occupiedTile.Count; i++)
        {   //  go through each tile and get the dictionary of species inside that tile
            Dictionary<string, int> localSpecies = new Dictionary<string, int>();
            localSpecies = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(occupiedTile[i]).GetComponent<TileData>().getLocalSpecies();
            foreach (KeyValuePair<string, int> species in localSpecies)
            {
                population = species.Value;
                population += population * speciesArray[i].GetComponent<Species>().getLitterSize();
                if (population > speciesArray[i].GetComponent<Species>().getMaxPerTile())
                {
                    //  set to false bc no implementation of player movement
                    Overpopulation(speciesArray[i].GetComponent<Species>(), false, occupiedTile[i]);
                }
                else
                {
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(occupiedTile[i]).GetComponent<TileData>().setSpeciesPopulation(species.Key, population);
                }
                print(species.Key);
            }
        }
    }


    //    private void Reproduce()
    //    {
    //        if (Global.change)
    //        {
    //            for (int i = 0; i < Global.newGenes.Count; i++)
    //            {
    //                playerSpeciesObject.GetComponent<Species>().evolve(true, Global.newGenes[i]);
    //                print(playerSpeciesObject.GetComponent<Species>().getGenes());
    //            }
    //        }
    //        List<Vector2Int> location = new List<Vector2Int>();
    //        int population = 0;
    //        for (int i = 0; i < speciesArray.Count; i++)    //  Iterates through all alive species, and increases their population in each tile
    //        {
    //            location = speciesArray[i].GetComponent<Species>().getLocation();
    //            for (int j = 0; j < location.Count; j++)
    //            {

    //                // start testing to get population working

    //                Dictionary<string, int> locationDict = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(location[j]).GetComponent<TileData>().getLocalSpecies();

    //                List<string> keyList = new List<string>(locationDict.Keys);
    //                for (int q = 0; q < keyList.Count; q++)
    //                {
    //                    print(keyList[q]);
    //                }

    //                print("Species: " + speciesArray[i].GetComponent<Species>().getSpeciesName());
    //                print("x: " + location[j].x + ", y: " + location[j].y);
    //                print("population: " + locationDict[i.ToString()]);

    //                //  end

    //                population = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(location[j]).GetComponent<TileData>().getLocalSpecies()[i.ToString()];
    //                population += population * speciesArray[i].GetComponent<Species>().getLitterSize();
    //                if (population > speciesArray[i].GetComponent<Species>().getMaxPerTile())
    //                {

    //                    //  set to false bc no implementation of player movement

    //                    Overpopulation(speciesArray[i].GetComponent<Species>(), false, location[j]);
    //                }
    //                else
    //                {
    //                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(location[j]).GetComponent<TileData>().setSpeciesPopulation(i.ToString(), population);
    //                }
    //            }
    //            //  get mutation variables from species to figure out chance - right now just random
    //            System.Random rnd = new System.Random();
    //            int mutVarTemp = 100;
    //            if (rnd.Next(1, 101) <= mutVarTemp)
    //            {
    //                Mutate(speciesArray[i].GetComponent<Species>(), (speciesArray[i].GetComponent<Species>().getSpeciesName().Equals("0")));
    //            }
    //        }


    //}


    /*
     *  Have the herbivore species in each tile containing species eat tile resources
     */
    private void HerbivoreMove()
    {
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
     *  Have the carnivore species in each tile containing species eat other species and tiny species (tile resource)
     */
    private void CarnivoreMove()
    {
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