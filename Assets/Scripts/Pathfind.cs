using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum State
    {
        None,
        Open,
        Closed
    }
    public int F = 0;   // Total estimated path length. F = G + h
    public int G = 0;   // Distance travelled so far.
    public int H = 0;   // Estimated distance remaining to target.
    public int C = 1;   // Cost of walking over this node.
    public bool Wall = false;   // Walls block movement.
    public Vector2Int Parent = new Vector2Int(-1,-1);   // The node before this one.
    public State state = State.None;    // Current node state. Could be none (not reached yet), Open (possible next node) and Closed (reached the node).
}


public class Pathfind : MonoBehaviour
{
    public static int GridWidth = 9;
    public static int GridHeight = 11;
    public static float CellSize = 1.0f;
    public static List<Vector2Int> openNodes = new List<Vector2Int>();


    public static Node[,] Nodes;
    public bool diagonal;
    //public Material groundMat;
    //static Texture2D tex;
    static Pathfind pathfind; 
    // Start is called before the first frame update

    public static void SetWall(int x, int y, bool wall)
    {
        if(Nodes == null)
        {
            Nodes = new Node[GridHeight, GridWidth];
        }
        Nodes[y, x].Wall = wall;
    }
    void Awake()
    {
        pathfind = this;

        //tex = new Texture2D(GridWidth, GridHeight);
        //tex.filterMode = FilterMode.Point;
        //groundMat.SetTexture("_MainTex", tex);
        if (Nodes == null)
        {
            Nodes = new Node[GridHeight, GridWidth];
        }
        for (int y = 0; y < GridHeight; ++y)
        {
            for (int x = 0; x < GridWidth; ++x)
            {
                Nodes[y, x] = new Node();
            }
        }
        //tex.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y <= GridHeight; ++y)
        {
            Debug.DrawLine(new Vector3(0, 0.1f, y), new Vector3(GridWidth, 0.1f, y));
        }
        for (int x = 0; x <= GridWidth; ++x)
        {
            Debug.DrawLine(new Vector3(x, 0.1f, 0), new Vector3(x, 0.1f, GridHeight));
        }
    }

    public static Node GetNode(Vector2Int pos)
    {
        return Nodes[pos.y, pos.x];
    }

    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        Vector2Int[] directions;
        if (pathfind.diagonal) {
            directions = new Vector2Int[] {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, -1)
        };
        }
        else
        {
            directions = new Vector2Int[] {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1), };
        }

        for (int y = 0; y < GridHeight; ++y)
        {
            for (int x = 0; x < GridWidth; ++x)
            {
                Nodes[y, x].state = Node.State.None;
                Nodes[y, x].Parent = new Vector2Int(-1, -1);
                Nodes[y, x].G = 0;
            }
        }
        openNodes.Clear();
        Vector2Int currentNode;

        openNodes.Add(start);
        Node startNode = GetNode(start);
        startNode.state = Node.State.Open;
        startNode.G = 0;

        while(openNodes.Count > 0)
        {
            int lowestF = 9000000;
            Vector2Int lowestFindex = new Vector2Int(-1,-1);
            foreach(var node in openNodes)
            {
                if(GetNode(node).F < lowestF)
                {
                    lowestF = GetNode(node).F;
                    lowestFindex = node;
                }
            }
            currentNode = lowestFindex;
            GetNode(currentNode).state = Node.State.Closed;
            openNodes.Remove(currentNode);

            foreach (var direction in directions)
            {
                Vector2Int adjacent = currentNode + direction;
                if (adjacent.x < 0 || adjacent.y < 0 || adjacent.x >= GridWidth || adjacent.y >= GridHeight)
                {
                    continue;
                }
                Node adjacentNode = GetNode(adjacent);
                int newCost = GetNode(currentNode).G + GetNode(adjacent).C;
                if (adjacentNode.Wall == true)
                {
                    continue;
                }
                else if (adjacentNode.state == Node.State.Closed)
                {
                    continue;
                }
                else if (adjacentNode.state == Node.State.Open)
                {
                    if(adjacentNode.G > newCost)
                    {
                        adjacentNode.G = newCost;
                        adjacentNode.H = Mathf.Abs(adjacent.x - end.x) + Mathf.Abs(adjacent.y - end.y);
                        adjacentNode.F = adjacentNode.G + adjacentNode.H;
                        adjacentNode.Parent = currentNode;
                        adjacentNode.state = Node.State.Open;
                        openNodes.Add(adjacent);
                    }
                }
                else
                {
                    adjacentNode.G = newCost;
                    adjacentNode.H = Mathf.Abs(adjacent.x - end.x) + Mathf.Abs(adjacent.y - end.y);
                    adjacentNode.F = adjacentNode.G + adjacentNode.H;
                    adjacentNode.Parent = currentNode;
                    adjacentNode.state = Node.State.Open;
                    openNodes.Add(adjacent);
                }
            }
            if(GetNode(end).state == Node.State.Closed)
            {
                List<Vector2Int> path = new List<Vector2Int>();
                Vector2Int backtrack = end;
                while(backtrack.x != -1)
                {
                    path.Add(backtrack);
                    backtrack = GetNode(backtrack).Parent;

                }
                path.Add(start);

                //for (int y = 0; y < GridHeight; ++y)
                //{
                //    for (int x = 0; x < GridWidth; ++x)
                //    {
                //        if (GetNode(new Vector2Int(x, y)).state == Node.State.None && !GetNode(new Vector2Int(x, y)).Wall)
                //        {
                //            tex.SetPixel(x, y, Color.black);
                //        }
                //        if (GetNode(new Vector2Int(x, y)).state == Node.State.Open)
                //        {
                //            tex.SetPixel(x, y, Color.blue);
                //        }
                //        if (GetNode(new Vector2Int(x, y)).state == Node.State.Closed)
                //        {
                //            tex.SetPixel(x, y, Color.yellow * (GetNode(new Vector2Int(x, y)).G/24.0f));
                //        }

                //    }
                //}
                //tex.Apply();
                return path;
            }
            
        }

        // Return empty path
        return new List<Vector2Int>();

    }
}
