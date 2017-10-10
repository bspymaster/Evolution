using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creation : MonoBehaviour {
    public int minheight;
    public int maxheight;
    public int mintemperature;
    public int maxtemperature;
    public int temperature;
    public int height;
	// Use this for initialization
	void Start () {
        temperature = Random.Range(mintemperature, maxtemperature);
        height = Random.Range(minheight, maxtemperature);
       //print(temperature);

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
