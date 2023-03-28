using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldSpawner : MonoBehaviour
{
    [SerializeField] protected Glyph currentGlpyh;
    [SerializeField] protected TextAsset currentLevel;
    [SerializeField] protected Transform floorparent;

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
        int layer = 0;
        int z = 0;
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
                    layer++;
                    z = 0;
                    Debug.Log("Hello Clay");
                }

                else if(mychars[i] == '1')
                {
                    currentGlpyh.SpawnGlyphItem('1', new Vector3(layer, 0, z), 0, '.', floorparent);
                    z++;
                    Debug.Log("Hello Bailey");
                }
                else if (mychars[i] == '2')
                {
                    currentGlpyh.SpawnGlyphItem('2', new Vector3(layer, 0, z), 0, '.', floorparent);
                    z++;
                    Debug.Log("Hello Matt!");
                }
            }
        }



    }

}
