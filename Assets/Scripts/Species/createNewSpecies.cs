using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createNewSpecies : MonoBehaviour {
    private int count;
	// Use this for initialization
	void Start () {
        Species newSpecies = new Species();
        count++;
        newSpecies.setName(count.ToString());
        newSpecies.setLocation(new int[]);
        newSpecies.setGenes(new int[]);
        newSpecies.spawn(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
