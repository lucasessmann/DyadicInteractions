using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class OverlayMenuUI : MonoBehaviour
{
    #region Initialisation
    
    public GameObject uiCanvas;
    public GameObject menuOverlay;
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject settingsTab;
    public GameObject calibrationButton;
    public GameObject validationButton;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject endExpButton;
    public TMP_InputField subIdText1;
    public string subId1;
    public bool subId1done = false;
    public bool hmdUsed = false;
    public bool enableEyeTracking = false;
    public bool captureData = false;
    public bool paused = false;
    private bool settingPressed = false;
    private bool calPressed = false;
    private bool valPressed = false;
    private bool makeInteractable = true;
    private Color ogButtonColor = new Color (0.03137255f, 0.5803922f, 0.9686275f, 1f);
    private Color lessSatButtonColor = new Color (0.03137255f, 0.5803922f, 0.9686275f, 0.3f);
    private Color ogTextColor = new Color (1f,1f,1f,1f);
    private Color lessSatTextColor = new Color (1f,1f,1f,0.3f);
    
    
    // Start is called before the first frame update
    void Start()
    {
        uiCanvas.SetActive(true);
        menuOverlay.SetActive(false);
    }

    #endregion

    #region Button Functions

    #region CogWheelButton
    
    // Open the settings menu
    public void CogWheelButton()
    {
        // On press:
        // activate Menu
        menuOverlay.SetActive(!menuOverlay.activeSelf);
        // Deactivate sub menus in case they were opened before
        settingsTab.SetActive((false));
        settingPressed = false;
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
            settingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;

            makeInteractable = false;
            // deactivate the buttons
            startButton.GetComponent<LeanButton>().interactable = makeInteractable;
            settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
        }
        else
        {
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
            // Reset the colors if they have been set before
            startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            settingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
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

            makeInteractable = true;
            // reactivate the buttons
            startButton.GetComponent<LeanButton>().interactable = makeInteractable;
            settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
        }
    }

    #endregion
    
    #region Start Button
    
    public void StartExperiment()
    {
        // Start Experiment Scene Switch
    }

    #endregion
    
    #region Settings
    // Settings Button
    public void SettingsButton()
    {
         if (settingPressed) 
         {
             // Setting the button color
             startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             pauseButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             resumeButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             
             makeInteractable = true;
             // reactivate the other buttons
             startButton.GetComponent<LeanButton>().interactable = makeInteractable;
             calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
             resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
             endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
             
            settingPressed = false;
            settingsTab.SetActive(false);
            settingsTab.SetActive(false);
         }
         else 
         {
             // Setting the button color
             startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             pauseButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             resumeButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             
             makeInteractable = false;
             // reactivate the other buttons
             startButton.GetComponent<LeanButton>().interactable = makeInteractable;
             calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
             resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
             endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
            settingPressed = true;
            settingsTab.SetActive(true);
         }
    }

    public void HmdUseEnable()
    {
        hmdUsed = true;
    }
    public void HmdUseDisable()
    {
        hmdUsed = false;
    }
    public void DataCaptureEnable()
    {
        captureData = true;
    }
    public void DataCaptureDisable()
    {
        captureData = false;
    }

    public void EyeTrackingEnable()
    {
        enableEyeTracking = true;
    }
    public void EyeTrackingDisable()
    {
        enableEyeTracking = false;
    }
    
    public void SubId1()
    {
        subId1 = subIdText1.text;
        if (!Input.GetKeyDown(KeyCode.Return) || subId1done !=false) return;
        subId1done = true;
    }

    #endregion
    
    #region CalibrationValidation
    
    // Calibration button
    public void CalibrationButton()
    {
        calPressed = !calPressed;
        // Start Calibration from somewhere
    }
    // Validation button
    public void ValidationButton()
    {
        valPressed = !valPressed;
        // Start Validation from somewhere
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
            settingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            
            makeInteractable = true;
            // reactivate the other buttons
            startButton.GetComponent<LeanButton>().interactable = makeInteractable;
            settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
            resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
            endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
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
            settingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
            
            makeInteractable = false;
            // reactivate the other buttons
            startButton.GetComponent<LeanButton>().interactable = makeInteractable;
            settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
        }
    }
    #endregion
    
    #region End Experiment
    // End experiment button
    public void EndExperiment()
    {
        // Are you sure stuff 
    }
    
    #endregion
    
    #endregion
    
}
