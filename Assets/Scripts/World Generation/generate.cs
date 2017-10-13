using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{

    public GameObject Ice;
    public GameObject Plains;
    public GameObject Forest;
    public GameObject Desert;
    public GameObject Ocean;
    public GameObject species;
   PerlinNoise noise;
    public Species speciesVariable;

    public void MakeMap()
    {
        noise = new PerlinNoise(Random.Range(0,1000000));
        Regen();
        Spawn();
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
            for (int j = minY; j < maxY; j++)//rows (y values)
            {
                 
 int rand= noise.getNoise(i,j, maxY-minY);
                if (rand % 9 == 0)
                    Instantiate(Ocean, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9 == 1)
                    Instantiate(Desert, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9==2 )
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9 == 3)
                    Instantiate(Forest, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9== 4)
                    Instantiate(Ice, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9 ==5)
                    Instantiate(Forest, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9 == 6)
                    Instantiate(Plains, new Vector2(i * width, j * height), Quaternion.identity);
                else if (rand % 9 == 7)
                    Instantiate(Desert, new Vector2(i * width, j * height), Quaternion.identity);        
               else
                    Instantiate(Ocean, new Vector2(i * width, j * height), Quaternion.identity);
            }

        }

    }
    /*
     * Spawn() generates 10 game objects as species on tiles other than ocean tiles on game creation
     */
    private void Spawn()
    {
        var rnd = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            int locX = rnd.Next(-100, 100);
            int locY = rnd.Next(-100, 100);
            //  while tile @ locX, locY == Ocean {
                    //locX = rnd.Next(-100, 100);
                    //locY = rnd.Next(-100, 100);
            //  }
            float width = species.transform.lossyScale.x;
            float height = species.transform.lossyScale.y;
            Instantiate(species, new Vector2(width * locX, height * locY), Quaternion.identity);
        }
    }
}

