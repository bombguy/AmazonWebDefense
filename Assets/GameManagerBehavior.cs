using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon;
using Amazon.DynamoDBv2;

public class GameManagerBehavior : MonoBehaviour
{
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
    public Text bugLabel;
    private int bug;
    public int Bug
    {
        get { return bug; }
        set
        {
            bug = value;
            if(bugLabel != null && bugLabel.GetComponent<Text>() != null)
            bugLabel.GetComponent<Text>().text = "" + bug;
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
                    if(nextWaveLabels[i] != null && GetComponent<Animator>() != null)
                        nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
                waveLabel.text = "Wave " + (wave + 1);
            }
            
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

            health = value;
            healthLabel.text = "Health " + health;
            // 2
            if (health <= 0 && !gameOver)
            {
                
                gameOver = true;
                showEnd();
                //SGameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");

                
            }
        }
    }
    private static string healthBarName = "greenhealth";
    private float originalPos;

    // Use this for initialization
    void Start()
	{
        Score = 0;
        Wave = 0;
        Health = 5;
        
        originalScale = GameObject.Find(healthBarName).transform.localScale.x;
        originalPos = GameObject.Find(healthBarName).transform.position.x;
        print(originalPos);

        Vector3 scale = GameObject.Find("EndObj").transform.localScale;
        scale.x = 0;
        scale.y = 0;
        GameObject.Find("EndObj").transform.localScale = scale;
    }

    void showEnd()
    {
        Vector3 scale = GameObject.Find("EndObj").transform.localScale;
        scale.x = 1;
        scale.y = 1;
        GameObject.Find("EndObj").transform.localScale = scale;

        Vector3 scale1 = GameObject.Find("HealthBar").transform.localScale;
        scale1.x = 0;
        scale1.y = 0;
        GameObject.Find("HealthBar").transform.localScale = scale1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 loc = GameObject.Find(healthBarName).transform.position;

        Vector3 tmpScale = GameObject.Find(healthBarName).transform.localScale;
        tmpScale.x = (health / 5.0f);
        GameObject.Find(healthBarName).transform.localScale = tmpScale;

        loc.x = originalPos;
        GameObject.Find(healthBarName).transform.position = loc;
    }


}
