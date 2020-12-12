using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public GameObject premenuCanvas;
    public GameObject areUsureCanvas;
    public GameObject settingsCanvas;
    public TMP_InputField subIdText1;
    public TMP_InputField subIdText2;
    public string subId1;
    public string subId2;
    public bool subId1done = false;
    public bool subId2done = false;
    public bool captureData = false;

    // Start is called before the first frame update
    public void Start()
    {
        // limit the SubID input field to 4 characters
        subIdText1.characterLimit = 4;
        subIdText2.characterLimit = 4;
        // Starting with the Pre Menu
        settingsCanvas.SetActive(false);
        areUsureCanvas.SetActive(false);
        premenuCanvas.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreStartButton()
    {
        // Load Lobby Scene
    }

    public void PreSettingsButton()
    {
        premenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void PreExitButton()
    {
        premenuCanvas.SetActive(false);
        areUsureCanvas.SetActive(true);
    }

    public void ExitNoButton()
    {
        areUsureCanvas.SetActive(false);
        premenuCanvas.SetActive(true);
    }
    public void ExitYesButton()
    {
        Application.Quit();
    }
    public void DataCaptureEnable()
    {
        captureData = true;
    }
    public void DataCaptureDisable()
    {
        captureData = false;
    }


    public void SubId1()
    {
        subId1 = subIdText1.text;
        if (!Input.GetKeyDown(KeyCode.Return) || subId1done !=false) return;
        subId1done = true;
    }
    public void SubId2()
    {
        subId2 = subIdText2.text;
        if (!Input.GetKeyDown(KeyCode.Return) || subId2done !=false) return;
        subId2done = true;
    }

    public void ExitSettings()
    {
        settingsCanvas.SetActive(false);
        premenuCanvas.SetActive((true));
    }
}
