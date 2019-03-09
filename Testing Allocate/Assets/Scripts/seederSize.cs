using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class seederSize : MonoBehaviour
{
    private float size;
    // Start is called before the first frame update
    public string[] ReadArray(string fileName)
    {
        string path = fileName + ".txt";
        string contents = File.ReadAllText(path);
        string[] s = contents.Split('\n');
        return s;
    }
    /*
    void Start()
    {
        string[] sSizes = ReadArray(seeder);
        for (int i = 0; i < sSizes.Length; i++)
        {
            size = sSizes[i];
            localScale.x = size;
            localScale = Vector3.one * size;

        }
    }
    */
}
