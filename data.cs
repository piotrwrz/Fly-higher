using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class data
{
    public float highScore;
    public float highScoreHard;
    public float highScoreEasy;
    public bool mute;
    public float adCount;
    public bool adRemove;
    public float gamesPlayed;
    public bool stabilizedAchievement;
    public bool[] achievementComplete;
    public int turbCount;
    public float coins;
    public bool coinsSet;
    public int helpCount;
    public data(player Player)
    {
        highScore = Player.highScore;
        highScoreHard = Player.highScoreHard;
        highScoreEasy = Player.highScoreEasy;
        mute = Player.muted;
        adCount = Player.adCount;
        adRemove = Player.adRemove;
        gamesPlayed = Player.gamesPlayed;
        coinsSet = Player.coinsSet;
        helpCount = Player.helpCount;
    }

    public data(achievements Achievement)
    {
        achievementComplete = new bool[Achievement.completed.Length];
        for (int i = 0; i < Achievement.l; i++)
        {
            achievementComplete[i] = Achievement.completed[i];
            turbCount = Achievement.turbCount;
        }
        coins = Achievement.coins;
    }
}
