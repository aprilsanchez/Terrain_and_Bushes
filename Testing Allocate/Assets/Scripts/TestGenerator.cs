using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TestGenerator : MonoBehaviour
{

    public Texture2D myTexture;
    private Terrain myTerrain;
    private TerrainData myTerrainData;

    // Start is called before the first frame update
    void Start()
    {
        myTerrain = Terrain.activeTerrain;
        myTerrainData = Terrain.activeTerrain.terrainData;
        //List<TreeInstance> treeList = new List<TreeInstance>();
        //go through myColors[], if pixel is green, instantiate a bush
        // note: myColors is a 2D array and the index corresponds to the piposition of the pixel in the texture


        float width = myTerrainData.size.x;
        float height = myTerrainData.size.z;

        Color green = new Color(0, 1, 0, 1);

        for (int j = 0; j < height; j++)
        {
            for (int k = 0; k < width; k++)
            {
                Color c = myTexture.GetPixel(j, k);
                string f = "false";
                

                Debug.Log("this color is green:" + f);             
                Debug.Log("compared colors");
                if (myTerrain.GetComponent<color>().IsSameColor(c, green))
                {
                    f = "true";
                    Debug.Log("in add tree loop");
                    addTreeToTerrain(j, k);
                    myTerrain.Flush();
                    
                }
                Debug.Log("this color is green:" + f);
            }
        }
    }

    void addTreeToTerrain(int j, int k)
    {
        Vector3 p = ( new Vector3((float) j/(float) myTexture.height, 0, (float) k/(float) myTexture.width));
        Debug.Log(p);
        TreeInstance tree = new TreeInstance();
        tree.prototypeIndex = 0;
        tree.position = p;
        float x = gameObject.GetComponent<Size>().sizes[0];
        float max = gameObject.GetComponent<Size>().sizes.Max();
        x = x * 2.25f / max; //x is heightScale for first tree
        tree.heightScale = x;
        tree.widthScale = x;
        tree.color = Color.white;
        tree.lightmapColor = Color.white;

        myTerrain.AddTreeInstance(tree);
    }
}
