using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int score { get; private set; }
    float lastEnmyKillTime;
    int streakCount;
    float streakExpiryTime = 1;


    void Start()
    {
        Enemy.OnDeathStatic += OnEnemyKilled;
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }

    private void OnEnemyKilled()
    {
        if (Time.time < lastEnmyKillTime + streakExpiryTime)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }
        score += 5 + (int)Mathf.Pow(2, streakCount);
    }

    private void OnPlayerDeath()
    {
        Enemy.OnDeathStatic -= OnEnemyKilled;
    }

}
