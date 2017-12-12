using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour
{

    private int mapSize;
    private Dictionary<int, Species> speciesDict;   //  speciesID as key, Species as value
    private bool[] alerts;
    private int nextId;

    /*
    *  COMPLETE
    *  Use this for initialization
    */
    public void GenerateSpecies()
    {
        //print("GenerateSpecies()");
        nextId = 11;
        GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("It Begins - Start evolving!", "I'll trade a magic trick for a vase!"));
        speciesDict = new Dictionary<int, Species>();
        mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize() - 1;
        alerts = new bool[10] { true, true, true, true, true, true, true, true, true, true };
        Spawn();
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
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
            while (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTileType() == "Ocean" ||
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() < 40 ||
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() > 100 ||
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getAltitude() > 40)
            {   //  checks if tile is in the ocean, too high, too cold, or too hot
                locX = rnd.Next(1, 99);
                locY = rnd.Next(1, 99);
            }
            lctn.Add(new Vector2Int(locX, locY));
            speciesScript.Init("Species: " + i.ToString(), i, lctn, gns, new int[4] { 0, 0, 0, 0 }, -1, 50, 50, 100, 1, 1, 1, 1, new Vector2Int(40, 100), 100, 0);
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
        locX = rnd.Next(44, 56);
        locY = rnd.Next(44, 56);
        while (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTileType() == "Ocean" ||
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() < 40 ||
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() > 100 ||
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getAltitude() > 40)
        {   //  checks if tile is in the ocean, too high, too cold, or too hot
            locX = rnd.Next(33, 67);
            locY = rnd.Next(33, 67);
        }
        playerLctn.Add(new Vector2Int(locX, locY));
        Species playerSpeciesScript = new Species("SHOULD NOT APPEAR: 0");
        playerSpeciesScript.Init("Player Species", 0, playerLctn, playerGns, new int[4] { 0, 0, 0, 0 }, -1, 50, 50, 100, 1, 1, 1, 1, new Vector2Int(40, 100), 100, 0);
        speciesDict.Add(0, playerSpeciesScript);
        Global.mutationPoints = 12;
        Global.playerSpeciesGeneList = playerGns;
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(sp.Value.getLocation()[0]).GetComponent<TileData>().setLocalSpecies(sp.Value, 100, speciesDict[0]);
        }
    }

    /*
     *  COMPLETE
     *  Have the species in each tile containing species eat
     */
    private void Interact()
    {
        //print("Interact()");
        if (speciesDict.Count > 100 & alerts[9])
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Pretty Crowded 'Round Here", "There are over 100 species!"));
            alerts[9] = false;
        }
        int aliveBefore = speciesDict.Count;
        HerbivoreMove();
        CarnivoreMove();
        if (speciesDict.Count < aliveBefore)
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Another One Bites The Dust", "A potential competitor has gone extinct somewhere in the world!"));
        }
    }

    /*
     *  NEEDS POPULATION MODIFIERS
     *  NEEDS MUTATION MODIFIERS
     *  Have the species in each tile reproduce
     */
    private void Reproduce()
    {
        //print("Reproduce()");
        if (Global.change)
        {
            //print("change");
            Species playerSpecies = speciesDict[0];
            Species childSpecies = new Species("SHOULD NOT APPEAR: -3");
            childSpecies.clone(playerSpecies, nextId);
            speciesDict.Add(nextId, childSpecies);
            nextId++;
            for (int i = 0; i < speciesDict[childSpecies.getSpeciesID()].getLocation().Count; i++)
            {
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(speciesDict[childSpecies.getSpeciesID()].getLocation()[i]).GetComponent<TileData>().setLocalSpecies(speciesDict[childSpecies.getSpeciesID()], 30, speciesDict[0]);
            }
            if (playerSpecies.getGenes().Count > 0 & alerts[0])
            {
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("First Mutation!", "Congratulations, your species has evolved! But beware, your old self still roams"));
                alerts[0] = false;
            }
            for (int i = 0; i < Global.newGenes.Count; i++)
            {
                playerSpecies.evolve(true, Global.newGenes[i]);
            }
            Global.newGenes.Clear();
            Global.change = false;
        }
        int population = 0;
        bool mutation = false;
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {
            int originalLocationCount = sp.Value.getLocation().Count;
            for (int i = 0; i < originalLocationCount; i++)
            {
                if (speciesDict.Count == 1 & sp.Value.getSpeciesID() == 0 & alerts[1])
                {
                    GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Veni, Vidi, Vici", "You've won! You have led all other potential competition to extinction. Congratulations?"));
                    alerts[1] = false;
                }
                int initialPop = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(sp.Value.getLocation()[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                population = initialPop;
                population += population * sp.Value.getLitterSize();
                /*
                 *  NEEDS POPULATION MODIFIERS
                 */
                if (population > (initialPop * 10) & alerts[5])
                {
                    GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("You Good For Another Round?", "Increase the population in a single tile tenfold"));
                    alerts[5] = false;
                }
                if (population > sp.Value.getMaxPerTile())
                {
                    Overpopulate(sp.Value, sp.Value.getLocation()[i]);
                }
                else
                {
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(sp.Value.getLocation()[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, population);
                }
            }
            System.Random rnd = new System.Random();
            int mutVarTemp = 50;
            /*
             *  NEEDS MUTATION MODIFIERS
             */
            if (rnd.Next(1, 101) <= mutVarTemp)
            {
                if (sp.Value.getSpeciesID() != 0)
                {
                    mutation = true;
                }
                Mutate(sp.Value, (sp.Key == 0));
            }
        }
        if (mutation)
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("It's Alive!", "Another competitor emerges from the chaos"));
            mutation = false;
        }
    }

    /*
     *  COMPLETE
     *  Parent Species will be copied into new speciesObject (mutatingSpecies) that will evolve once
     */
    private void Mutate(Species parentSpecies, bool isPlayer)
    {
        //print("Mutate()");
        if (isPlayer)
        {
            if (parentSpecies.getGenes().Count == GameObject.Find("Web Builder").GetComponent<buildWeb>().getNumNodes() & alerts[2])
            {
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Perfect Organism", "'You still don't understand what you're dealing with, do you?' Obtain all genes"));
                alerts[2] = false;
                return;
            }
            Global.mutationPoints += 1;
            if (Global.mutationPoints > 19 & alerts[3])
            {
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Horder", "Have 20+ mutation points and still survive without using them"));
                alerts[3] = false;
            }
            Global.playerSpeciesGeneList = parentSpecies.getGenes();
        }
        else
        {
            Species childSpecies = new Species("SHOULD NOT APPEAR: -4");
            childSpecies.clone(parentSpecies, nextId);
            speciesDict.Add(nextId, childSpecies);
            nextId++;
            for (int i = 0; i < speciesDict[childSpecies.getSpeciesID()].getLocation().Count; i++)
            {
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(speciesDict[childSpecies.getSpeciesID()].getLocation()[i]).GetComponent<TileData>().setLocalSpecies(speciesDict[childSpecies.getSpeciesID()], 30, speciesDict[0]);
            }
            parentSpecies.evolve(true, -1);
        }
        if (parentSpecies.getGenes().Count * 2 > GameObject.Find("Web Builder").GetComponent<buildWeb>().getNumNodes() & alerts[8])
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Clever Girl", "Obtain 50% of all genes"));
            alerts[8] = false;
        }
    }

    /*
     *  OCEAN IMPLEMENTATION NEEDED
     *  ALTITUDE IMPLEMENTATION NEEDED
     *  TEMPERATURE IMPLEMENTATION NEEDED
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
        else
        {
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
     *  COMPLETE
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
        foreach (KeyValuePair<int, Species> localS in rTile.getLocalSpecies())
        {
            if (localS.Value.getSpeciesID() == migratingSpecies.getSpeciesID())
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
            rTile.setLocalSpecies(migratingSpecies, movingPopulation, speciesDict[0]);
            migratingSpecies.addToLocation(recievingTile);
        }
        gTile.setSpeciesPopulation(migratingSpecies.getSpeciesID(), stayingPopulation);
        if (speciesDict[0].getLocation().Count > 99 & alerts[4])
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("A Whole New World", "Have members of your species in 100+ tiles"));
            alerts[4] = false;
        }
        if (speciesDict[0].getLocation().Count > 5000 & alerts[6])
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Type 1 Civilization", "Exist in over half of the world"));
            alerts[6] = false;
        }
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
        if (alerts[7])
        {
            if (speciesDict[0].getLocation().Count == 0)
            {
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Game Over, Man; Game Over!", "And because the hungry hungry baby ate too many people, it exploded"));
                alerts[7] = false;
                speciesDict.Remove(0);
                /*
                 *  END GAME?
                 */
            }
        }
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
        if (alerts[7])
        {   //  seperated these checks, as if player species dies in the other move method, checking its location would cause an error
            if (speciesDict[0].getLocation().Count == 0)
            {
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Game Over, Man; Game Over!", "And because the hungry hungry baby ate too many people, it exploded"));
                alerts[7] = false;
                speciesDict.Remove(0);
                /*
                 *  END GAME?
                 */
            }
        }
    }
}