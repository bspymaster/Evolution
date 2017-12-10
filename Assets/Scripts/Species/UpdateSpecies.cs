using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour {
    
    private int mapSize;
    private Dictionary<int, Species> speciesDict;   //  speciesID as key, Species as value

    /*
    *  COMPLETE
    *  Use this for initialization
    */
    public void GenerateSpecies()
    {
        //print("GenerateSpecies()");
        speciesDict = new Dictionary<int, Species>();
        mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize() - 1;
        Spawn();
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
    }

    /*
     *  COMPLETE
     *  Use this to get the player species
     */
    public Species getPlayerSpecies()
    {
        //print("getPlayerSpecies()");
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {
            if (sp.Key == 0)
            {
                return sp.Value;
            }
        }
        return new Species("SHOULD NOT APPEAR: -1");
    }

    /*
     *  NEEDS DIFFERENT INIT VALUES
     *  Spawn() generates n game objects as species on game creation
     */
    private void Spawn()
    {
        //print("Spawn()");
        var rnd = new System.Random();
        int locX = 0;
        int locY = 0;
        for (int i = 1; i < 11; i++)
        {
            Species speciesScript = new Species("SHOULD NOT APPEAR: -2");
            List<Vector2Int> lctn = new List<Vector2Int>();
            List<int> gns = new List<int>();
            locX = rnd.Next(1, 99);
            locY = rnd.Next(1, 99);
            lctn.Add(new Vector2Int(locX, locY));
            speciesScript.Init("Species: " + i.ToString(), i, lctn, gns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
            speciesDict.Add(i, speciesScript);
            //  addNode is now locked to true, we may want to change this later, time permitted
            speciesScript.evolve(true, 0);
            speciesScript.evolve(true, 95);
            speciesScript.evolve(true, 100);
            speciesScript.evolve(true, 106);
            //  0-6 single-jaw (14%), 7-10 teeth w/venom (8%), 11-14 intense stomach acids (8%), 15-29 eats grass (30%), 30-39 eats nuts (20%), 40-49 eats berries (20%)
            locX = rnd.Next(0, 50);
            locY = rnd.Next(0, 4);
            if (-1 < locX & locX < 7)
            {
                speciesScript.evolve(true, 1);
                speciesScript.evolve(true, 2);
                speciesScript.evolve(true, 3);
                speciesScript.evolve(true, 8);
                speciesScript.evolve(true, 9);
                speciesScript.evolve(true, 10);
            }
            else if (6 < locX & locX < 11)
            {
                speciesScript.evolve(true, 4);
                speciesScript.evolve(true, 5);
                speciesScript.evolve(true, 6);
                speciesScript.evolve(true, 8);
                speciesScript.evolve(true, 9);
                speciesScript.evolve(true, 10);
            }
            else if (10 < locX & locX < 15)
            {
                speciesScript.evolve(true, 4);
                speciesScript.evolve(true, 5);
                speciesScript.evolve(true, 7);
                speciesScript.evolve(true, 8);
                speciesScript.evolve(true, 9);
                speciesScript.evolve(true, 10);
            }
            else if (14 < locX & locX < 30)
            {
                speciesScript.evolve(true, 15);
                speciesScript.evolve(true, 16);
                speciesScript.evolve(true, 19);
                locY += 3;
            }
            else if (29 < locX & locX < 40)
            {
                speciesScript.evolve(true, 15);
                speciesScript.evolve(true, 16);
                speciesScript.evolve(true, 18);
                locY += 3;
            }
            else
            {
                speciesScript.evolve(true, 15);
                speciesScript.evolve(true, 16);
                speciesScript.evolve(true, 17);
                locY += 3;
            }
            for (int j = 0; j < locY; j++)
            {
                speciesScript.evolve(true, -1);
            }
        }
        List<Vector2Int> playerLctn = new List<Vector2Int>();
        List<int> playerGns = new List<int>();
        locX = rnd.Next(1, 99);
        locY = rnd.Next(1, 99);
        playerLctn.Add(new Vector2Int(locX, locY));
        Species playerSpeciesScript = new Species("SHOULD NOT APPEAR: 0");
        playerSpeciesScript.Init("Player Species", 0, playerLctn, playerGns, new int[4], 1, 1, 1, 1, 1, 1, 1, 1);
        speciesDict.Add(0, playerSpeciesScript);
        //  addNode is now locked to true, we may want to change this later, time permitted
        playerSpeciesScript.evolve(true, 0);
        Global.mutationPoints = 11;
        Global.playerSpeciesGeneList = playerGns;
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(sp.Value.getLocation()[0]).GetComponent<TileData>().setLocalSpecies(sp.Value, 10, getPlayerSpecies());
        }
    }

    /*
     *  COMPLETE
     *  Have the species in each tile containing species eat
     */
    private void Interact()
    {
        //print("Interact()");
        HerbivoreMove();
        CarnivoreMove();
    }

    /*
     *  NEEDS POPULATION MODIFIERS
     *  NEEDS MUTATION MODIFIERS
     *  NEEDS REVIEW - TOTILEWORLD ISSUE
     *  Have the species in each tile reproduce
     */
    private void Reproduce()
    {
        //print("Reproduce()");
        if (Global.change)
        {
            Species playerSpecies = getPlayerSpecies();
            for (int i = 0; i < Global.newGenes.Count; i++)
            {
                playerSpecies.evolve(true, Global.newGenes[i]);
            }
            Global.mutationPoints -= Global.newGenes.Count;
            Global.newGenes.Clear();
            Global.change = false;
        }
        int population = 0;
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {
            int originalLocationCount = sp.Value.getLocation().Count;
            for (int i = 0; i < originalLocationCount; i++)
            {
                population = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(sp.Value.getLocation()[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                population += population * sp.Value.getLitterSize();
                /*
                 *  NEEDS POPULATION MODIFIERS
                 */
                if (population > sp.Value.getMaxPerTile())
                {
                    Overpopulate(sp.Value, sp.Value.getLocation()[i]);
                }
                else
                {
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(sp.Value.getLocation()[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Value.getSpeciesID(), population);
                }
            }
            /*
             *  NEEDS MUTATION MODIFIERS
             */
            System.Random rnd = new System.Random();
            int mutVarTemp = 100;
            if (rnd.Next(1, 101) <= mutVarTemp)
            {
                Mutate(sp.Value, (sp.Key == 0));
            }
        }
    }

    /*
     *  ACHIEVEMENTS NEEDED
     *  CHILD SPECIES NEEDED TO BE ADDED TO WORLD
     *  Parent Species will be copied into new speciesObject (mutatingSpecies) that will evolve once
     */
    private void Mutate(Species parentSpecies, bool isPlayer)
    {
        //print("Mutate()");
        Species mutatingSpecies = new Species("Species: " + speciesDict.Count + 1);
        mutatingSpecies.clone(parentSpecies);
        /*
         *  CHILD SPECIES NEEDED TO BE CREATED
         */
        if (isPlayer)
        {
            Global.mutationPoints += 1;
            Global.playerSpeciesGeneList = parentSpecies.getGenes();
        }
        else
        {
            parentSpecies.evolve(true, -1);
        }
    }

    /*
     *  COMPLETE
     *  Have the species in a given tile migrate to adjacent tile
     */
    private void Overpopulate(Species migratingSpecies, Vector2Int tileLocation)
    {
        //print("Overpopulate()");
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
    private void Migrate(Vector2Int recievingTile, Species migratingSpecies, Vector2Int givingTile)
    {
        //print("Migrate()");
        TileData rTile = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(recievingTile).GetComponent<TileData>();
        TileData gTile = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(givingTile).GetComponent<TileData>();
        int pop = gTile.getSpeciesPopulation(migratingSpecies.getSpeciesID());
        int movingPopulation = (int)(0.3 * pop);
        if (movingPopulation == 0)
        {
            movingPopulation = 1;
        }
        int stayingPopulation = (int)(0.7 * pop);
        if (stayingPopulation == 0)
        {
            stayingPopulation = 1;
        }
        bool speciesIsThere = false;
        foreach (KeyValuePair<int, int> localS in rTile.getLocalSpecies())
        {
            if (localS.Key == migratingSpecies.getSpeciesID())
            {
                speciesIsThere = true;
            }
        }
        if (speciesIsThere)
        {
            rTile.setSpeciesPopulation(migratingSpecies.getSpeciesID(), rTile.getSpeciesPopulation(migratingSpecies.getSpeciesID()) + movingPopulation);
        }
        else
        {
            rTile.setLocalSpecies(migratingSpecies, movingPopulation, getPlayerSpecies());
        }
        gTile.setSpeciesPopulation(migratingSpecies.getSpeciesID(), stayingPopulation);
        migratingSpecies.addToLocation(recievingTile);
    }

    /*
     *  IMPLEMENTATION NEEDED
     *  Have the herbivore species in each tile containing species eat tile resources
     */
    private void HerbivoreMove()
    {
        //print("HerbivoreMove()");
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
        //print("CarnivoreMove()");
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