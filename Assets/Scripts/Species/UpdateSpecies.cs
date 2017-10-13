using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpecies : MonoBehaviour {

    public GameObject speciesObject;
    public GameObject Ice;

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
        for (int i = 0; i < 10; i++)
        {
            int locX = rnd.Next(-100, 100);
            int locY = rnd.Next(-100, 100);
            float width = Ice.transform.lossyScale.x;
            float height = Ice.transform.lossyScale.y;
            Instantiate(speciesObject, new Vector2(width * locX, height * locY), Quaternion.identity);
            Species speciesScript = speciesObject.GetComponent<Species>();
            // set parameters
            speciesScript.Init(i.ToString(), new List<Vector2Int>(), new List<int>(), new List<int>(), 0, 0, 0, 0, 0, 0, 0, 0);
        }
    }

    /*
     *  Have the species in each tile evolve
     */
    public void Mutation()
    {
    }

    /*
     *  Have the species in each tile containing species eat
     */
    public void Interact()
    {
        HerbivoreMove();
        CarnivoreMove();
    }

    /*
     *  Have the species in each tile reproduce
     */
    public void Reproduce()
    {
        print("reproduce");
        // have species mate
    }

    /*
     *  Have the herbivore species in each tile containing species eat tile resources
     */
    public void HerbivoreMove()
    {
        print("herbivore");
    }

    /*
     *  Have the carnivore species in each tile containing species eat other species and tiny species (tile resource)
     */
    public void CarnivoreMove()
    {
        print("carnivore");
    }
}