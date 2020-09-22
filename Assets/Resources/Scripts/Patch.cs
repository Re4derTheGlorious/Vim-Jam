using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    public Patch[] neighbours = new Patch[4];
    public int posX;
    public int posY;

    private int spawnRange = 6;
    private int despawnRange = 12;

    public bool OutOfRange()
    {
        return Vector2.Distance(GameObject.Find("Player").transform.position, transform.position) > despawnRange;
    }

    public bool InRange()
    {
        return Vector2.Distance(GameObject.Find("Player").transform.position, transform.position) < spawnRange;
    }

    public void Extend()
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
                newPatch.posX = posX + x;
                newPatch.posY = posY + y;
                newPatch.gameObject.name = "Patch " + newPatch.posX + ":" + newPatch.posY;

                Grid.GridPatch template = GameObject.Find("World").GetComponent<Grid>().map[newPatch.posX, newPatch.posY];
                newPatch.transform.Find("Graphic").gameObject.GetComponent<Renderer>().material.color = template.color;

                neighbours[i] = newPatch;
                newPatch.FillNeighbours();
                newPatch.Spawn();
            }
        }
    }

    public void Spawn()
    {
        GetComponent<Animator>().Play("Spawn");
    }
    public void Despawn()
    {
        GetComponent<Animator>().Play("Despawn");

        float length = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        Invoke("Invoke_Remove", length);
    }

    private void Invoke_Remove()
    {
        Destroy(gameObject);
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
        if (InRange())
        {
            Extend();
        }
        else if(OutOfRange())
        {
            Despawn();
        }
    }
}
