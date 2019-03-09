using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bella_color : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private int numberOfObject1;
    [SerializeField] private int numberOfObject2;
    [SerializeField] private Texture2D myText;
    private Color[] myColors;
    public List<Color> result;
    private Terrain myTerrain;
    private TerrainData myTerrainData;
    private List<Vector3> takenCoordinates = new List<Vector3>();

    public bool IsSame(Color c1, Color c2)
    {   // if a color's h(hue) value has the same number in the position to the right of decimal point
        // ex: color 1's hue:0.2, color2's hue:0.27. Function return true
        // color3's hue : 0.3, color 4's hue: 0.5. Function return false
        float H1, S1, V1;
        float H2, S2, V2;
        Color.RGBToHSV(c1, out H1, out S1, out V1);
        Color.RGBToHSV(c2, out H2, out S2, out V2);

        if (Math.Floor(H1 * 10) != Math.Floor(H2 * 10))
        {
            return false;
        }
        return true;
    }
    public bool Find(Color c, List<Color> colors)
    {
        // Find if Color s is in the colors list
        foreach (Color color in colors)
        {
            if (IsSame(c, color))
            {
                return true;
            }
        }
        return false;
    }
    public List<Color> GetColorList(Color[] allColors)
    {
        result = new List<Color>();

        foreach (Color color in allColors)
        {
            if (!Find(color, result))
            {
                result.Add(color);
            }
        }
        return result;
    }

    public GameObject AllocateObject(GameObject prefab, Color c)
    {
        //x, z are the coordinates of the top left corner of myTerrain
        float x = myTerrain.GetPosition().x;
        float z = myTerrain.GetPosition().z;
        float width = myTerrainData.size.x; //width of myTerrain
        float length = myTerrainData.size.z;  //length of myTerrain

        for (int i = 0; i < width * length; i++)
        {
            float sampleX = UnityEngine.Random.Range(x, x + width); //x coordinates of my random sample point
            float sampleZ = UnityEngine.Random.Range(z, z + length);
            float sampleY = myTerrainData.GetHeight((int)(sampleX), (int)(sampleZ));
            Color samplePointColor = myText.GetPixel((int)(sampleX), (int)(sampleZ));
            Vector3 samplePoint = new Vector3(sampleX, sampleY, sampleZ);

            if (IsSame(c, samplePointColor) && takenCoordinates.IndexOf(samplePoint) == -1)
            {
                takenCoordinates.Add(samplePoint);
                return GameObject.Instantiate(prefab, samplePoint, Quaternion.identity);


            }

        }
        return null;
    }

    public void AllocateObjects(GameObject prefab, Color c, int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            AllocateObject(prefab, c);
        }
    }

    void Start()
    {
        myTerrain = Terrain.activeTerrain;
        myTerrainData = Terrain.activeTerrain.terrainData;

        //GetPixels get an Color[] of the Color of all Pixels in the Texture.
        //myText is a public property. When run, assign your png to myText by using the inspector
        myColors = myText.GetPixels();
        List<Color> result = GetColorList(myColors);

        //This loop is for debugging, to print the hue of the list of color I have from GetColorList
        //foreach (Color c in result)
        //{
        //    Color.RGBToHSV(c, out float H1, out float S1, out float V1);
        //    Debug.Log(H1);
        // }
        float H2, S2, V2;
        Color.RGBToHSV(result[2], out H2, out S2, out V2);
        AllocateObjects(object1, result[0], numberOfObject1);
        AllocateObjects(object2, result[1], numberOfObject2);
    }
}