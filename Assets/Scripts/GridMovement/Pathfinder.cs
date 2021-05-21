using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public int GraphWidth;
    public int GraphHeight;

    public Vector2 StartNodePosition;
    public Vector2 GoalNodePosition;

    public List<Vector2> Walls;
    public List<Vector2> Forests;

    private void Start() {
        Walls.Add(new Vector2(1, 2));

    }

    private void OnDrawGizmosSelected()
    {
        GridGraph map = new GridGraph(GraphWidth, GraphHeight);
        
        map.Walls = Walls;

        map.Forests = Forests;

        int x1 = (int)StartNodePosition.x;
        int y1 = (int)StartNodePosition.y;
        int x2 = (int)GoalNodePosition.x;
        int y2 = (int)GoalNodePosition.y;

        List<Node> path = AStar.Search(map, map.Grid[x1, y1], map.Grid[x2, y2]);

        for (int y = 0; y < GraphHeight; y++)
        {
            for (int x = 0; x < GraphWidth; x++)
            {
                Gizmos.DrawSphere(new Vector3(x, 0, y), 0.2f);
            }
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(StartNodePosition, 0.2f);

        Gizmos.color = Color.black;
        foreach (Vector2 wall in Walls)
        {
            Gizmos.DrawSphere(new Vector3(wall.x, 0, wall.y), 0.2f);
        }

        Gizmos.color = Color.green;
        foreach (Vector2 forest in Forests)
        {
            Gizmos.DrawSphere(forest, 0.2f);
        }

        foreach (Node n in path)
        {
            if (n.Position == GoalNodePosition)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawSphere(n.Position, 0.2f);
        }
    }
}
