using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Vector3 posCam;
    public Vector3 posPla;
    public player player;
    public Camera kamera;
    public float width, height;
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(player)) as player;
    }

    // Update is called once per frame
    void Update()
    {
        width = Screen.width;
        height = Screen.height;
        if (width / height < 0.562f && width / height > 0.47f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        if (width / height < 0.595f && width / height > 0.562f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -8.84f);
        }
        if (width / height > 0.595f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -8.25f);
        }
        if (width / height < 0.47f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -11);
        }
        posCam = transform.position;
        posPla = player.pos();
        if(posPla.y > posCam.y)
        {
            transform.position = new Vector3(0, posPla.y, transform.position.z);
        }
        if (posPla.y <= posCam.y - 5)
        {
            player.dead();
        }

    }
}
