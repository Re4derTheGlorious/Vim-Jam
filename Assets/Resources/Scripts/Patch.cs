using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    public Patch[] neighbours = new Patch[4];
    public int posX;
    public int posY;

    public void Extend()
    {
        if (transform.Find("Graphic").GetComponent<Renderer>().isVisible)
        {
            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] == null)
                {
                    Patch newPatch = Instantiate(Resources.Load("Prefabs/Patch") as GameObject, transform.parent).GetComponent<Patch>();
                    int x = 0;
                    int y = 0;

                    if (i == 0)
                    {
                        y++;
                    }
                    else if (i == 1)
                    {
                        x++;
                    }
                    else if (i == 2)
                    {
                        y--;
                    }
                    else
                    {
                        x--;
                    }

                    newPatch.transform.position = transform.position;
                    newPatch.transform.Translate(new Vector2(x, y));
                    newPatch.neighbours[(i + 2) % 4] = this;
                    newPatch.gameObject.name = "Patch " + x + ":" + y;
                    newPatch.posX = posX + x;
                    newPatch.posY = posY + y;
                    neighbours[i] = newPatch;
                    newPatch.FillNeighbours();
                    newPatch.Spawn();
                }
            }
        }
    }

    public void Spawn()
    {
        GetComponent<Animator>().Play("Drop");
    }

    public void LinkWith(Patch link, int dir) {
        neighbours[dir] = link;
        link.neighbours[(dir + 2) % 4] = this;
    }
    private Patch FindPatch(int x, int y)
    {
        foreach(Transform obj in transform.parent)
        {
            if(obj.gameObject.GetComponent<Patch>().posX==x && obj.gameObject.GetComponent<Patch>().posY == y)
            {
                return obj.gameObject.GetComponent<Patch>();
            }
        }
        return null;
    }
    public void FillNeighbours()
    {
        Patch[] links = new Patch[4];
        links[0] = neighbours[0] == null ? FindPatch(posX, posY + 1):null;
        links[1] = neighbours[1] == null ? FindPatch(posX+1, posY):null;
        links[2] = neighbours[2] == null ? FindPatch(posX, posY - 1):null;
        links[3] = neighbours[3] == null ? FindPatch(posX-1, posY):null;

        for (int i = 0; i < links.Length; i++)
        {
            if (links[i])
            {
                LinkWith(links[i], i);
            }
        }
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            Extend();
        //}
    }
}
