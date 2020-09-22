using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public class GridPatch
    {
        public Color color;
    }

    public GridPatch[,] map = new GridPatch[100,100];

    void Start()
    {
        for(int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                map[x,y] = new GridPatch();
                map[x,y].color = Color.white;
            }
        }

        map[5,0].color = Color.blue;
    }
}
