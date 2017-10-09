using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{

    long seed;

    public PerlinNoise(long seed)
    {
        this.seed = seed;

    }
    private int random(int x,int range)
    {

        return (int)((x + seed) ^ 3) % range;

    }
    public int getNoise(int x, int range)
    {
        int chunkSize = 35;
        int chunkIndex = x / 35;
        float prog = (x % chunkSize) / (chunkSize * 1f);
        float left_random = random(chunkIndex, range);
        float  right_random = random(chunkIndex + 1, range);

        float noise = (1 - prog) * left_random + prog * right_random;
        return (int)Mathf.Round(noise);
    }







}
