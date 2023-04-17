using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldSpawner : MonoBehaviour
{
    [SerializeField] protected Glyph currentGlpyh;
    [SerializeField] protected TextAsset currentLevel;
    [SerializeField] protected GameObject floorParent;
    [SerializeField] protected GameObject objectParent;
    //[SerializeField] protected Pathfind paths;

    // Start is called before the first frame update
    void Start()
    {
        currentGlpyh.Setup();
        //currentGlpyh.SpawnGlyphItem('p', new Vector3(1, 0, 1),1, '>');   
        //currentGlpyh.SpawnGlyphItem('1', new Vector3(1, 0, 1),0, '.');   
        // currentGlpyh.SpawnGlyphItem('1', new Vector3(1, 0, 1),2, '.');  

        //print(currentLevel.text);

        string groundText = currentLevel.text;

        string[] groundArray = groundText.Split('\n');
        //int x = 0;
        int z = 0;
        int layer = 0;
        for (int line = 0; line < groundArray.Length; ++line)
        {
            char[] mychars = groundArray[line].ToCharArray();
            int width = (mychars.Length - 2) / 2;
            if (groundArray[line] == "?\r")
            {
                layer++;
                z = 0;
                continue;
            }
            for (int x = 0; x < width; x++)
            {
                char rotation = mychars[x+ width + 1]; 
                if (mychars.Length > 0)
                {
                    if (mychars[0] == '/')
                    {
                        break;
                        //do nothing
                        Debug.Log("Hello ME");
                    }

                    else if (mychars[x] == '*')
                    {
                        //x++;
                        //z = 0;
                        Debug.Log("Hello Clay");
                    }
                    else if (mychars[x] == '.')
                    {
                        //z++;
                        Debug.Log("Hello Nathan");
                    }

                    else if (mychars[x] == '1')
                    {
                        currentGlpyh.SpawnGlyphItem('1', new Vector3(x, 0, z), layer, rotation, floorParent.transform);
                        //z++;
                        Debug.Log("Hello Bailey");
                        
                    }
                    else if (mychars[x] == '2')
                    {
                        currentGlpyh.SpawnGlyphItem('2', new Vector3(x, 0, z), layer, rotation, floorParent.transform);
                        //z++;
                        //Debug.Log("Hello Matt!");
                        Debug.Log(x + " " + z);
                        Pathfind.SetWall(x, z, true);
                    }
                    else if (mychars[x] == '?')
                    {
                        layer++;
                        z = 0;
                        //x = 0;
                        Debug.Log("Hello Greg");
                    }

                    else if (mychars[x] == 't')
                    {
                        currentGlpyh.SpawnGlyphItem('t', new Vector3(x, 0, z), layer, rotation, objectParent.transform);
                        //z++;
                        Debug.Log("Hello Nat");
                        Pathfind.SetWall(x, z, true);
                    }
                    else if (mychars[x] == 'p')
                    {
                        currentGlpyh.SpawnGlyphItem('p', new Vector3(x, 0, z), layer, rotation, objectParent.transform);
                        //z++;
                        Debug.Log("Hello Bridget!");
                    }
                    else if (mychars[x] == 'n')
                    {
                        currentGlpyh.SpawnGlyphItem('n', new Vector3(x, 0, z), layer, rotation, objectParent.transform);
                        //z++;
                        Debug.Log("Hello Bridget!");
                    }
                }
            }
            z++;
        }


    }

    public void NoMap()
    {
        int objectChildren = objectParent.transform.childCount;
        int floorChildren = floorParent.transform.childCount;

        for (int i = objectChildren - 1; i >= 0; i--)
        {
            DestroyImmediate(objectParent.transform.GetChild(i).gameObject);

        }

        for (int i = floorChildren - 1; i >= 0; i--)
        {
            DestroyImmediate(floorParent.transform.GetChild(i).gameObject);

        }
    }
    public void SpawnAgain()
    {
        Start();
    }

}
