using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallGeneration {
    public int seed; //store in game controller
    public int width;
    public int height;
    public int[,] wall;

    public int[] top;
    public int[] left;
    public int[] right;
    public int[] bottom;
    public List<int[]> holes;

    public int[,] exits;

    public WallGeneration(int seed, int[] top, int[] left, int[] right, int[] bottom, List<int[]> holes)
    {
        Random.seed = seed;
        width = top.Length;
        height = left.Length;
        this.top = top;
        this.left = left;
        this.right = right;
        this.bottom = bottom;
        this.holes = holes;

        List<int[]> exits = new List<int[]>();

        wall = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            if(top[i] == 1)
            {

                wall[0,i] = 1;
                exits.Add(new int[] {0,i});
            }
            if(bottom[i] == 1)
            {
                wall[width - 1, i] = 1;
                exits.Add(new int[] { width - 1, i });
            }
        }


        for (int i = 0; i < height; i++)
        {
            if (left[i] == 1)
            {
                wall[i, 0] = 1;
                exits.Add(new int[] { i,0 });
            }
            if (right[i] == 1)
            {
                wall[i,height - 1] = 1;
                exits.Add(new int[] {i, height - 1});
            }
        }


    }

}
