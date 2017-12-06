using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour {

    public GameObject speciesObject;
    public GameObject playerSpeciesObject;
    //private int DIMENSION = speciesObject.transform.lossyScale.y;
    private int mapSize;
    private List<GameObject> speciesArray;

    // Use this for initialization
    public void GenerateSpecies()
    {
        //  print("GenerateSpecies()");
        speciesArray = new List<GameObject>();
        mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize();
        Spawn();
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
    }

    /*
     * Spawn() generates 10 game objects as species on game creation
     */
    private void Spawn()
    {
         float DIMENSION = speciesObject.transform.lossyScale.y;
    //  print("Spawn()");
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
            locX = rnd.Next(0, 99);
            locY = rnd.Next(0, 99);
            lctn.Add(new Vector2Int(locX, locY));
           Vector2 holder= GameObject.Find("Generator").GetComponent<generate>().FindHexagonLocation(locX, locY);
            holder.x *=(DIMENSION /*- (float)2.5*/);
            holder.y *=(DIMENSION/* - (float)2*/);
        GameObject newSpeciesObject = Instantiate(speciesObject, holder, Quaternion.identity);
            speciesScript = newSpeciesObject.GetComponent<Species>();
            speciesScript.Init(i.ToString(), i, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
            // set parameters
            for (int j = 0; j < 11; j++)
            {
                gns.Add(j);
                speciesScript.evolve(true, j);
            }
            speciesArray.Add(newSpeciesObject);
            Dictionary<int, int> localSpecies = new Dictionary<int, int>();
            localSpecies.Add(i, 10);
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().setLocalSpecies(localSpecies);
        }
        locX = rnd.Next(0, 99);
        locY = rnd.Next(0, 99);
        lctn.Add(new Vector2Int(locX, locY));
       float dimeny = GameObject.Find("BaseWorldTile").GetComponent<generate>().transform.lossyScale.y;
        float dimenx = (Mathf.Sqrt(3) / 2) * dimeny;
        Vector2 hold = GameObject.Find("Generator").GetComponent<generate>().FindHexagonLocation(locX, locY);
        hold.x = hold.x * (dimenx);// - (float)2.5);
        hold.y = hold.y * (dimeny);//- (float)2);

        GameObject newPlayerSpeciesObject = Instantiate(playerSpeciesObject,hold, Quaternion.identity);
        speciesScript = newPlayerSpeciesObject.GetComponent<Species>();
        speciesScript.Init("0", 0, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
        speciesArray.Add(newPlayerSpeciesObject);
        Dictionary<int, int> localPlayerSpecies = new Dictionary<int, int>();
        localPlayerSpecies.Add(0, 10);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().setLocalSpecies(localPlayerSpecies);
    }

    /*
     *  Have the species in each tile containing species eat
     */
    private void Interact()
    {
        //  print("Interact()");
        HerbivoreMove();
        CarnivoreMove();
    }

    /*
     *  Have the species in each tile reproduce
     */
    private void Reproduce()
    {
        //  print("Reproduce()");
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
        }
        int population = 0;
        //  add population to each of the species in each occupiedTile
        for (int i = 0; i < occupiedTile.Count; i++)
        {   //  go through each tile and get the dictionary of species inside that tile
            Dictionary<int, int> localSpecies = new Dictionary<int, int>(GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(occupiedTile[i]).GetComponent<TileData>().getLocalSpecies());
            foreach (KeyValuePair<int, int> species in localSpecies)
            {
                population = species.Value;
                population += population * speciesArray[species.Key].GetComponent<Species>().getLitterSize();
                if (population > speciesArray[species.Key].GetComponent<Species>().getMaxPerTile())
                {
                    //  set to false bc no implementation of player movement
                    Overpopulate(species.Key, (speciesArray[species.Key].GetComponent<Species>().getSpeciesID() == 0), occupiedTile[i]);
                }
                else
                {
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(occupiedTile[i]).GetComponent<TileData>().setSpeciesPopulation(species.Key, population);
                }
            }
        }
        //  mutate chance
        for (int i = 0; i < speciesArray.Count; i++)
        {
            //  get mutation variables from species to figure out chance - right now just random
            System.Random rnd = new System.Random();
            int mutVarTemp = 100;
            if (rnd.Next(1, 101) <= mutVarTemp)
            {
                Mutate(speciesArray[i].GetComponent<Species>(), (speciesArray[i].GetComponent<Species>().getSpeciesID() == 0));
            }

        }
    }

    /*
     *  Parent Species will be copied into new speciesObject (mutatingSpecies) that will evolve once
     */
    private void Mutate(Species parentSpecies, bool isPlayer)
    {
        //  print("Mutate()");
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
    private void Overpopulate(int migratingSpeciesKey, bool isPlayer, Vector2Int tileLocation)
    {
        //  print("Overpopulate()");
        if (!isPlayer)
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
                        Migrate(target, migratingSpeciesKey, tileLocation);
                        break;
                    }
                case 1:
                    {
                        Vector2Int target = new Vector2Int(tileLocation.x, tileLocation.y + 1);
                        if (target.y > mapSize)
                        {
                            target.y -= 2;
                        }
                        Migrate(target, migratingSpeciesKey, tileLocation);
                        break;
                    }
                case 2:
                    {
                        Vector2Int target = new Vector2Int(tileLocation.x + 1, tileLocation.y);
                        if (target.x > mapSize)
                        {
                            target.x -= 2;
                        }
                        Migrate(target, migratingSpeciesKey, tileLocation);
                        break;
                    }
                case 3:
                    {
                        Vector2Int target = new Vector2Int(tileLocation.x, tileLocation.y - 1);
                        if (target.y < 0)
                        {
                            target.y += 2;
                        }
                        Migrate(target, migratingSpeciesKey, tileLocation);
                        break;
                    }
            }
        }
        else
        {
            //  need player choice to migrate
        }
    }

    /*
     *  Migrates the population for OverPopulation()
     */
    private void Migrate(Vector2Int recievingTile, int speciesKey, Vector2Int givingTile)
    {
        //  print("Migrate()");
        int x = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>().getSpeciesPopulation(speciesKey);
        int movingPopulation = (int)(0.3 * x);
        int stayingPopulation = (int)(0.7 * x);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(recievingTile).GetComponent<TileData>().setSpeciesPopulation(speciesKey, movingPopulation);
        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>().setSpeciesPopulation(speciesKey, stayingPopulation);
    }
    
    /*
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