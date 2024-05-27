using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButtonScript : MonoBehaviour
{
    public int sizeX, sizeY, mines;
    public GameObject fieldX, fieldY, fieldMines;
    private GameObject SM;
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SettingsManager");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        SM.GetComponent<SettingsManagerScript>().SetSizeX(sizeX);
        SM.GetComponent<SettingsManagerScript>().SetSizeY(sizeY);
        SM.GetComponent<SettingsManagerScript>().SetMineCount(mines);
        fieldX.GetComponent<InputFieldScript>().SetText();
        fieldY.GetComponent<InputFieldScript>().SetText();
        fieldMines.GetComponent<InputFieldScript>().SetText();
    }


}
