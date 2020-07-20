using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam2 : MonoBehaviour
{
    private player player;
    public bool rdy;
    private float time;
    private Rigidbody2D rb;
    public float width, height;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rdy = true;
        player = GameObject.FindObjectOfType(typeof(player)) as player;
    }

    // Update is called once per frame
    void Update()
    {
        width = Screen.width;
        height = Screen.height;
        if (width/height < 0.562f && width / height > 0.47f)
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
        time += Time.deltaTime;
        rdy = player.ST();
        if (!rdy)
            rb.velocity = new Vector2(0, 3 + (time / 20));
        if(rdy)
            rb.velocity = new Vector2(0, 0);

        if (player.pos().y > transform.position.y + 2)
        {
            transform.position = new Vector3(0, player.pos().y - 2, transform.position.z);
        }
        if (player.pos().y <= transform.position.y - 5 || player.pos().y >= transform.position.y + 5)
        {
            player.dead();
        }

    }

}
