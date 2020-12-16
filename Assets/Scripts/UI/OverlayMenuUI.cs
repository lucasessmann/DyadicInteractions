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
    public GameObject startButton;
    public GameObject calibrationButton;
    public GameObject validationButton;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject endExpButton;
    public GameObject calSub1;
    public GameObject calSub2;
    public GameObject valSub1;
    public GameObject valSub2;
    public bool paused = false;
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
        // On press:
        // activate Menu
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        // Deactivate sub menus in case they were opened before
        calSub1.SetActive(false);
        calSub2.SetActive(false);
        valSub1.SetActive(false);
        valSub2.SetActive(false);
        calPressed = false;
        valPressed = false;
        // Activate pause or resume button
        if (paused)
        {
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
            // color
            startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
        }
        else
        {
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
            // Reset the colors if they have been set before
            startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            pauseButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            resumeButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        }
    }

    public void StartExperiment()
    {
        // Start Experiment Scene Switch
    }
    
    // Calibration button
    public void CalibrationButton()
    {
        if (calPressed)
        {
            calPressed = false;
            calSub1.SetActive(false);
            calSub2.SetActive(false);
            // Setting the button color
            startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            pauseButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            resumeButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        }
        else
        {
            calPressed = true;
            calSub1.SetActive(true);
            calSub2.SetActive(true);
            // Setting the button color
            startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            pauseButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            resumeButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
        }
    }
    // Validation button
    public void ValidationButton()
    {
        if (valPressed)
        {
            valPressed = false;
            valSub1.SetActive(false);
            valSub2.SetActive(false);
            // Setting the button color
            startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            pauseButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            resumeButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        }
        else
        {
            valPressed = true;
            valSub1.SetActive(true);
            valSub2.SetActive(true);
            // Setting the button color
            startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            pauseButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            resumeButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
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

    // Pausing or resuming the experiment (what about data capture pause?)
    public void PauseExperiment()
    {
        if (paused)
        {
            resumeButton.SetActive(false);
            pauseButton.SetActive(true);
            paused = false;
            Time.timeScale = 1;
            // color
            startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        }
        else
        {
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
            paused = true;
            Time.timeScale = 0;
            // color
            startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
        }
        
    }
    // End experiment button
    public void EndExperiment()
    {
        // Are you sure stuff and data saving 
    }
}
