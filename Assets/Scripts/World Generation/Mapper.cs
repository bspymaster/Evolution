using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapper : MonoBehaviour {

    public GameObject DesertTilePrefab;
    public GameObject ForestTilePrefab;

    int width = 20;
    int height = 20;
    
    

    float oddRowXoffset = 18f;
    float yOffset = 16f;

	// Use this for initialization

	public void createMap (int z) {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * oddRowXoffset;

                if (y % 2 == 1)
                {
                    xPos += oddRowXoffset / 2f;
                }
                if (z == 0) {

                    Instantiate(DesertTilePrefab, new Vector3(xPos, y * yOffset, 0), Quaternion.identity);
                }
                if (z == 1)
                {

                    Instantiate(ForestTilePrefab, new Vector3(xPos, y * yOffset, 0), Quaternion.identity);
                }
            }
        }

	}

    public void createForestMap()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * oddRowXoffset;

                if (y % 2 == 1)
                {
                    xPos += oddRowXoffset / 2f;
                }

                    Instantiate(ForestTilePrefab, new Vector3(xPos, y * yOffset, 0), Quaternion.identity);
                
            }
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
