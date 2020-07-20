using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class player : MonoBehaviour
{
    private GameObject[] blockList;
    public GameObject wallL, wallR , buttonMute, buttonUnmute, adRemoveButton , adRemovedButton;
    public Renderer rend, rend2, wallLRend, wallRRend;
    private BlockManager bm;
    private Rigidbody2D rb;
    private float speed, time, time1 , time2, startTime, los;
    public float score, highScore, highScoreHard, highScoreEasy,x, gamesPlayed;
    private float y,y2;
    public bool alive , alredyDead , muted;
    private bool st = true;
    public Collider2D collid;
    public LayerMask deadLayers,goodLayers;
    private bool redDead = false;
    private Vector4 color, color1;
    public Text text;
    public AudioSource sound, GOsound, EngineSound;
    public over gom;
    private bool stabilize;
    public Animator animator;
    public GameObject stabilizeButton, scoreText, HTF , startMenu, changeDifficulty;
    public float spd;
    private Scene scene;
    public Material material;
    public float adCount;
    public bool adRemove = false;
    public bool stabilizedAchievement = false;
    public bool turbulencesAchievement = false;
    public bool colorAchievement = false;
    public bool color2Achievement = false;
    public int swaps;
    public GameObject coinPrefab;
    public bool coinsSet = true;
    public GameObject coinsON, coinsOFF;
    public int helpCount;
    public GameObject helpText;


    void Start()
    {
        string path = Application.persistentDataPath + "/PlayerData.xd";
        Advertisement.Initialize("3665405", false);
        if (File.Exists(path))
            {
                LoadData();
            }
        bm = GameObject.FindObjectOfType(typeof(BlockManager)) as BlockManager;
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        collid = GetComponent<Collider2D>();
        alive = true;
        wallLRend = wallL.GetComponent<Renderer>();
        wallRRend = wallR.GetComponent<Renderer>();
        stabilize = false;
        alredyDead = false;
        startTime = 0;
        score = -4;
        color = Color.white;
        color1 = new Color(0.4528302f, 0, 0.4017969f);
        rend.material.color = color1;
        scene = SceneManager.GetActiveScene();
        adCount++;
        if (adRemove == true)
        {
            adRemoveButton.SetActive(false);
            adRemovedButton.SetActive(true);
        }

        if(helpCount >= 2)
        {
            helpText.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (coinsSet == false)
        {
            coinsON.SetActive(false);
            coinsOFF.SetActive(true);
        }
        if (muted)
        {
            EngineSound.mute = true;
            GOsound.mute = true;
            sound.mute = true;
            buttonMute.SetActive(false);
            buttonUnmute.SetActive(true);
        }
        if (!muted)
        {
            GOsound.mute = false;
            sound.mute = false;
            buttonMute.SetActive(true);
            buttonUnmute.SetActive(false);
        }
            ///STABILITY
            if (transform.rotation.z > 0.04f || transform.rotation.z < -0.04f && alive == true)
        {
            stabilizeButton.SetActive(true);
            turbulencesAchievement = true;

        }
        if(alive == false)
        {
            stabilizeButton.SetActive(false);
        }
        if (transform.rotation.z < 0.3f && transform.rotation.z > -0.3f && stabilize == true)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            stabilizeButton.SetActive(false);
            rb.freezeRotation = true;
            stabilizedAchievement = true;

        }
        checkBlocks();
        /// ANIMATOR
        if(Input.GetMouseButton(0))
        {
            startTime += Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && alive == true && startTime > 0.15f)
        {
            if (!muted && st == false && alredyDead == false)
            {
                EngineSound.mute = false;
            }
            animator.SetBool("fly", true);
            if (st == true)
            {
                scoreText.SetActive(true);
                rb.simulated = true;
                rb.velocity = new Vector2(4, 6);
                st = false;
                HTF.SetActive(false);
                startMenu.SetActive(false);
                changeDifficulty.SetActive(false);
                if(coinsSet)
                {
                    Instantiate(coinPrefab);
                }
            }
        }
        if (!Input.GetMouseButton(0))
        {
            animator.SetBool("fly", false);
            EngineSound.mute = true;
            if(st)
            startTime = 0;
        }


        ////SCORE
        if (score - 3 < transform.position.y)
        {
            score = (float)System.Math.Round(transform.position.y + 3, 0);
        }
        text.text = ""+score;
        if (scene.name == "main")
        {
            if (score > highScore)
            {
                highScore = score;
            }
        }
        if (scene.name == "mode 2")
        {
            if (score > highScoreHard)
            {
                highScoreHard = score;
            }
        }
        if (scene.name == "mode 1")
        {
            if (score > highScoreEasy)
            {
                highScoreEasy = score;
            }
        }


        ////LEVELS
        if (transform.position.y > 20)
        {
            color = new Color(0.1814134f, 0.4056604f, 0);
            color1 = Color.yellow;
        }
        if (transform.position.y > 50)
        {
            color = Color.red;
            color1 = Color.cyan;
        }
        if (transform.position.y > 90)
        {
            color = Color.blue;
            color1 = new Color(1, 0.5448123f, 0);
        }
        if (transform.position.y > 140)
        {
            color = Color.red;
            color1 = Color.green;
        }
        ///GOM Material
        if (transform.position.y <= 70)
        {
            Color color = new Color(0, 0, 0);
            color.a = 0.69f;
            material.color = color;
        }
        if (transform.position.y > 70)
        {
            Color color = new Color(0.5f, 0.5f, 0.5f);
            color.a = 0.69f;
            material.color = color;
        }
        if (transform.position.y > 60 && scene.name == "mode 2")
        {
            Color color = new Color(0.5f, 0.5f, 0.5f);
            color.a = 0.69f;
            material.color = color;
        }
        ///color swap
        if (collid.IsTouchingLayers(goodLayers) && redDead == false && !collid.IsTouchingLayers(deadLayers))
        {
            if (time == 0)
            {
                sound.Play();
            }
            time += Time.deltaTime;
            los = Random.Range(1, 100);
            if(los > 50)
            {
                if (swaps == 0 && transform.position.y > 20)              ///////////COLOR ACHIEVEMENT
                {
                    colorAchievement = true;
                }
                if (swaps < 2 && transform.position.y > 50)
                {
                    color2Achievement = true;
                }
                redDead = true;
                rend.material.color = color;
                p1swap();
                swaps++;

            }
        }
        if(!collid.IsTouchingLayers(goodLayers))
        {
            time = 0;
        }
        if (collid.IsTouchingLayers(deadLayers) && redDead == true)
        {
            if (time1 == 0)
            {
                sound.Play();
            }
            time1 += Time.deltaTime;
            los = Random.Range(1, 100);
            if (los > 50)
            {
                if (swaps == 0 && transform.position.y > 20)                ///////////COLOR ACHIEVEMENT
                {
                    colorAchievement = true;
                }
                if (swaps < 2 && transform.position.y > 50)
                {
                    color2Achievement = true;
                }
                redDead = false;
                rend.material.color = color1;
                p1swap();
                swaps++;
            }
        }
        if (!collid.IsTouchingLayers(deadLayers))
        {
            time1 = 0;
        }



        ///dead
        if (collid.IsTouchingLayers(goodLayers) && redDead == true && time == 0 && !collid.IsTouchingLayers(deadLayers))
        {
            dead();
        }
        if (collid.IsTouchingLayers(deadLayers) && redDead == false && time1 == 0 && time2 > 0.1f)
        {
            dead();
        }


        /// speed
        if (scene.name == "main")/////////////////////////// MAIN SPEED
        {
            x = rb.velocity.x;
            if (st == false)
            {
                time2 += Time.deltaTime;
            }
            if (rb.velocity.x >= 0)
            {
                speed = spd;
                if (Input.GetMouseButton(0) && alive == true)
                {
                    rb.velocity = new Vector2(speed + transform.position.y / 100, 6);
                }
            }
            if (rb.velocity.x < 0)
            {
                speed = -spd;
                if (Input.GetMouseButton(0) && alive == true)
                {
                    rb.velocity = new Vector2(speed - transform.position.y / 100, 6);
                }
            }
        }
        if (scene.name == "mode 1")/////////////////////////// EASY SPEED
        {
            x = rb.velocity.x;
            if (st == false)
            {
                time2 += Time.deltaTime;
            }
            if (rb.velocity.x >= 0)
            {
                speed = spd;
                if (Input.GetMouseButton(0) && alive == true)
                {
                    rb.velocity = new Vector2(speed + transform.position.y / 200, 5);
                }
            }
            if (rb.velocity.x < 0)
            {
                speed = -spd;
                if (Input.GetMouseButton(0) && alive == true)
                {
                    rb.velocity = new Vector2(speed - transform.position.y / 200, 5);
                }
            }
        }
        if (scene.name == "mode 2") /////////////////////////// HARD SPEED
        {
            x = rb.velocity.x;
            if (st == false)
            {
                time2 += Time.deltaTime;
            }
            if (rb.velocity.x >= 0)
            {
                speed = spd;
                if (Input.GetMouseButton(0) && alive == true)
                {
                    rb.velocity = new Vector2( speed + transform.position.y / 100, 6 + ( time2 / 20 ));
                }
            }
            if (rb.velocity.x < 0)
            {
                speed = -spd;
                if (Input.GetMouseButton(0) && alive == true)
                {
                    rb.velocity = new Vector2( speed - transform.position.y / 100, 6 + (time2 / 20));
                }
            }
        }
        


        ///block manager
        y = bm.takeY();
        y2 = bm.takeY2();
        if ( Mathf.Abs(transform.position.y - y) > 70 && Mathf.Abs(transform.position.y - y2) > 70 )
        {
            bm.SpwanBlocksOff();
        }
        if (Mathf.Abs(transform.position.y - y) <= 70 || Mathf.Abs(transform.position.y - y2) <= 70)
        {
            bm.SpwanBlocksOn();
        }

    }

    public void dead()
    {
        if (!alredyDead)
        {
            gamesPlayed++;
            ShowAd();
            rb.simulated = false;
            alive = false;
            gom.overMenu();
            animator.SetBool("dead", true);
            GOsound.Play();
            stabilizeButton.SetActive(false);
            st = true;
            alredyDead = true;
            HelpCheck();
            save.SaveData(this);
        }
    }

    public float hs()
    {
        return highScore;
    }
    public float hsHard()
    {
        return highScoreHard;
    }
    public float hsEasy()
    {
        return highScoreEasy;
    }
    public bool ST()
    {
        return st;
    }

    public Vector3 pos()
    {
        return transform.position;
    }

    void checkBlocks()
    {
        blockList = GameObject.FindGameObjectsWithTag("block");
    }

    void p1swap()
    {
        for (int i = 0; i < blockList.Length; i++)
        {
            rend2 = blockList[i].GetComponent<Renderer>();
            rend2.material.color = color;
        }
        wallLRend.material.color = color1;
        wallRRend.material.color = color1;
    }

    public void MuteSounds()
    {
        if (!muted)
        {
            EngineSound.mute = true;
            GOsound.mute = true;
            sound.mute = true;
            muted = true;
            return;
        }
        if (muted)
        {
            GOsound.mute = false;
            sound.mute = false;
            muted = false;
            return;
        }

    }

    public void Stabilize()
    {
        stabilize = true;
    }

    public void ChangeToHard()
    {
        save.SaveData(this);
        SceneManager.LoadScene(1);
    }
    public void ChangeToNormal()
    {
        save.SaveData(this);
        SceneManager.LoadScene(0);
    }
    public void ChangeToEasy()
    {
        save.SaveData(this);
        SceneManager.LoadScene(2);
    }
    public void ChangeToShop()
    {
        save.SaveData(this);
        SceneManager.LoadScene(3);
    }
    public void CoinsSetON()
    {
        coinsSet = true;
    }
    public void CoinsSetOFF()
    {
        coinsSet = false;
    }

    public void HelpCheck()
    {
        if(score <= 20)
        {
            helpCount++;
        }
        if(score > 20)
        {
            helpCount = 0;
        }
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData.xd";
        if (File.Exists(path))
        {
            data data = save.LoadData();

            highScore = data.highScore;
            highScoreHard = data.highScoreHard;
            highScoreEasy = data.highScoreEasy;
            muted = data.mute;
            adCount = data.adCount;
            adRemove = data.adRemove;
            gamesPlayed = data.gamesPlayed;
            coinsSet = data.coinsSet;
            helpCount = data.helpCount;
        }
    }

    public void ShowAd() /////////////////////////AD
    {
        if (Advertisement.IsReady("video") && adCount > 10 && adRemove == false)
        {
            Advertisement.Show("video");
            adCount = 0;
        }
    }

    public void AdRemove()
    {
        adRemove = true;
    }

    void OnApplicationQuit()
    {
        save.SaveData(this);
    }

}
