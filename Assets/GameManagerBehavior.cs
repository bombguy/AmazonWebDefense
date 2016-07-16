﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{

    public Text cashLabel;
    private int cash;
    public int Cash
    {
        get { return cash; }
        set
        {
            cash = value;
            cashLabel.GetComponent<Text>().text = "$" + cash;
        }
    }

    public Text scoreLabel;
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreLabel.GetComponent<Text>().text = "Score: " + score;
        }
    }

    public Text waveLabel;
    public GameObject[] nextWaveLabels;

    public bool gameOver = false;

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }

    public Text healthLabel;
    public GameObject[] healthIndicator;

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            // 1
            if (value < health)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
            }
            // 2
            health = value;
            healthLabel.text = "HEALTH: " + health;
            // 2
            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Score = 1000;
        Wave = 0;
        Health = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
