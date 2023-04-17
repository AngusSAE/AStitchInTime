using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Textfuie : MonoBehaviour
{
    public Glyph SelectedGlyph;
    public StreamReader sr;
    // Start is called before the first frame update
    private void Start()
    {
        string text = File.ReadAllText("Assets/Levels/Level01.txt");
        //Debug.Log(text);
        string[] textArray = text.Split("\n");
        int layer = 0;
       // int y = 0;
        List<string> layerStrings = new List<string>();

        for (int i = 0; i < textArray.Length; i++)
        {
            string line = textArray[i].Trim();

            if (line.Length > 0)
            {
                if (line[0] == '/')
                {
                    //do Nothing
                }

                else if (line[0] == '*')
                {
                    
                    Debug.Log(string.Format("End of Layer {0}", layer));

                    for(int y = 0; y < layerStrings.Count/2; y++)
                    {
                        string rowTile = layerStrings[y];
                        string rowRot = layerStrings[y+layerStrings.Count/2];
                        for (int x = 0; x < rowTile.Length; ++x) ;
                        //SelectedGlyph.SpawnGlyphItem(rowTile[x], new Vector3(x, 0, y), layer, rowRot[x]);

                        
                    }
                    
                    layerStrings.Clear();

                    layer++;
                    //y = 0;

                }
                else
                {
                    layerStrings.Add(line);
                    //for (int e = 0; e < line.Length; e++)
                    //{
                    //    if (line[e] == '1')
                    //    {
                    //        SelectedGlyph.SpawnGlyphItem(line[e], new Vector3(e, 0, y), layer, line[e]);
                    //    }

                    //}
                    //Debug.Log(string.Format("{1} : {0} : length = {2}", y, line, line.Length));
                  //  y++;
                }

            }
        }
    }
}
