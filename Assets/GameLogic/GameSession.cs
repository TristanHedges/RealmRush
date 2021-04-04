using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameSession : MonoBehaviour
{
    [SerializeField] int maxLives, startingGold;
    [SerializeField] TextMeshProUGUI livesText, goldText;

    int currentLives, currentGold;
    public int CurrentGold { get { return currentGold; } }

    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();

        currentLives = maxLives;
        currentGold = startingGold;

        UpdateLivesText();
        UpdateGoldText();
    }

    void CheckIfOutOfLives()
    {
        if(currentLives <= 0)
        {
            sceneLoader.LoadLoseMenu();
        }
    }

    void UpdateGoldText()
    {
        goldText.text = $"Gold: {currentGold}";
    }

    void UpdateLivesText()
    {
        livesText.text = $"Lives: {currentLives}";
    }

    public void ReduceLives()
    {
        currentLives--;
        UpdateLivesText();
        CheckIfOutOfLives();
    }

    public void AddToCurrentGold(int amount)
    {
        currentGold += Mathf.Abs(amount);
        UpdateGoldText();
    }

    public void ReduceCurrentGold(int amount)
    {
        currentGold -= Mathf.Abs(amount);
        UpdateGoldText();
    }
    
}
