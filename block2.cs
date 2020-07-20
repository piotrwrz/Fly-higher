using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block2 : MonoBehaviour
{
    public BlockManager bm;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.FindObjectOfType(typeof(BlockManager)) as BlockManager;
        y = bm.takeY2();
        transform.localScale = new Vector3(0.2f, Random.Range(0.7f, 1.35f), 1);
        transform.position = new Vector3(2.77f, Random.Range(y + 2, y + 3), transform.position.z);
        y = transform.position.y;
        bm.setY2(y);
    }

}
