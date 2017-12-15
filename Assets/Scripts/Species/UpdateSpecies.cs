using System.Collections.Generic;
using System.Linq;
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
        nextId = 100;
        GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("It Begins.", "Start evolving!"));
        speciesDict = new Dictionary<int, Species>();
        mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize() - 1;
        alerts = new bool[10] { true, true, true, true, true, true, true, true, true, true };
        Spawn(nextId);
        InvokeRepeating("Interact", 10f, 20f);
        InvokeRepeating("Reproduce", 5f, 20f);
    }

    /*
     *  COMPLETE
     *  Spawn() generates n game objects as species on game creation
     */
    private void Spawn(int numSpecies)
    {
        //print("Spawn()");
        var rnd = new System.Random();
        int locX = 0;
        int locY = 0;
        for (int i = 1; i < numSpecies; i++)
        {
            Species speciesScript = new Species("SHOULD NOT APPEAR: -2");
            List<Vector2Int> lctn = new List<Vector2Int>();
            List<int> gns = new List<int>();
            locX = rnd.Next(1, 99);
            locY = rnd.Next(1, 99);
            while (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTileType() == "Ocean" ||
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() < 40 ||
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() > 90 ||
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getAltitude() > 40)
            {   //  checks if tile is in the ocean, too high, too cold, or too hot
                locX = rnd.Next(1, 99);
                locY = rnd.Next(1, 99);
            }
            lctn.Add(new Vector2Int(locX, locY));

            /*
             *  (string speciesName, int speciesID, List<Vector2Int> location, List<int> genes, int[] herbivoreFoodSource, int carnivoreFoodSource,
             *  int requiredCalories, int creatureSize, int litterSize, int reproductionRate, int mutationChance, int carnivorous, int offspringSize, int altitude,
             *  int canFly, int dexterity, int maxPerTile, int peckingOrder, int offspringSurvivalChance, int canSwim)
             */

            speciesScript.Init("Species: " + i.ToString(), i, lctn, gns, new int[4] { 0, 0, 0, 0 }, 0, 100, 50, 1, 1, 10, 0, 10, 50, 0, 1, 200, 0, 10, 0, new Vector2Int(32, 70));
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
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getAltitude() > 40 ||
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() > 70 ||
            GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(locX, locY)).GetComponent<TileData>().getTemperature() < 32)
        {   //  checks if tile is in the ocean, too high, too cold, or too hot
            locX = rnd.Next(33, 67);
            locY = rnd.Next(33, 67);
        }
        playerLctn.Add(new Vector2Int(locX, locY));
        Species playerSpeciesScript = new Species("SHOULD NOT APPEAR: 0");
        playerSpeciesScript.Init("Player Species", 0, playerLctn, playerGns, new int[4] { 0, 0, 0, 0 }, 0, 100, 50, 1, 1, 10, 0, 10, 50, 0, 1, 200, 0, 10, 0, new Vector2Int(32, 70));
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
     *  COMPLETE
     *  Have the species in each tile reproduce
     */
    private void Reproduce()
    {
        //print("Reproduce()");
        if (Global.change)
        {
            //print("change");
            Species playerSpecies = speciesDict[0];
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
        List<int> mutatingSpecies = new List<int>();
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
                population += sp.Value.getReproductionRate() * sp.Value.getLitterSize();
                int offspring = population / sp.Value.getLitterSize();
                population -= offspring;
                var rnda = new System.Random();
                offspring = offspring * (rnda.Next(0, 100 - sp.Value.getOffspringSurvivalChance()) / 100);
                population += offspring;
                if (population > (initialPop * 10) & alerts[5])
                {
                    GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("And Ten Will Take My Place", "Increase the population in a single tile tenfold"));
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
            int mutVarTemp = 80 - sp.Value.getMutationChance();
            if (rnd.Next(1, 101) <= mutVarTemp)
            {
                if (sp.Value.getSpeciesID() != 0)
                {
                    mutation = true;
                }
                mutatingSpecies.Add(sp.Value.getSpeciesID());
            }
        }
        if (mutation)
        {
            for (int i = 0; i < mutatingSpecies.Count; i++)
            {
                Mutate(speciesDict[mutatingSpecies[i]], (mutatingSpecies[i] == 0));
            }
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("It's Adapting...", "Out from the chaos, a species grows ever stronger."));
            mutation = false;
        }
    }

    /*
     *  COMPLETE
     *      OBSOLETE - CHILD SPECIES NEEDED TO BE ADDED TO WORLD
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
            //Species childSpecies = new Species("SHOULD NOT APPEAR: -4");
            //childSpecies.clone(parentSpecies, nextId);
            //speciesDict.Add(nextId, childSpecies);
            //nextId++;
            //for (int i = 0; i < speciesDict[childSpecies.getSpeciesID()].getLocation().Count; i++)
            //{
            //    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(speciesDict[childSpecies.getSpeciesID()].getLocation()[i]).GetComponent<TileData>().setLocalSpecies(speciesDict[childSpecies.getSpeciesID()], 30, speciesDict[0]);
            //}
            parentSpecies.evolve(true, -1);
        }
        if (parentSpecies.getGenes().Count * 2 > GameObject.Find("Web Builder").GetComponent<buildWeb>().getNumNodes() & alerts[8])
        {
            GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Clever Girl", "Obtain 50% of all genes"));
            alerts[8] = false;
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
        Vector2Int target = new Vector2Int(0, 0);
        bool flag = false;
        int receivingTile = rnd.Next(0, 6); //  0 is left tile, 1 is right tile, 2 is top-left tile, 3 is top-right tile, 4 is bottom-left tile, 5 is bottom-right tile
        if (receivingTile == 0)
        {
            target = new Vector2Int(tileLocation.x - 1, tileLocation.y);
            if (target.x < 0)
            {
                target.x += 2;
            }
        }
        else if (receivingTile == 1)
        {
            target = new Vector2Int(tileLocation.x + 1, tileLocation.y);
            if (target.x > mapSize)
            {
                target.x -= 2;
            }
        }
        else if (receivingTile == 2)
        {
            target = new Vector2Int(tileLocation.x, tileLocation.y + 1);
            if (target.y > mapSize)
            {
                target.y -= 2;
            }
        }
        else if (receivingTile == 3)
        {
            target = new Vector2Int(tileLocation.x + 1, tileLocation.y + 1);
            if (target.y > mapSize)
            {
                target.y -= 2;
            }
            if (target.x > mapSize)
            {
                target.x -= 2;
            }
        }
        else if (receivingTile == 4)
        {
            target = new Vector2Int(tileLocation.x, tileLocation.y - 1);
            if (target.y < 0)
            {
                target.y += 2;
            }
        }
        else
        {
            target = new Vector2Int(tileLocation.x + 1, tileLocation.y - 1);
            if (target.y < 0)
            {
                target.y += 2;
            }
            if (target.x > mapSize)
            {
                target.x -= 2;
            }
        }
        if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getTileType() == "Ocean")
        {   //  check ocean obstacle
            if (migratingSpecies.getCanFly() > 0)
            {   //  fly over ocean tiles in a straight line until land (new area) or border (wasted migrate)
                int x = rnd.Next(0, 2);
                int y = rnd.Next(0, 2);
                while (x == 0 & y == 0)
                {   //  makes sure species actually flies somewhere
                    x = rnd.Next(0, 2);
                    y = rnd.Next(0, 2);
                }
                int c = 10;
                while (!flag & c > 0)
                {
                    target.x += x;
                    target.y += y;
                    if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getTileType() != "Ocean" &
                        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getAltitude() < migratingSpecies.getAltitude() &
                        target.x < mapSize + 1 & target.x > -1 & target.y < mapSize + 1 & target.y > -1)
                    {   //  checks that this tile is not ocean, not too high, and still inside borders
                        flag = true;
                    }
                    c--;
                }
            }
            else if (migratingSpecies.getCanSwim() > 0)
            {   //  swim through ocean tiles randomly until land (new area) or energy runs out (wasted migrate)
                int tx;
                int ty;
                int count = 10;
                while (!flag & count > 0)
                {
                    tx = target.x + rnd.Next(0, 2);
                    ty = target.y + rnd.Next(0, 2);
                    if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(new Vector2Int(tx, ty)).GetComponent<TileData>().getTileType() != "Ocean" &
                        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getAltitude() < migratingSpecies.getAltitude() &
                        tx < mapSize + 1 & tx > -1 & ty < mapSize + 1 & ty > -1)
                    {   //  checks that this tile is not ocean, not too high, and still inside borders
                        target = new Vector2Int(tx, ty);
                        flag = true;
                    }
                    count--;
                }
            }
        }
        else if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getAltitude() < migratingSpecies.getAltitude())
        {   //  check altitude obstacle
            flag = true;
        }
        else if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getTemperature() < migratingSpecies.getTemperatureTolerance().y
            & GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(target).GetComponent<TileData>().getTemperature() > migratingSpecies.getTemperatureTolerance().x)
        {   //  check temperature obstacle

        }
        if (flag)
        {   //  if tile isn't appropriate, wasted migrate
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
     *  NEEDS REVIEW - BALANCE?
     *  Have the herbivore species in each tile containing species eat tile resources
     */
    private void HerbivoreMove()
    {
        //print("HerbivoreMove()");
        List<Vector2Int> berriesTiles = new List<Vector2Int>();
        List<Vector2Int> nutsTiles = new List<Vector2Int>();
        List<Vector2Int> leavesTiles = new List<Vector2Int>();
        List<Vector2Int> grassTiles = new List<Vector2Int>();
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {
            for (int i = 0; i < sp.Value.getLocation().Count; i++)
            {
                if (sp.Value.getHFS()[3] > 0)
                {   //  check if species eats berries
                    if (!berriesTiles.Contains(sp.Value.getLocation()[i]))
                    {   //  check if this tile is already in berries
                        berriesTiles.Add(sp.Value.getLocation()[i]);
                    }
                }
                else if (sp.Value.getHFS()[2] > 0)
                {   //  check if species eats nuts
                    if (!nutsTiles.Contains(sp.Value.getLocation()[i]))
                    {   //  check if this tile is already in berries
                        nutsTiles.Add(sp.Value.getLocation()[i]);
                    }

                }
                else if (sp.Value.getHFS()[1] > 0)
                {   //  check if species eats leaves
                    if (!leavesTiles.Contains(sp.Value.getLocation()[i]))
                    {   //  check if this tile is already in berries
                        leavesTiles.Add(sp.Value.getLocation()[i]);
                    }

                }
                else if (sp.Value.getHFS()[0] > 0)
                {   //  check if species eats grass
                    if (!grassTiles.Contains(sp.Value.getLocation()[i]))
                    {   //  check if this tile is already in berries
                        grassTiles.Add(sp.Value.getLocation()[i]);
                    }

                }
            }
        }
        for (int i = 0; i < berriesTiles.Count; i++)
        {   //  iterate through all tiles that contain at least one berry-eating species
            int food = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().getNumBerries() * 10;
            foreach (KeyValuePair<int, Species> sp in GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().getLocalSpecies())
            {
                int caloriesNeeded = sp.Value.getRequiredCalories() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((food - caloriesNeeded) / sp.Value.getCreatureSize()));
                if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                {
                    //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(berriesTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                }
            }
        }
        for (int i = 0; i < nutsTiles.Count; i++)
        {   //  iterate through all tiles that contain at least one berry-eating species
            int food = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().getNumBerries() * 10;
            foreach (KeyValuePair<int, Species> sp in GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().getLocalSpecies())
            {
                int caloriesNeeded = sp.Value.getRequiredCalories() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((food - caloriesNeeded) / sp.Value.getCreatureSize()));
                if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                {
                    //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(nutsTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                }
            }
        }
        for (int i = 0; i < grassTiles.Count; i++)
        {   //  iterate through all tiles that contain at least one berry-eating species
            int food = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().getNumBerries() * 10;
            foreach (KeyValuePair<int, Species> sp in GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().getLocalSpecies())
            {
                int caloriesNeeded = sp.Value.getRequiredCalories() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((food - caloriesNeeded) / sp.Value.getCreatureSize()));
                if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                {
                    //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(grassTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                }
            }
        }
        for (int i = 0; i < leavesTiles.Count; i++)
        {   //  iterate through all tiles that contain at least one berry-eating species
            int food = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().getNumBerries() * 10;
            foreach (KeyValuePair<int, Species> sp in GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().getLocalSpecies())
            {
                int caloriesNeeded = sp.Value.getRequiredCalories() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((food - caloriesNeeded) / sp.Value.getCreatureSize()));
                if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                {
                    //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(leavesTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                }
            }
        }
        if (alerts[7])
        {
            if (speciesDict[0].getLocation().Count == 0)
            {   //  seperated these checks, as if player species dies in the other move method, checking its location would cause an error
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Game Over, Man; Game Over!", "Sorry, but mother nature dealt her cruel hand against you this time!"));
                alerts[7] = false;
                speciesDict.Remove(0);
            }
        }
    }

    /*
     *  NEEDS REVIEW - BALANCE?
     *  Have the carnivore species in each tile containing species eat other species and tiny species (tile resource)
     */
    private void CarnivoreMove()
    {
        //print("CarnivoreMove()");
        List<Vector2Int> carnivoreTiles = new List<Vector2Int>();
        foreach (KeyValuePair<int, Species> sp in speciesDict)
        {   //  iterates through all species
            if (sp.Value.getCFS() > 0)
            {   //  checks if species is a carnivore
                for (int i = 0; i < sp.Value.getLocation().Count; i++)
                {   //  gets the carnivore's tiles
                    if (!carnivoreTiles.Contains(sp.Value.getLocation()[i]))
                    {   //  checks if the tile is already included
                        carnivoreTiles.Add(sp.Value.getLocation()[i]);
                    }
                }
            }
        }
        for (int i = 0; i < carnivoreTiles.Count; i++)
        {   //  iterates through each carniverous tile
            int tiny = GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getNumAmbientMeat();
            int small = 0;
            int medium = 0;
            int large = 0;
            int humungous = 0;
            foreach (KeyValuePair<int, Species> sp in GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getLocalSpecies())
            {   //  add food from other species
                if (sp.Value.getCreatureSize() > 300)
                {
                    humungous += sp.Value.getCreatureSize() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Value.getSpeciesID());
                }
                else if (sp.Value.getCreatureSize() > 200)
                {
                    large += sp.Value.getCreatureSize() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Value.getSpeciesID());
                }
                else if (sp.Value.getCreatureSize() > 100)
                {
                    medium += sp.Value.getCreatureSize() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Value.getSpeciesID());
                }
                else if (sp.Value.getCreatureSize() > 1)
                {
                    small += sp.Value.getCreatureSize() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Value.getSpeciesID());
                }
                tiny += sp.Value.getOffspringSurvivalChance() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Value.getSpeciesID());
            }
            int[] deaths = new int[4] { 0, 0, 0, 0 };
            foreach (KeyValuePair<int, Species> sp in GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getLocalSpecies())
            {   //  hunt for food
                int caloriesNeeded = sp.Value.getRequiredCalories() * GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key);
                bool more = true;
                if (sp.Value.getCFS() > 300)
                {
                    if (humungous > caloriesNeeded)
                    {
                        more = false;
                        tiny += humungous - caloriesNeeded;
                        deaths[0] = humungous - caloriesNeeded;
                    }
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((humungous - caloriesNeeded) / sp.Value.getCreatureSize()));
                }
                else if (sp.Value.getCFS() > 200 & more)
                {
                    if (large > caloriesNeeded)
                    {
                        more = false;
                        tiny += large - caloriesNeeded;
                        deaths[1] = large - caloriesNeeded;
                    }
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((large - caloriesNeeded) / sp.Value.getCreatureSize()));
                }
                else if (sp.Value.getCFS() > 100 & more)
                {
                    if (medium > caloriesNeeded)
                    {
                        more = false;
                        tiny += medium - caloriesNeeded;
                        deaths[2] = medium - caloriesNeeded;
                    }
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((medium - caloriesNeeded) / sp.Value.getCreatureSize()));
                }
                else if (sp.Value.getCFS() > 1 & more)
                {
                    if (small > caloriesNeeded)
                    {
                        more = false;
                        tiny += small - caloriesNeeded;
                        deaths[3] = small - caloriesNeeded;
                    }
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((small - caloriesNeeded) / sp.Value.getCreatureSize()));
                }
                else if (more)
                {
                    GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) + ((tiny - caloriesNeeded) / sp.Value.getCreatureSize()));
                }
                for (int j = 0; j < 4; j++)
                {   //  tally deaths
                    if (sp.Value.getCFS() > 300)
                    {
                        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) - (deaths[0] / GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getLocalSpecies().Count));
                        if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                        {
                            //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                        }
                    }
                    else if (sp.Value.getCFS() > 200)
                    {
                        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) - (deaths[1] / GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getLocalSpecies().Count));
                        if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                        {
                            //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                        }
                    }
                    else if (sp.Value.getCFS() > 100)
                    {
                        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) - (deaths[2] / GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getLocalSpecies().Count));
                        if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                        {
                            //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                        }
                    }
                    else if (sp.Value.getCFS() > 1)
                    {
                        GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().setSpeciesPopulation(sp.Key, GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) - (deaths[3] / GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getLocalSpecies().Count));
                        if (GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().getSpeciesPopulation(sp.Key) < 1)
                        {
                            //GameObject.Find("TileList").GetComponent<TileListData>().getTileAtLocation(carnivoreTiles[i]).GetComponent<TileData>().killSpecies(sp.Key);
                        }
                    }
                }
            }
        }
        if (alerts[7])
        {   //  seperated these checks, as if player species dies in the other move method, checking its location would cause an error
            if (speciesDict[0].getLocation().Count == 0)
            {
                GameObject.Find("EventSystem").GetComponent<AlertSystem>().addAlert(new Alert("Game Over, Man; Game Over!", "Sorry, but mother nature dealt her cruel hand against you this time!"));
                alerts[7] = false;
                speciesDict.Remove(0);
            }
        }
    }
}