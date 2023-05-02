using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<Vector2Int> Path = new List<Vector2Int>();
    public List<Vector2Int> Path2 = new List<Vector2Int>();


    void Update()
    {
        Vector2Int pos = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        for(int i=0;i<Path2.Count-1;++i)
        {
            Debug.DrawLine(
                new Vector3(Path2[i].x + 0.5f, 0.1f, Path2[i].y + 0.5f),
                new Vector3(Path2[i + 1].x + 0.5f, 0.1f, Path2[i + 1].y + 0.5f),
                Color.green);
        }

        if (Path.Count == 0)
        {
            Vector2Int newtarget = new Vector2Int();
            do
            {
                newtarget.x = Random.Range(1, Pathfind.GridWidth - 1);
                newtarget.y = Random.Range(1, Pathfind.GridHeight - 1);
            } while (Pathfind.GetNode(newtarget).Wall);

            Path = Pathfind.FindPath(
                new Vector2Int((int)transform.position.x, (int)transform.position.z),
                newtarget);
            Path2.Clear();
            foreach(var node in Path)
            { 
                Path2.Add(node);
            }
            
        }
        if (Path.Count != 0)
        {
            Vector3 target = new Vector3(
                Path[Path.Count - 1].x + Pathfind.CellSize * 0.5f, 
                transform.position.y, 
                Path[Path.Count - 1].y + Pathfind.CellSize * 0.5f);
            GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * 8.0f;
            Debug.LogError("moving");
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                Path.RemoveAt(Path.Count - 1);
            }
        }
    }
}
