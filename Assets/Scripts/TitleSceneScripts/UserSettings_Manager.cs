using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettings_Manager : MonoBehaviour
{
    public Slider _movementSlider;
    private float movementSpeedMod;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadSettings()
    {
        PlayerPrefs.GetFloat("_movementSlider");
        //movementSpeedMod = _movementSlider
    }

    private void SaveSettings()
    {

    }
}
