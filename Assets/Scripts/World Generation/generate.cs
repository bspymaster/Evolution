using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{

    public GameObject Ice;
    public GameObject Plains;
    public GameObject Forest;
    public GameObject Desert;
    public GameObject Lake;
    public GameObject Ocean;
   PerlinNoise noise;
    void Start()
    {
        noise = new PerlinNoise(Random.Range(0,1000000));
        Regen();

    }
    private void Regen()
    {
        int minY = -200;
        int minX = -200;
        int maxY = 200;
        int maxX = 200;
        float width = Ice.transform.lossyScale.x;
        float height = Ice.transform.lossyScale.y;
        for (int i = minX; i < maxX; i++) //columns (x values)
        {
            int hrand = noise.getNoise(i, maxX - minX);
            for (int j = minY; j < maxY; j++)//rows (y values)
            {
                int lrand = noise.getNoise(j, maxY-minY);
                int rand = (hrand + lrand) /6;
                if (rand % 8 == 0)
                    Instantiate(Lake, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 8 == 1)
                    Instantiate(Ice, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 8==2 )
                    Instantiate(Forest, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 8 == 3)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 8== 4)
                    Instantiate(Desert, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 8 == 5)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 8== 6)
                    Instantiate(Lake, new Vector2(i * width, j * height), Quaternion.identity);
                else
                    Instantiate(Ocean, new Vector2(i * width, j * height), Quaternion.identity);
            }

        }

    }
}

