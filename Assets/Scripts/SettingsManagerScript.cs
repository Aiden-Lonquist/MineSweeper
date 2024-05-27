using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagerScript : MonoBehaviour
{
    public int sizeX, sizeY, mineCount;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSizeX(int x)
    {
        sizeX = x;
    }

    public void SetSizeY(int y)
    {
        sizeY = y;
    }

    public void SetMineCount(int mines)
    {
        mineCount = mines;
    }
}
