using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    public BlockManager bm;
    private float y,x;

    void Start()
    {
        bm = GameObject.FindObjectOfType(typeof(BlockManager)) as BlockManager;
        y = bm.takeY();
        x = Random.Range(1.5f, 0.5f);
        transform.localScale = new Vector3(x, x, 1);
        transform.position = new Vector3(Random.Range(-8, 8), Random.Range(y, y + 3), transform.position.z);
        y = transform.position.y;

    }

}