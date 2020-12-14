using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OverlayMenuUI : MonoBehaviour
{
    public GameObject settingButtonCanvas;
    public GameObject menuCanvas;
    public GameObject settingButton;
    public GameObject calibrationButton;
    public GameObject validationButton;
    public GameObject endExpButton;
    public GameObject calSub1;
    public GameObject calSub2;
    public GameObject valSub1;
    public GameObject valSub2;
    private bool calPressed = false;
    private bool valPressed = false;
    private Color ogButtonColor = new Color (0.03137255f, 0.5803922f, 0.9686275f, 1f);
    private Color lessSatButtonColor = new Color (0.03137255f, 0.5803922f, 0.9686275f, 0.3f);
    private Color ogTextColor = new Color (1f,1f,1f,1f);
    private Color lessSatTextColor = new Color (1f,1f,1f,0.3f);
    
    
    // Start is called before the first frame update
    void Start()
    {
       settingButtonCanvas.SetActive((true));
       menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Functions
    // Open the settings menu
    public void SettingsButton()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        calSub1.SetActive(false);
        calSub2.SetActive(false);
        valSub1.SetActive(false);
        valSub2.SetActive(false);
        calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        
        // Fix color bug
    }
    public void CalibrationButton()
    {
        if (calPressed)
        {
            calPressed = false;
            calSub1.SetActive(false);
            calSub2.SetActive(false);
            // Setting the button color
            validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        }
        else
        {
            calPressed = true;
            calSub1.SetActive(true);
            calSub2.SetActive(true);
            // Setting the button color
            validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
        }
    }
    public void ValidationButton()
    {
        if (valPressed)
        {
            valPressed = false;
            valSub1.SetActive(false);
            valSub2.SetActive(false);
            // Setting the button color
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        }
        else
        {
            valPressed = true;
            valSub1.SetActive(true);
            valSub2.SetActive(true);
            // Setting the button color
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;

        }
    }

    public void CalibrationSub1()
    {
        // Start calibration sub1
    }
    public void CalibrationSub2()
    {
        // Start calibration sub2
    }
    public void ValidationSub1()
    {
        // Start validation sub1
    }
    public void ValidationSub2()
    {
        // Start validation sub2
    }

    public void EndExperiment()
    {
        // Are you sure stuff and data saving 
    }
}
