using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;
using System.IO;

public class achievement : MonoBehaviour
{
    public int id;
    public Text title;
    public Text text;
    public Slider slider;
    public float progress;
    public float maxValue;
    public achievements achievements;
    public GameObject complete , locked;
    public bool achievementComplete = false;
    public float reward;

    public void AchievementUpdate()
    {
        maxValue = slider.maxValue;
        if (progress < maxValue)
        {
            slider.value = progress;
            text.text = "" + progress + " / " + maxValue;
        }
        if (achievementComplete)
        {
            slider.value = maxValue;
            text.text = "" + maxValue + " / " + maxValue;
            complete.SetActive(true);
            locked.SetActive(false);
        }
        if (progress >= maxValue && !achievementComplete)
        {
            achievements.AchievementUnlocked(id, reward);
        }
    }

    public void go()
    {
        achievements = FindObjectOfType<achievements>();
    }


}
