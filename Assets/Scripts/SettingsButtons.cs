using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtons : MonoBehaviour
{
    public GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettings()
    {
        if (GameObject.Find("SettingsCanvas(Clone)") == null)
        {
            Instantiate(settingsMenu, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    public void CloseSettings()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
