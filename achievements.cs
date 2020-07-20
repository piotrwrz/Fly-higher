using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class achievements : MonoBehaviour
{
    public achievement[] achievement;
    public bool[] completed;
    public player player;
    public Animator[] animator;
    public float l;
    public bool turb = true;
    public int turbCount;
    public float coins;
    public Text coinsText;

    void Start()
    {
        l = achievement.Length;
        string path = Application.persistentDataPath + "/AchievementsData.xd";
        if(File.Exists(path))
        {
            LoadAchievementData();
        }
        for(int i = 0; i < l; i++)
        {
            achievement[i].go();
            if (completed[i])
            {
                achievement[19].progress ++;                             //////////////END?
            }
        }

    }

    void Update()
    {
        coinsText.text = "" + coins + "$";
        ///Games Played
        achievement[0].progress = achievement[1].progress = achievement[2].progress = achievement[3].progress = player.gamesPlayed;

        ///Score Easy
        achievement[4].progress = achievement[5].progress = achievement[12].progress = player.highScoreEasy;
        ///Score Normal
        achievement[6].progress = achievement[10].progress = achievement[13].progress = player.highScore;
        ///Score Hard
        achievement[7].progress = achievement[11].progress = achievement[14].progress = player.highScoreHard;

        ///Turbulences
        if (player.turbulencesAchievement)
        {
            if(turb)
            {
                turbCount++;
                turb = false;
            }
        }
        achievement[9].progress = achievement[17].progress = turbCount;

        ///Stabilized
        if (player.stabilizedAchievement)
        {
            achievement[15].progress = 1;
        }

        ///Colors
        if(player.colorAchievement)
        {
            achievement[16].progress = 1;
        }
        if (player.color2Achievement)
        {
            achievement[18].progress = 1;
        }

        ///Space
        if (player.hsEasy() > 80 || player.hs() > 75 || player.hsHard() > 70)
        {
            achievement[8].progress = 1;
        }



        for (int i = 0; i < achievement.Length; i++)
        {
            achievement[i].achievementComplete = completed[i];
            achievement[i].AchievementUpdate();



        }
        save.SaveAchievementData(this);
    }

    public void AchievementUnlocked(int i, float j)
    {
        animator[i].SetBool("achievement", true);
        completed[i] = true;
        coins += j;
        achievement[19].progress++;
    }


    public void LoadAchievementData()
    {
        string path = Application.persistentDataPath + "/AchievementsData.xd";
        if (File.Exists(path))
        {
            data data = save.LoadAchievementData();
            for (int i = 0; i < l; i++)
            {
                completed[i] = data.achievementComplete[i];
                turbCount = data.turbCount;
                coins = data.coins;
            }
        }
    }
}
