using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    public achievements Achievements;
    public GameObject coinPrefab;
    public GameObject player;
    public player Player;
    public Animator anim;
    public CircleCollider2D circle;
    public SpriteRenderer sprite;
    public AudioSource sound;
    public int i;
 

    void Start()
    {
        Player = GameObject.FindObjectOfType(typeof(player)) as player;
        Achievements = GameObject.FindObjectOfType(typeof(achievements)) as achievements;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(Random.Range(-2.4f , 2.4f), Random.Range(player.transform.position.y + 6, player.transform.position.y + 9), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > transform.position.y + 5)
        {
            if(i == 0)
            {
                Instantiate(coinPrefab);
            }
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && i == 0)
        {
            Achievements.coins += 10;
            anim.SetBool("coin", true);
            sound.Play();
            Instantiate(coinPrefab);
            Destroy(circle);
            Destroy(sprite);
            i++;
        }
    }
}
