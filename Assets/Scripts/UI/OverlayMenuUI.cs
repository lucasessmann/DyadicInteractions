﻿using System;
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
    public GameObject remoteGazeSphere;
    public GameObject VRcamera;
    public GameObject EyeTrackingManager;

    private ExperimentManager _experimentManager;
	private SavingManager _savingManager;
    public GameObject subjectCanvasHmd;
    public GameObject subjectCanvasFallback;
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
    public GameObject readyButtonFallback;
    public GameObject textBackground;
    public GameObject text1P;
    public GameObject textSv;
    public GameObject textSgv;
    public GameObject textSg;
    public GameObject textNc;
    public GameObject generalInfoText;
    public GameObject vrText;
    public GameObject fallbackText;
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
    public bool condition1PPressed = false;
    public bool conditionSVPressed = false;
    public bool conditionSGVPressed = false;
    public bool conditionSGPressed = false;
    public bool conditionNCPressed = false;
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
        SteamVR_Utils.Event.Send("hide_render_models", false);
        uiCanvas.SetActive(true);
        menuOverlay.SetActive(false);
        subjectCanvasHmd.SetActive(false);
        textBackground.SetActive(false);
        generalInfoText.SetActive(false);
        fallbackText.SetActive(false);
        vrText.SetActive(false);
        text1P.SetActive(false);
        textSv.SetActive(false);
        textSgv.SetActive(false);
        textSg.SetActive(false);
        textNc.SetActive(false);
        
        _experimentManager = GetComponentInParent<ExperimentManager>();
		_savingManager = this.transform.parent.Find("SavingManager").GetComponent<SavingManager>();
        
        // playerSteam = GameObject.Find("PlayerSTEAM");
        VRcamera = GameObject.Find("VRCamera");
        EyeTrackingManager = GameObject.Find("EyeTrackingManager");
        
    }

    private void Update()
    {
        if (hmdUsed)
        {
            readyButton.transform.Find("Cap").Find("Text").GetComponent<Text>().color = _experimentManager.LocalPlayerReady ? Color.green : Color.white;
        }
        else
        {
            readyButtonFallback.transform.Find("Cap").Find("Text").GetComponent<Text>().color = _experimentManager.LocalPlayerReady ? Color.green : Color.white;
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
        _experimentManager.startExperimentPress = true;

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
        subjectCanvasFallback.SetActive(false);
        subjectCanvasHmd.SetActive(true);
        //playerSteam.SetActive(true);
        // playerSteam.GetComponent<Player>().useHMD = true;
    }
    public void HmdUseDisable()
    {
        hmdUsed = false;
        subjectCanvasFallback.SetActive(true);
        subjectCanvasHmd.SetActive(false);
        // layerSteam.SetActive(true);
        // playerSteam.GetComponent<Player>().useHMD = false;
    }
    public void DataCaptureEnable()
    {
        captureData = true;
		_savingManager.logData = true;
    }
    public void DataCaptureDisable()
    {
        captureData = false;
		_savingManager.logData = false;
    }

    // note the eye tracking settings should only be used if a VR headset is connected!
    public void EyeTrackingEnable()
    {
        enableEyeTracking = true;
        // if eye tracking is set activate eye tracking manager component
        EyeTrackingManager.SetActive(true);
        // deactivate the gaze sphere movement based on nose vector
        VRcamera.GetComponent<VR_Camera_Tracking>().withEyeTracking = true;
    }
    public void EyeTrackingDisable()
    {
        // if eye tracking is deactivated
        enableEyeTracking = false;
        // deactivate the eye tracking component
        EyeTrackingManager.SetActive(false);
        // activate the gaze sphere movement based on nose vector
        VRcamera.GetComponent<VR_Camera_Tracking>().withEyeTracking = false;
    }
    
    public void SubId1()
    {
        subId1 = subIdText1.text;
		_savingManager.subID = subId1;
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
                // set instructions according to condition
                textBackground.SetActive(false);
                generalInfoText.SetActive(false);
                fallbackText.SetActive(false);
                vrText.SetActive(false);
                text1P.SetActive(false);
                
                // make remote gaze sphere visible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
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
                
                // set instructions according to condition
                textBackground.SetActive(true);
                generalInfoText.SetActive(true);
                if (hmdUsed)
                {
                    vrText.SetActive(true); 
                }
                else
                {
                    fallbackText.SetActive(true);
                }
                text1P.SetActive(true);
                // make remote gaze sphere invisible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = false;
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
                
                // set instructions according to condition
                textBackground.SetActive(false);
                generalInfoText.SetActive(false);
                fallbackText.SetActive(false);
                vrText.SetActive(false);
                textSv.SetActive(false);

                // make remote gaze sphere visible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
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
                
                // set instructions according to condition
                textBackground.SetActive(true);
                generalInfoText.SetActive(true);
                if (hmdUsed)
                {
                    vrText.SetActive(true); 
                }
                else
                {
                    fallbackText.SetActive(true);
                }
                textSv.SetActive(true);
                
                // make remote gaze sphere invisible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = false;
                
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
                
                // set instructions according to condition
                textBackground.SetActive(false);
                generalInfoText.SetActive(false);
                fallbackText.SetActive(false);
                vrText.SetActive(false);
                textSgv.SetActive(false);
                
                // make remote gaze sphere visible (default)
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
                
                
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
                
                // set instructions according to condition
                textBackground.SetActive(true);
                generalInfoText.SetActive(true);
                if (hmdUsed)
                {
                    vrText.SetActive(true); 
                }
                else
                {
                    fallbackText.SetActive(true);
                }
                textSgv.SetActive(true);
                
                // make remote gaze sphere visible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
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
                
                // set instructions according to condition
                textBackground.SetActive(false);
                generalInfoText.SetActive(false);
                fallbackText.SetActive(false);
                vrText.SetActive(false);
                textSg.SetActive(false);
                
                // make remote gaze sphere visible (default)
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
                
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
                
                // set instructions according to condition
                textBackground.SetActive(true);
                generalInfoText.SetActive(true);
                if (hmdUsed)
                {
                    vrText.SetActive(true); 
                }
                else
                {
                    fallbackText.SetActive(true);
                }
                textSg.SetActive(true);
                
                // make remote gaze sphere visible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
                
            }
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
                
                // set instructions according to condition
                textBackground.SetActive(false);
                generalInfoText.SetActive(false);
                fallbackText.SetActive(false);
                vrText.SetActive(false);
                textNc.SetActive(false);
                
                
                // make remote gaze sphere visible (default)
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = true;
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
                
                // set instructions according to condition
                textBackground.SetActive(true);
                generalInfoText.SetActive(true);
                if (hmdUsed)
                {
                    vrText.SetActive(true); 
                }
                else
                {
                    fallbackText.SetActive(true);
                }
                textNc.SetActive(true);
                
                // make remote gaze sphere invisible
                remoteGazeSphere.GetComponent<MeshRenderer>().enabled = false;
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
        Application.Quit();
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
