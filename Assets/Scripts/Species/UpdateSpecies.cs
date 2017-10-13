using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour {

    public GameObject speciesObject;
    private static int DIMENSION = 10;

    // Use this for initialization
    public void GenerateSpecies()
    {
        Spawn();
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
        InvokeRepeating("Mutation", 40f, 40f);
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
        // Web speciesWeb = speciesObject.GetComponent<Web>();
        for (int i = 0; i < 10; i++)
        {
            locX = rnd.Next(0, 100);
            locY = rnd.Next(0, 100);
            Instantiate(speciesObject, new Vector2(DIMENSION * locX, DIMENSION * locY), Quaternion.identity);
            // set parameters
            lctn.Add( new Vector2Int((DIMENSION * locX), DIMENSION * locY) );
            List<int> gns = new List<int>();
            //for (int j = 0; j < 11; j++)
            //{
            //    gns.Add(j);
            //    speciesScript.evolve(true, j);
            //}
            // get herbivore food source (web function)
            // get carnivore food source (web function)
            // get amount calories (web function)
            // get creature size (web function)
            // get max per tile (web function)
            // get litter size (web function)
            // get mate frequency (web function)
            // get mate attachment (web function)
            // get pecking order (web function)
            speciesScript.Init(i.ToString(), lctn, gns, new int[0], 0, 0, 0, 0, 0, 0, 0, 0);
        }
    }

    /*
     *  Parent Species will be copied into new speciesObject (mutatingSpecies) that will evolve once
     */
    private void Mutation(Species parentSpecies, bool isPlayer)
    {
        int newName = int.Parse(parentSpecies.getSpeciesName()) + 100;  //  100 should be replaced by number of existing speciesObjects
        Species mutatingSpecies = new Species(newName.ToString());
        mutatingSpecies.clone(parentSpecies);
        bool addNode;
        int nodeIndex;
        if (!isPlayer)
        {
            addNode = true; //  may change this later on to allow bots to change both ways
            //  nodeIndex set to random node
        }
        else
        {
            //  have player set addNode to true/false
            //  have player choose node
        }
        //  mutatingSpecies.evolve(addNode, nodeIndex);
        //  mutatingSpecies = speciesObject.GetComponent<Species>();
        //  Instantiate(speciesObject, new Vector2(DIMENSION * locX, DIMENSION * locY), Quaternion.identity);
    }

    /*
     *  Have the species in a given tile migrate to adjacent tile
     */
    private void Overpopulation(Species migratingSpecies, bool isPlayer, int tileIndex)
    {
        //  overPopulatedTile = Tile @ tileIndex
        //  Tile[] adjacentTiles;
        //  add all adjacent tiles to adjacentTiles[]
        //  Tile selectedTile;
        //  int movingPopulation = 0;
        /*  if (!isPlayer)
         *  {
         *      selectedTile = randomly choose adjacent tile
         *  }
         *  else
         *  {
         *      selectedTile = player chooses adjacent tile
         *  }
         *      movingPopulation = 0.3 * overPopulatedTile.getPopulation(migratingSpecies);
         *      overPopulatedTile.setPopulation(migratingSpecies) = 0.7 * overPopulatedTile.getPopulation(migratingSpecies);
         *      selectedTile.addSpecies(migratingSpecies);
         *      selectedTile.setPopulation(migratingSpecies) = movingPopulation;
         */
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
        //  int[] validTiles = 'tiles who have species in them'
        //  int[] localSpecies = 'species in valid tile';
        //  for each species in valid tile, species.getMaxPerTile()
        //  for each species in valid tile, species.getLitterSize()
        //  for each species in valid tile, species.getMatingFrequency()
        //  for each species in valid tile, species.getMateAttachment()
        //  call mutation based on mutation chance * number of offspring
        //  if population of local species > maxPerTile, Overpopulation()
    }

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