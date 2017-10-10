using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{

    public GameObject Ice;
    public GameObject Plains;
    public GameObject Forest;
    public GameObject Desert;
<<<<<<< HEAD
    //public GameObject Lake;
=======
    // public GameObject Lake;
>>>>>>> 408ee0b02ea2b9644ae00ca6ee7d70b1db750897
    public GameObject Ocean;
   PerlinNoise noise;
    void Start()
    {
        noise = new PerlinNoise(Random.Range(0,1000000));
        Regen();

    }
    private void Regen()
    {
        int minY = -100;
        int minX = -100;
        int maxY = 100;
        int maxX = 100;
        float width = Ice.transform.lossyScale.x;
        float height = Ice.transform.lossyScale.y;
        for (int i = minX; i < maxX; i++) //columns (x values)
        {
            int hrand = noise.getNoise(i, maxX - minX);
            for (int j = minY; j < maxY; j++)//rows (y values)
            {
                int lrand = noise.getNoise(j, maxY-minY);
                int rand = (hrand + lrand) /6;
                if (rand % 7 == 0)
                    Instantiate(Ocean, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7 == 1)
                    Instantiate(Ice, new Vector2(i * width, j * height), Quaternion.identity);
<<<<<<< HEAD
                else if (rand % 7==2 )
                    Instantiate(Forest, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7 == 3)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7== 4)
                    Instantiate(Desert, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7 == 5)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
=======
                else if (rand % 7 ==2 )
                    Instantiate(Forest, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7 == 3)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7 == 4)
                    Instantiate(Desert, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 7 == 5)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
               // else if (rand % 8== 6)
                   // Instantiate(Lake, new Vector2(i * width, j * height), Quaternion.identity);
>>>>>>> 408ee0b02ea2b9644ae00ca6ee7d70b1db750897
                else
                    Instantiate(Ocean, new Vector2(i * width, j * height), Quaternion.identity);
            }

        }

    }
}

