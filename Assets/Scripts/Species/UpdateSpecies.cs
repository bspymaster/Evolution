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
     *  Have the species in each tile evolve
     */
    private void Mutation()
    {
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
        print("reproduce");
        // have species mate
    }

    /*
     *  Have the herbivore species in each tile containing species eat tile resources
     */
    private void HerbivoreMove()
    {
        // get list of tiles who have herbivores in them
        // for each valid tile, get herbivore food sources (tile gets)
        // for each species in valid tile, species.getHFS()
        // for (int i = 0; i < amountSpecies; i++)
    }

    /*
     *  Have the carnivore species in each tile containing species eat other species and tiny species (tile resource)
     */
    private void CarnivoreMove()
    {
        print("carnivore");
    }
}