using System.Collections.Generic;
using UnityEngine;

public class GridGraph
{
    public int Width;
    public int Height;

    public Node[,] Grid;
    public List<Vector2> Walls;
    public List<Vector2> Forests;

    public GridGraph(int w, int h)
    {
        Width = w;
        Height = h;

        Grid = new Node[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Grid[x, y] = new Node(x, y);
            }
        }
    }

    public bool InBounds(Vector2 v)
    {
        if (v.x >= 0 && v.x < this.Width &&
            v.y >= 0 && v.y < this.Height)
            return true;
        else
            return false;
    }

    public bool Passable(Vector2 id)
    {
        if (Walls.Contains(id)) return false;
        else return true;
    }

    public List<Node> Neighbours(Node n)
    {
        List<Node> results = new List<Node>();

        List<Vector2> directions = new List<Vector2>()
        {
            new Vector2( -1, 0 ), // left
            new Vector2( 0, 1 ),  // top
            new Vector2( 1, 0 ),  // right
            new Vector2( 0, -1 ), // bottom
        };

        foreach (Vector2 v in directions)
        {
            Vector2 newVector = v + n.Position;
            if (InBounds(newVector) && Passable(newVector))
            {
                results.Add(Grid[(int)newVector.x, (int)newVector.y]);
            }
        }

        return results;
    }

    public int Cost(Node b)
    {
        // If Node 'b' is a Forest return 2, otherwise 1
        if (Forests.Contains(b.Position)) return 2;
        else return 1;
    }
}
