using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldSpawner : MonoBehaviour
{
    [SerializeField] protected Glyph currentGlpyh;
    [SerializeField] protected TextAsset currentLevel;
    [SerializeField] protected GameObject floorParent;
    [SerializeField] protected GameObject objectParent;

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
        int x = 0;
        int z = 0;
        int layer = 0;
        char[] mychars = groundText.ToCharArray();

        for (int i=0; i < mychars.Length; i++)
        {
            
            if(mychars.Length > 0)
            {
                if(mychars[0] == '/')
                {
                    //do nothing
                    Debug.Log("Hello ME");
                }

                else if (mychars[i] == '*')
                {
                    x++;
                    z = 0;
                    Debug.Log("Hello Clay");
                }
                else if (mychars[i] == '.')
                {
                    z++;
                    Debug.Log("Hello Nathan");
                }

                else if(mychars[i] == '1')
                {
                    currentGlpyh.SpawnGlyphItem('1', new Vector3(x, 0, z), layer, '.', floorParent.transform);
                    z++;
                    Debug.Log("Hello Bailey");
                }
                else if (mychars[i] == '2')
                {
                    currentGlpyh.SpawnGlyphItem('2', new Vector3(x, 0, z), layer, '.', floorParent.transform);
                    z++;
                    Debug.Log("Hello Matt!");
                }
                else if (mychars[i] == '?')
                {
                    layer++;
                    z = 0;
                    x = 0;
                    Debug.Log("Hello Greg");
                }

                else if (mychars[i] == 't')
                {
                    currentGlpyh.SpawnGlyphItem('t', new Vector3(x, 0, z), layer, '.', objectParent.transform);
                    z++;
                    Debug.Log("Hello Nat");
                }
                else if (mychars[i] == 'p')
                {
                    currentGlpyh.SpawnGlyphItem('p', new Vector3(x, 0, z), layer, '.', objectParent.transform);
                    z++;
                    Debug.Log("Hello Bridget!");
                }
            }
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
