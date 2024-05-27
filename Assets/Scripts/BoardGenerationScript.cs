using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardGenerationScript : MonoBehaviour
{
    public int sizeX, sizeY, mines;
    public GameObject tile;
    public List<List<int>> finalScores;
    public List<int> tester, tester2, tester3, tester4;
    public GameObject winText;
    public GameObject timerText;
    private GameObject SM;
    private List<GameObject> initialTiles = new List<GameObject>();
    private List<GameObject> allGameTiles = new List<GameObject>();
    private int totalTiles;
    private bool gameActive;
    private float scalingFactor, paddingSize;
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SettingsManager");
        sizeX = SM.GetComponent<SettingsManagerScript>().sizeX;
        sizeY = SM.GetComponent<SettingsManagerScript>().sizeY;
        mines = SM.GetComponent<SettingsManagerScript>().mineCount;
        totalTiles = (sizeX * sizeY) - mines;
        CalculateScaling();
        GenerateInitialGrid();
/*        finalScores = GenerateGameBoard(1,1);
        RemoveAllTiles();
        GeneratePlayBoard();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalculateScaling()
    {
        float boardScaleX = 35.54f;
        float boardScaleY = 16.25f;

        float XScalingFactor = boardScaleX / sizeX;
        float YScalingFactor = boardScaleY / sizeY;

        if (XScalingFactor < YScalingFactor)
        {
            scalingFactor = XScalingFactor;
            paddingSize = 0;
        } else
        {
            scalingFactor = YScalingFactor;
            paddingSize = (boardScaleX - (scalingFactor * sizeX)) / 2;
        }
    }

    private void GenerateInitialGrid()
    {
        for (int i=0; i<sizeY; i++)
        {
            for (int j=0; j<sizeX; j++)
            {
                GameObject t = Instantiate(tile, new Vector3(gameObject.transform.position.x + (j * scalingFactor) + paddingSize, gameObject.transform.position.y - i * scalingFactor, 0), gameObject.transform.rotation);
                t.GetComponent<GameTileScript>().SetScore(0);
                t.GetComponent<GameTileScript>().SetPosition(j, i);
                t.GetComponent<GameTileScript>().SetFirstClick(true);
                t.transform.localScale = new Vector3 (scalingFactor, scalingFactor, 1);

                initialTiles.Add(t);
            }
        }
    }

    public void StartGame(int firstClickX, int firstClickY)
    {
        gameActive = true;
        finalScores = GenerateGameBoard(firstClickX, firstClickY);
        RemoveAllTiles();
        GeneratePlayBoard(firstClickX, firstClickY);
        Debug.Log("Getting Tile: " + "tile " + firstClickX + "," + firstClickY);
        GameObject firstTile = GameObject.Find("tile " + firstClickX + "," + firstClickY);
        firstTile.GetComponent<GameTileScript>().TileClicked();
        timerText.GetComponent<TimerTextScript>().StartGame();
    }

    private List<List<int>> GenerateGameBoard(int firstTileX, int firstTileY)
    {
        List<Vector2> possibleCoords = new List<Vector2>();

        List<List<int>> tileScores = new List<List<int>>();

        for (int i=0; i<sizeY; i++)
        {
            for (int j=0; j <sizeX; j++)
            {
                possibleCoords.Add(new Vector2(j, i));
            }
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX, firstTileY)))
        {
            possibleCoords.Remove(new Vector2(firstTileX, firstTileY));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX + 1, firstTileY + 1)))
        {
            possibleCoords.Remove(new Vector2(firstTileX + 1, firstTileY + 1));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX + 1, firstTileY)))
        {
            possibleCoords.Remove(new Vector2(firstTileX + 1, firstTileY));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX, firstTileY + 1)))
        {
            possibleCoords.Remove(new Vector2(firstTileX, firstTileY + 1));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX - 1, firstTileY - 1)))
        {
            possibleCoords.Remove(new Vector2(firstTileX - 1, firstTileY - 1));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX - 1, firstTileY)))
        {
            possibleCoords.Remove(new Vector2(firstTileX - 1, firstTileY));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX, firstTileY - 1)))
        {
            possibleCoords.Remove(new Vector2(firstTileX, firstTileY - 1));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX + 1, firstTileY - 1)))
        {
            possibleCoords.Remove(new Vector2(firstTileX + 1, firstTileY - 1));
        }
        if (CheckCoordExists(possibleCoords, new Vector2(firstTileX - 1, firstTileY + 1)))
        {
            possibleCoords.Remove(new Vector2(firstTileX - 1, firstTileY + 1));
        }

        for (int i=0; i<sizeY; i++)
        {
            List<int> newRow = new List<int>();
            for (int j=0; j<sizeX; j++)
            {
                newRow.Add(0);
            }
            tileScores.Add(newRow);
        }

        for (int i=0; i<mines; i++)
        {
            Vector2 newMinePOS = possibleCoords[Random.Range(0, possibleCoords.Count)];
            Debug.Log("Adding mine at coord: " + newMinePOS.x + ", " + newMinePOS.y);
            tileScores = AssignMineScores(tileScores, newMinePOS);
            possibleCoords.Remove(newMinePOS);


        }

        return tileScores;
    }

    private void RemoveAllTiles()
    {
        foreach (GameObject tempTile in initialTiles)
        {
            Destroy(tempTile);
        }
    }
    
    private void GeneratePlayBoard(int firstX, int firstY)
    {
        for (int i=0; i<finalScores.Count; i++)
        {
            for (int j=0; j<finalScores[0].Count; j++)
            {
                GameObject t = Instantiate(tile, new Vector3(gameObject.transform.position.x + (j * scalingFactor) + paddingSize, gameObject.transform.position.y - i * scalingFactor, 0), gameObject.transform.rotation);
                t.GetComponent<GameTileScript>().SetScore(finalScores[i][j]);
                t.GetComponent<GameTileScript>().SetPosition(j, i);
                t.name = "tile " + j + "," + i;
                t.transform.localScale = new Vector3(scalingFactor, scalingFactor, 1);

                allGameTiles.Add(t);
            }
        }
    }

    private bool CheckCoordExists(List<Vector2> coords, Vector2 coord)
    {
        return coords.Contains(coord);
    }

    private List<List<int>> AssignMineScores(List<List<int>> scores, Vector2 pos)
    {
        scores[(int)pos.y][(int)pos.x] = -9;
        Debug.Log("score of tile is now: " + scores[(int)pos.y][(int)pos.x]);
        if (CheckScoreCoordExists(scores, (int)pos.x + 1, (int)pos.y + 1)) {
            scores[(int)pos.y+1][(int)pos.x+1] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x + 1, (int)pos.y))
        {
            scores[(int)pos.y][(int)pos.x + 1] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x, (int)pos.y + 1))
        {
            scores[(int)pos.y + 1][(int)pos.x] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x - 1, (int)pos.y - 1))
        {
            scores[(int)pos.y - 1][(int)pos.x - 1] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x - 1, (int)pos.y))
        {
            scores[(int)pos.y][(int)pos.x - 1] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x, (int)pos.y - 1))
        {
            scores[(int)pos.y - 1][(int)pos.x] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x - 1, (int)pos.y + 1))
        {
            scores[(int)pos.y + 1][(int)pos.x - 1] += 1;
        }
        if (CheckScoreCoordExists(scores, (int)pos.x + 1, (int)pos.y - 1))
        {
            scores[(int)pos.y - 1][(int)pos.x + 1] += 1;
        }

        return scores;
    }

    private bool CheckScoreCoordExists(List<List<int>> scores, int x, int y)
    {
        if (y < 0)
        {
            return false;
        } else if (y >= scores.Count)
        {
            return false;
        } else if (x < 0)
        {
            return false;
        } else if (x >= scores[0].Count)
        {
            return false;
        } else
        {
            return true;
        }

    }

    public void ClickTile()
    {
        totalTiles -= 1;
        if (totalTiles <= 0 && gameActive)
        {
            winText.GetComponent<WinTextScript>().GameWon();
            timerText.GetComponent<TimerTextScript>().EndGame();
            float finalScore = timerText.GetComponent<TimerTextScript>().GetScore();
            winText.GetComponent<HighScoreScript>().GameWon(finalScore);

        }
    }

    public int GetMines()
    {
        return mines;
    }

    public void GameLost()
    {
        gameActive = false;
        timerText.GetComponent<TimerTextScript>().EndGame();
        foreach (GameObject tempTile in allGameTiles)
        {
            tempTile.GetComponent<GameTileScript>().TileClicked();
        }
    }
}

