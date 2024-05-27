using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTileScript : MonoBehaviour
{
    public Sprite sprTile, sprTileOne, sprTileTwo, sprTileThree, sprTileFour, sprTileFive, sprTileSix, sprTileSeven, sprTileEight, sprTileZero, sprTileMine, sprTileFlagged;
    public bool isFirstClick;
    public int tileScore;
    public bool isMine;
    public bool flagged;
    public bool clicked;
    public int x, y;
    public SpriteRenderer sr;
    private static bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;

        //sr = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        //change to hover sprite
        
    }

    private void OnMouseDown()
    {
        if (gameActive)
        {
            TileClicked();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1) && !clicked && gameActive) {
            ToggleFlagged();
        }
    }

    private void OnMouseExit()
    {
        //change to normal sprite
    }

    public void TileClicked()
    {
        if (isFirstClick)
        {
            GameObject boardGenerator = GameObject.Find("GameManager");
            boardGenerator.GetComponent<BoardGenerationScript>().StartGame(x, y);
        } else if (!flagged && !clicked)
        {
            clicked = true;
            GameObject boardGenerator = GameObject.Find("GameManager");
            switch (tileScore)
            {
                case 0:
                    TriggerAdjTile(x + 1, y + 1);
                    TriggerAdjTile(x + 1, y);
                    TriggerAdjTile(x, y + 1);
                    TriggerAdjTile(x - 1, y - 1);
                    TriggerAdjTile(x - 1, y);
                    TriggerAdjTile(x, y - 1);
                    TriggerAdjTile(x - 1, y + 1);
                    TriggerAdjTile(x + 1, y - 1);
                    sr.sprite = sprTileZero;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 1:
                    sr.sprite = sprTileOne;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 2:
                    sr.sprite = sprTileTwo;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 3:
                    sr.sprite = sprTileThree;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 4:
                    sr.sprite = sprTileFour;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 5:
                    sr.sprite = sprTileFive;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 6:
                    sr.sprite = sprTileSix;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 7:
                    sr.sprite = sprTileSeven;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                case 8:
                    sr.sprite = sprTileEight;
                    boardGenerator.GetComponent<BoardGenerationScript>().ClickTile();
                    break;
                default:
                    sr.sprite = sprTileMine;
                    EndGame();
                    break;
            }
        }
    }

    private void TriggerAdjTile(int adjX, int adjY)
    {
        if (GameObject.Find("tile " + adjX + "," + adjY) != null)
        {
            GameObject adjTile = GameObject.Find("tile " + adjX + "," + adjY);
            adjTile.GetComponent<GameTileScript>().TileClicked();
        }
    }

    private void ToggleFlagged()
    {
        flagged = !flagged;
        if (flagged)
        {
            sr.sprite = sprTileFlagged;
            GameObject.Find("MineText").GetComponent<MineTextScript>().DecreaseMineCount();
        } else
        {
            sr.sprite = sprTile;
            GameObject.Find("MineText").GetComponent<MineTextScript>().IncreaseMineCount();
        }
    }

    private void EndGame()
    {
        gameActive = false;
        GameObject boardGenerator = GameObject.Find("GameManager");
        boardGenerator.GetComponent<BoardGenerationScript>().GameLost();
    }

    public void SetFirstClick(bool firstClick)
    {
        isFirstClick = firstClick;
    }

    public void SetScore(int score)
    {
        tileScore = score;
    }

    public void SetPosition(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
}
