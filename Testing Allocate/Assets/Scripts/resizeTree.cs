using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.TerrainModule;
public class resizeTree : MonoBehaviour
{
    [Range(1.0f, 30f)]
    [SerializeField]private float precip = 5f;
    [SerializeField]private float maxSize = 10f;
    [SerializeField]private GameObject rainDrop;
    [SerializeField]private float initialGrowRate  = 0.02f;
    [SerializeField]private float fastGrowRate = 0.1f;
    public float growRate;
    [SerializeField]private List<Transform> trees;

    public void SetRain(){
        rainDrop.SetActive(precip > 10f);
    }

    //trees.Add(newTree);
    Vector3 rate = new Vector3(0, 0.005f, 0);
    // Start is called before the first frame update
    void Awake()
    {
        SetRain();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(precip > 10f)
        {
            growRate = fastGrowRate;
        } else
        {
            growRate = initialGrowRate;
        }
        foreach(Transform t in trees){
            float current = t.localScale.x;
            if(current < maxSize){
                
                current += growRate * Time.deltaTime;
            }
            
            t.localScale = Vector3.one * current;
        
        }
        SetRain();
    }
}
