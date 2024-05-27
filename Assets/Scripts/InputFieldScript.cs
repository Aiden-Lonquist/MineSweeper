using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour
{
    public bool isX, isY, isMines;
    private GameObject SM;
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SettingsManager");
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueUpdated()
    {
        if (isX)
        {
            SM.GetComponent<SettingsManagerScript>().SetSizeX(int.Parse(textBox.text));
        } else if (isY)
        {
            SM.GetComponent<SettingsManagerScript>().SetSizeY(int.Parse(textBox.text));
        } else
        {
            SM.GetComponent<SettingsManagerScript>().SetMineCount(int.Parse(textBox.text));
        }
    }

    public void SetText()
    {
        if (isX)
        {
            int tempValue = SM.GetComponent<SettingsManagerScript>().sizeX;
            gameObject.GetComponent<InputField>().text = tempValue.ToString();
        } else if (isY)
        {
            int tempValue = SM.GetComponent<SettingsManagerScript>().sizeY;
            gameObject.GetComponent<InputField>().text = tempValue.ToString();
        } else
        {
            int tempValue = SM.GetComponent<SettingsManagerScript>().mineCount;
            gameObject.GetComponent<InputField>().text = tempValue.ToString();
        }
    }
}
