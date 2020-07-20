using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class over : MonoBehaviour
{
    public Text highScore;
    public GameObject Menu;
    public Material material;
    public float hs;
    public player Player;
    private Scene scene;
    void Start()
    {

    }
    public void Update()
    {
        
    }
    public void overMenu()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "main")
            hs = Player.hs();
        if (scene.name == "mode 1")
            hs = Player.hsEasy();
        if (scene.name == "mode 2")
            hs = Player.hsHard();

        Menu.SetActive(true);
        highScore.text = "HIGH SCORE : " + hs ;
    }

    public void retry()
    {
        SceneManager.LoadScene(0);
    }

    public void retryMode2()
    {
        SceneManager.LoadScene(1);
    }

    public void retryMode1()
    {
        SceneManager.LoadScene(2);
    }



}
