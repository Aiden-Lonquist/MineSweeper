using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineTextScript : MonoBehaviour
{
    private int minesRemaining;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        minesRemaining = gameManager.GetComponent<BoardGenerationScript>().GetMines();
        gameObject.GetComponent<Text>().text = "Mines: " + minesRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseMineCount()
    {
        minesRemaining++;
        gameObject.GetComponent<Text>().text = "Mines: " + minesRemaining;
    }

    public void DecreaseMineCount()
    {
        minesRemaining--;
        gameObject.GetComponent<Text>().text = "Mines: " + minesRemaining;
    }
}
