using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//generate 10 resprouters and seeders at random location of terrain
public class TreeGenerator : MonoBehaviour
{
    
    [SerializeField] public GameObject resprouterPrefab;
    [SerializeField] public GameObject seederPrefab;
    [SerializeField] private Size rs;
    [SerializeField] private Size ss;
    //[SerializeField] private int numberOfResprouters = 15;
    //[SerializeField] private int numberOfSeeder = 15;

    public Texture2D myTexture;
    private Terrain myTerrain;
    private TerrainData myTerrainData; 
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject tree;
        myTerrain = Terrain.activeTerrain;
        myTerrainData = Terrain.activeTerrain.terrainData;
        //GameObject thisTree;
        //go through myColors[], if pixel is green, instantiate a bush
        // note: myColors is a 2D array and the index corresponds to the piposition of the pixel in the texture


        //int width = (int) gameObject.GetComponent<Terrain>().terrainData.size.x;
        //int height = (int) gameObject.GetComponent<Terrain>().terrainData.size.z;
        float width = myTerrainData.size.x;
        float height = myTerrainData.size.z;

        Color green = new Color(0, 1, 0, 1);

        for (int j = 0; j < height; j++)
        {
            for (int k = 0; k < width; k++)
            {
                

                Color c = myTexture.GetPixel(j, k);

                if (  myTerrain.GetComponent<color>().IsSameColor(c, green) )
                {
                    // j = x position, k = z position, GetHeight = y position
                    // the 0.5 is to place the bush exactly in the middle of the square
                    //one bush takes up one square in Unity

                    //float x = gameObject.GetComponent<Transform>().position.x + j + 0.5f;
                    //float z = gameObject.GetComponent<Transform>().position.z + k + 0.5f;
                    //float x =  transform.localPosition.x + j + 0.5f;
                    //float z =  transform.localPosition.z + k + 0.5f;
                    //float y = gameObject.GetComponent<Terrain>().terrainData.GetHeight( (int) x, (int) z);
                    float x = myTerrain.GetPosition().x + j + 0.5f;
                    float z = myTerrain.GetPosition().z + k + 0.5f;
                    float y = myTerrain.SampleHeight( new Vector3( x, myTerrainData.GetHeight( (int) x, (int) z), z) );
                    //float y = gameObject.GetComponent<Terrain>().SampleHeight((int)x, (int) z);
                    Debug.Log("height is " + y);
                    Vector3 p = new Vector3(x , y, z);
                    tree = GenerateTree(resprouterPrefab, p);
                    //rs.AddTree(tree.transform);
                }
                if (gameObject.GetComponent<Terrain>().terrainData.GetHeight((int)j, (int)k) > 0)
                {
                    Debug.Log("coordinate: (" + j + ", " + gameObject.GetComponent<Terrain>().terrainData.GetHeight((int)j, (int)k) + ", " + k + ")");
                }
            }
           
        }
    }
    private GameObject GenerateTree(GameObject treePrefab, Vector3 p)
    {
        return GameObject.Instantiate(treePrefab, p, Quaternion.identity);
    }
    
}
