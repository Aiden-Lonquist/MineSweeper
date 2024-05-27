using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextScript : MonoBehaviour
{
    private float timerScore;
    private Text scoreText;
    private bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        scoreText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            timerScore += 1 * Time.deltaTime;
            scoreText.text = "Time: " + Mathf.Round(timerScore * 100) / 100;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void EndGame()
    {
        gameStarted = false;
    }

    public float GetScore()
    {
        return timerScore;
    }

}
