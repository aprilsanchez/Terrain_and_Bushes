using System.Collections;
using System.Collections.Generic;
using System.Linq; //for finding min
using UnityEngine;
using System.IO;

public class Size : MonoBehaviour
{
    private int index = 1;   // use to index my sizes list
    [SerializeField] private float growthTime = 1;    // how long to pause before reading the next value in sizes
    public string fileName;
    public List<float> sizes = new List<float>();

    Terrain myTerrain;

    private void Start()
    {
        myTerrain = Terrain.activeTerrain;
        //StartCoroutine(WaitThenChangeSize(10));
        sizes = GameFunctions.Read(fileName, "biomass");
        Debug.Log("Read sizes size is " + sizes.Count);
        StartCoroutine(WaitThenChangeSize(0));
    }

    IEnumerator WaitThenChangeSize(float time)
    {
        yield return new WaitForSeconds(time);


        for (int i = 1; i < myTerrain.terrainData.treeInstances.Length; i++)
        {
            TreeInstance t = myTerrain.terrainData.GetTreeInstance(i);
            float percentage = (sizes[index] - sizes[index - 1]) / sizes[index - 1];
            float x = myTerrain.terrainData.GetTreeInstance(i - 1).heightScale * (1 + percentage);
            //t.heightScale =  (sizes[index] / 1000f);
            //t.widthScale = (sizes[index] / 1000f);
            t.heightScale = x;
            t.widthScale = x;

            myTerrain.terrainData.SetTreeInstance(i, t);

            Debug.Log("scale size is now: " + t.heightScale);
        }
        index++;

        if (index >= sizes.Count - 1)
        {
            Debug.Break();
        }
        StartCoroutine(WaitThenChangeSize(growthTime));
    }

}
