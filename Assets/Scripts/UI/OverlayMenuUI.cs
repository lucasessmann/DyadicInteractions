using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class OverlayMenuUI : MonoBehaviour
{
    #region Initialisation
    
    public GameObject uiCanvas;
    public GameObject menuOverlay;
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject settingsTab;
    public GameObject subsettingsButton;
    public GameObject subsettingsTab;
    public GameObject button1P;
    public GameObject buttonSV;
    public GameObject buttonSGV;
    public GameObject buttonSG;
    public GameObject buttonNC;
    public GameObject calibrationButton;
    public GameObject validationButton;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject endExpButton;
    public GameObject exitTab;
    public GameObject readyButton;
    public TMP_InputField subIdText1;
    public string subId1;
    public bool subId1done = false;
    public bool hmdUsed = false;
    public bool enableEyeTracking = false;
    public bool captureData = false;
    public bool paused = false;
    public bool readyBool = false;
    private bool endPressed = false;
    private bool settingPressed = false;
    private bool subjectsettingPressed = false;
    private bool condition1PPressed = false;
    private bool conditionSVPressed = false;
    private bool conditionSGVPressed = false;
    private bool conditionSGPressed = false;
    private bool conditionNCPressed = false;
    private bool calPressed = false;
    private bool valPressed = false;
    private bool makeInteractable = true;
    private Color ogButtonColor = new Color (0.03137255f, 0.5803922f, 0.9686275f, 1f);
    private Color lessSatButtonColor = new Color (0.03137255f, 0.5803922f, 0.9686275f, 0.3f);
    private Color ogTextColor = new Color (1f,1f,1f,1f);
    private Color lessSatTextColor = new Color (1f,1f,1f,0.3f);
    
    // Controllers
    public SteamVR_Action_Boolean grabGrip;
    public SteamVR_Input_Sources leftInput = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources rightInput = SteamVR_Input_Sources.RightHand;
    
    
    // Start is called before the first frame update
    void Start()
    {
        uiCanvas.SetActive(true);
        menuOverlay.SetActive(false);
    }

    private void Update()
    {
        if (hmdUsed)
        {
            ReadyButtonController();
        }
        else
        {
            ReadyButtonNoController();
        }
        
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
        settingsTab.SetActive(false);
        subsettingsTab.SetActive(false);
        settingPressed = false;
        subjectsettingPressed = false;
        calPressed = false;
        valPressed = false;
        exitTab.SetActive(false);
        endPressed = false;
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
            subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
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
            subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
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
            endExpButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            endExpButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            
            makeInteractable = true;
            // reactivate the buttons
            startButton.GetComponent<LeanButton>().interactable = makeInteractable;
            settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
            resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
            endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
        }
    }

    #endregion
    
    #region Start Button
    
    public void StartExperiment()
    {
        // Close the menu 
        menuOverlay.SetActive(!menuOverlay.activeSelf);
        // Deactivate sub menus in case they were opened before
        settingsTab.SetActive(false);
        subsettingsTab.SetActive(false);
        settingPressed = false;
        subjectsettingPressed = false;
        calPressed = false;
        valPressed = false;
        exitTab.SetActive(false);
        endPressed = false;
        
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
             subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
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
             subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
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
             subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
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
             subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
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
    
    #region Subject Settings

     // Subject Settings Button
        public void SubjectSettingsButton()
        {
             if (subjectsettingPressed) 
             {
                 // Setting the button color
                 startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                 startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                 settingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                 settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
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
                 settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 
                subjectsettingPressed = false;
                subsettingsTab.SetActive(false);
             }
             else 
             {
                 // Setting the button color
                 startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                 startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                 settingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                 settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
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
                 settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
                 
                 subjectsettingPressed = true;
                 subsettingsTab.SetActive(true);
             }
        }

        public void Condition1P()
        {
            if (condition1PPressed)
            {
                // change color of others to OG
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;

                // make the others interactable 
                makeInteractable = true;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                condition1PPressed = false;
            }
            else
            {
                // change color of others to less sat
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                
                // make the others non interactable 
                makeInteractable = false;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                condition1PPressed = true;
            }
            // Select condition
        }
        public void ConditionSV()
        {
            if (conditionSVPressed)
            {
                // change color of others to OG
                button1P.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;

                // make the others interactable 
                makeInteractable = true;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionSVPressed = false;
            }
            else
            {
                // change color of others to less sat
                button1P.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                
                // make the others non interactable 
                makeInteractable = false;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionSVPressed = true;
            }
            // Select condition
        }
        public void ConditionSGV()
        {
            if (conditionSGVPressed)
            {
                // change color of others to OG
                button1P.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;

                // make the others interactable 
                makeInteractable = true;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionSGVPressed = false;
            }
            else
            {
                // change color of others to less sat
                button1P.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                
                // make the others non interactable 
                makeInteractable = false;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionSGVPressed = true;
            }
            // Select condition
        }
        public void ConditionSG()
        {
            if (conditionSGPressed)
            {
                // change color of others to OG
                button1P.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;

                // make the others interactable 
                makeInteractable = true;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionSGPressed = false;
            }
            else
            {
                // change color of others to less sat
                button1P.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonNC.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonNC.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                
                // make the others non interactable 
                makeInteractable = false;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonNC.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionSGPressed = true;
            }
            // Select condition
        }
        public void ConditionNC()
        {
            if (conditionNCPressed)
            {
                // change color of others to OG
                button1P.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;

                // make the others interactable 
                makeInteractable = true;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionNCPressed = false;
            }
            else
            {
                // change color of others to less sat
                button1P.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                button1P.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSGV.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSGV.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                buttonSG.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
                buttonSG.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
                
                // make the others non interactable 
                makeInteractable = false;
                button1P.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSGV.GetComponent<LeanButton>().interactable = makeInteractable;
                buttonSG.GetComponent<LeanButton>().interactable = makeInteractable;
                
                conditionNCPressed = true;
            }
            // Select condition
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

    #endregion
    
    #region  Pause

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
            subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
            subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
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
            subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
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
            subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
            subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
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
            subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
            calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
            endExpButton.GetComponent<LeanButton>().interactable = makeInteractable;
        }
    }

    #endregion

    #region Ready Button

    private void ReadyButtonController()
    {
        if (grabGrip.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (readyBool)
            {
                readyButton.transform.transform.Find("Cap").Find("Text").GetComponent<Text>().color = Color.white;
                readyBool = false;
            }
            else
            {
                readyButton.transform.transform.Find("Cap").Find("Text").GetComponent<Text>().color = Color.green;
                readyBool = true;
            }
        }
    }

    private void ReadyButtonNoController()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (readyBool)
            {
                readyButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = Color.white;
                readyBool = false;
            }
            else
            {
                readyButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = Color.green;
                readyBool = true;
            }
        }
    }

    #endregion

    #region End Experiment
    // End experiment button
    public void EndExperiment()
    {
         if (endPressed) 
         {
             // Setting the button color
             startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             settingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             pauseButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
             resumeButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
             resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
            
             
             makeInteractable = true;
             // reactivate the other buttons
             startButton.GetComponent<LeanButton>().interactable = makeInteractable;
             settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
             subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
             calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
             resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
  
             
             endPressed = false; 
             exitTab.SetActive(false); 
             exitTab.SetActive(false);
         }
         else 
         {
             // Setting the button color
             startButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             settingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             subsettingsButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             subsettingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             calibrationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             validationButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             pauseButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             resumeButton.transform.Find("Cap").GetComponent<Image>().color = lessSatButtonColor;
             resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = lessSatTextColor;
             
             
             makeInteractable = false;
             // reactivate the other buttons
             startButton.GetComponent<LeanButton>().interactable = makeInteractable;
             settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
             subsettingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
             calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
             pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
             resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
             endPressed = true; 
             exitTab.SetActive(true);
         }
    }
    public void SureYes()
    {
        // Stop Experiment
    }
    public void SureNo()
    {
        exitTab.SetActive(false);
        
        // Setting the button color
        startButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        startButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        settingsButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        settingsButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        calibrationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        calibrationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        validationButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        validationButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        pauseButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        pauseButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        resumeButton.transform.Find("Cap").GetComponent<Image>().color = ogButtonColor;
        resumeButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = ogTextColor;
        
        makeInteractable = true;
        // reactivate the other buttons
        startButton.GetComponent<LeanButton>().interactable = makeInteractable;
        settingsButton.GetComponent<LeanButton>().interactable = makeInteractable;
        calibrationButton.GetComponent<LeanButton>().interactable = makeInteractable;
        validationButton.GetComponent<LeanButton>().interactable = makeInteractable;
        pauseButton.GetComponent<LeanButton>().interactable = makeInteractable;
        resumeButton.GetComponent<LeanButton>().interactable = makeInteractable;
        
        endPressed = false; 
    }
    
    #endregion
    
    #endregion
    
}
