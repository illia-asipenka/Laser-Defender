using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static int playerPoints;
    public static int playerFinalPoints;
    private GameSession _gameSession;
    private static TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        _gameSession = FindObjectOfType<GameSession>();
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        scoreText.text = _gameSession.GetScore().ToString();
    }

    public static void AddPoints (int points)
    {
        playerPoints += points;
        scoreText.text = playerPoints.ToString();
    }

    public static void Reset()
    {
        playerPoints = 0;
        scoreText.text = playerPoints.ToString();
    }

}
