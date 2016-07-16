using UnityEngine;
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
            scoreLabel.GetComponent<Text>().text = "" + score;
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
            waveLabel.text = "Wave " + (wave + 1);
        }
    }
    public float originalScale;
    public Text healthLabel;
    public Image healthBar;
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
            if (value >= 0)
            {
                healthBar.fillAmount = value / 5.0f;
            } else {
                healthBar.fillAmount = 0f;
            }
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
        Cash = 1024;
        Score = 0;
        Wave = 0;
        Health = 5;
        
        originalScale = GameObject.Find("healthSliderTop").transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpScale = GameObject.Find("healthSliderTop").transform.localScale;
        tmpScale.x = (health / 5.0f);
        GameObject.Find("healthSliderTop").transform.localScale = tmpScale;
    }
}
