using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject blockPrefab2;
    public GameObject starPrefab;
    public GameObject starPrefab2;
    public static bool spawnBlocks = true;
    private float x,z;

    void Start()
    {
        StartCoroutine(SpawnBlocks());
        x = -6;
        z = -6;
    }

    public void setY(float y)
    {
        x = y; 
    }

    public float takeY()
    {
        return x;
    }

    public void setY2(float y)
    {
        z = y;
    }

    public float takeY2()
    {
        return z;
    }

    public void SpwanBlocksOff()
    {
        StopAllCoroutines();
    }
    public void SpwanBlocksOn()
    {
        StartCoroutine(SpawnBlocks());
    }

    IEnumerator SpawnBlocks()
    {
        while(true)
        {
            while (spawnBlocks)
            {
                Instantiate(blockPrefab);
                Instantiate(blockPrefab2);
                Instantiate(starPrefab);
                Instantiate(starPrefab2);
                Instantiate(starPrefab2);
                yield return new WaitForSeconds(0);
            }
            
        }
    }

}
