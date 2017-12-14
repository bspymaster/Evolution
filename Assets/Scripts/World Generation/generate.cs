using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{

    //public GameObject Ice;
    //public GameObject Plains;
    //public GameObject Forest;
    //public GameObject Desert;
    //public GameObject Ocean;
    public GameObject BaseWorldTile;
    PerlinNoise noise;

    public void MakeWorld()
    {
        noise = new PerlinNoise(Random.Range(0,1000000));
        Regen();
    }
    private void Regen()
    {
        int mapSize = GameObject.Find("TileList").GetComponent<TileListData>().getMapSize();
        int minY = 0;
        int minX = 0;
        int maxY = mapSize;
        int maxX = mapSize;
        //float width = BaseWorldTile.transform.lossyScale.x;
        float height = BaseWorldTile.transform.lossyScale.y;
        float width = (Mathf.Sqrt(3) / 2) * height;
        string[] tileType = { "Ocean", "Desert", "Plains", "Forest", "Tundra" };
        Color[] tileColors = { new Color(18f / 255f, 15f / 255f, 62f / 255f, 255f / 255f), new Color(197f / 255f, 183f / 255f, 68f / 255f, 255f / 255f),
                               new Color(60f/255f,242f/255f,2f/255f,255f/255f), new Color(0f/255f,143f/255f,48f/255f,255f/255f), new Color(67f/255f,186f/255f,203f/255f,255f/255f) };
        Vector2Int[] altitude = { new Vector2Int(0,0), new Vector2Int(0, 30), new Vector2Int(30, 40), new Vector2Int(40, 80), new Vector2Int(10, 90) };
        Vector2Int[] temperature = { new Vector2Int(25, 65), new Vector2Int(70, 90), new Vector2Int(60, 80), new Vector2Int(30, 70), new Vector2Int(-20, 30) };
        int numBerries = 0;
        int numNuts = 0;
        int numGrass = 0;
        int numLeaves = 0;
        int numAmbientMeat = 0;
        int rand = 0;
        TileListData tileList = GameObject.Find("TileList").GetComponent<TileListData>();

        for (int i = minX; i < maxX; i++) //columns (x values)
        {
            for (int j = minY; j < maxY; j++)//rows (y values)
            {
                numBerries = 0;
                numNuts = 0;
                numGrass = 0;
                numLeaves = 0;
                numAmbientMeat = 0;

                rand = noise.getNoise(i,j,maxX-minX);
                rand = rand % 7;

                if(rand == 5)
                {
                    rand = 3;
                }
                else if(rand == 6)
                {
                    rand = 0;
                }
                
                Vector2 offset = FindHexagonLocation(i,j);
                GameObject tileInstance = Instantiate(BaseWorldTile, new Vector2(/*offset **/ (float)(width - 2.5)* offset.x, (height - 2) *offset.y), Quaternion.identity);
                TileData instanceData = tileInstance.GetComponent<TileData>();
                instanceData.setTileType(tileType[rand]);
                //food is based on 0-200 is scarce 200-400 is below average 400-600 is average 600-800 is above average 800-1000 is plentiful
                if (rand == 0)  // Ocean
                {
                    numAmbientMeat = Random.Range(200,400);
                }
                else if (rand == 1)  // Desert
                {
                    numBerries = Random.Range(0,100);
                    numGrass = Random.Range(0,50);
                    numLeaves = Random.Range(0,25);
                    numAmbientMeat = Random.Range(200,400);
                }
                else if (rand == 2)  // Plains
                {
                    numBerries = Random.Range(300,500);
                    numNuts = Random.Range(100,200);
                    numGrass = Random.Range(800,1000);
                    numLeaves = Random.Range(200,400);
                    numAmbientMeat = Random.Range(400,600);
                }
                else if (rand == 3)  // Forest
                {
                    numBerries = Random.Range(800,1000);
                    numNuts = Random.Range(900,1000);
                    numGrass = Random.Range(500,1000);
                    numLeaves = Random.Range(800,1000);
                    numAmbientMeat = Random.Range(400,600);
                }
                else if (rand == 4)  // Tundra
                {
                    numBerries = Random.Range(200,400);
                    numNuts = Random.Range(300,500);
                    numGrass = Random.Range(0,200);
                    numLeaves = Random.Range(200,300);
                    numAmbientMeat = Random.Range(400,600);
                }
                
                instanceData.setNumBerries(numBerries);
                instanceData.setNumNuts(numNuts);
                instanceData.setNumGrass(numGrass);
                instanceData.setNumLeaves(numLeaves);
                instanceData.setNumAmbientMeat(numAmbientMeat);
                tileInstance.GetComponent<SpriteRenderer>().color = tileColors[rand];
                instanceData.setAltitude(Random.Range(altitude[rand].x, altitude[rand].y));
                instanceData.setTemperature(Random.Range(temperature[rand].x, temperature[rand].y));
                tileList.setTileAtLocation(new Vector2Int(i,j),tileInstance);
            }

        }

    }
    public Vector2 FindHexagonLocation(int col, int row)
    {
        float X = Mathf.Sqrt(3) * (float)(col + .5 * row);
        float Y = 3 / 2 * row;
        return new Vector2(X, Y);
    }

}

