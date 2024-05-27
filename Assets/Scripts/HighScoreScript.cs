using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public GameObject gm;
    public Text score1Text, score2Text, score3Text;
    private int sizeX, sizeY, mineCount;
    private float score1, score2, score3;
    // Start is called before the first frame update
    void Start()
    {
/*        sizeX = gm.GetComponent<BoardGenerationScript>().sizeX;
        sizeY = gm.GetComponent<BoardGenerationScript>().sizeY;
        mineCount = gm.GetComponent<BoardGenerationScript>().mines;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameWon(float newScore)
    {
        sizeX = gm.GetComponent<BoardGenerationScript>().sizeX;
        sizeY = gm.GetComponent<BoardGenerationScript>().sizeY;
        mineCount = gm.GetComponent<BoardGenerationScript>().mines;
        LoadHighScores();
        updateScores(newScore);
        SaveHighScores();
        DisplayHighScore();
    }


    public void LoadHighScores()
    {
        Debug.Log("Loading scores with key:" + sizeX + ", " + sizeY + ", " + mineCount);
        string topScores = PlayerPrefs.GetString(sizeX + ", " + sizeY + ", " + mineCount, "99999,99999,99999");
        Debug.Log(topScores);
        string[] scores = topScores.Split(',');
        score1 = float.Parse(scores[0]);
        score2 = float.Parse(scores[1]);
        score3 = float.Parse(scores[2]);
    }

    public void updateScores(float currentScore)
    {
        float newScore = currentScore;
        if (newScore < score1)
        {
            score3 = score2;
            score2 = score1;
            score1 = newScore;
        }
        else if (newScore < score2)
        {
            score3 = score2;
            score2 = newScore;
        }
        else if (newScore < score3)
        {
            score3 = newScore;
        }
    }

    public void SaveHighScores()
    {
        // player pref key reads: 35, 16, 99    value reads: 10.5,14.6,22.1
        PlayerPrefs.SetString(sizeX + ", " + sizeY + ", " + mineCount, score1 + "," + score2 + "," + score3);
    }

    public void DisplayHighScore()
    {
        score1Text.text = SetScoreText("1: ", score1);
        score2Text.text = SetScoreText("2: ", score2);
        score3Text.text = SetScoreText("3: ", score3);
    }

    private string SetScoreText(string index, float s)
    {
        if (s < 99999)
        {
            return index + s;
        }
        return index + "-";
    }
}
