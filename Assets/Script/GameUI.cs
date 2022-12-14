using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] public Image fadePlane;
    [SerializeField] GameObject gameOverUI;

    [SerializeField] RectTransform newWaveBanner;
    [SerializeField] Text newWaveTitle;
    [SerializeField] Text newWaveEnemyCount;
    [SerializeField] Text scoreUI;
    [SerializeField] Text gameOverScoreUI;
    [SerializeField] Transform healthBar;

    Spawner spawner;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnDeath += OnGameOver;

    }

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    private void Update()
    {
        scoreUI.text = ScoreKeeper.score.ToString("D6");
        float healthPercent = 0;
        if (player != null)
        {
            healthPercent = player.health / player.startingHealth;
        }
        healthBar.localScale = new Vector3(healthPercent, 1, 1);

    }

    private void OnNewWave(int waveNumber)
    {
        string[] number = { "One", "Two", "Three", "Four", "Five" };
        newWaveTitle.text = "- Wave " + number[waveNumber - 1] + " -";
        string enemyCountString = ((spawner.waves[waveNumber - 1].infinite ? "Infinite" : spawner.waves[waveNumber - 1].enemyCount + ""));
        newWaveEnemyCount.text = "Enemies: " + enemyCountString;


        StopCoroutine("AnimateNewWaveBanner");
        StartCoroutine("AnimateNewWaveBanner");
    }

    void OnGameOver()
    {
        Cursor.visible = true;
        StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .85f), 1));
        gameOverScoreUI.text = scoreUI.text;
        scoreUI.gameObject.SetActive(true);
        healthBar.transform.parent.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    private IEnumerator AnimateNewWaveBanner()
    {
        float deleyTime = 1.5f;
        float speed = 3f;
        float animatePercent = 0;
        int dir = 1;

        float endDelayTime = Time.time + 1 / speed + deleyTime;

        while (animatePercent >= 0)
        {
            animatePercent += Time.deltaTime * speed * dir;
            if (animatePercent >= 1)
            {
                animatePercent = 1;
                if (Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }
            newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-410, -180, animatePercent);
            yield return null;
        }
    }


    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    // UI Input
    public void StartNewGame()
    {
        SceneManager.LoadScene("GameSecen");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
