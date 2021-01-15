using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Random = System.Random;

public class SpawningManager : MonoBehaviour
{
    public GameObject spawnPointsGameObject;
    public GameObject distractorPrefab;
    public GameObject targetPrefab;
    public GameObject targetGO;
    public float stimulusPresenceRate = 0.5f;

    private ExperimentManager _experimentManager;

    private List<Transform> _spawnPointsList = new List<Transform>();
    private List<Transform> _chosenSpawnPoints;
    private List<Quaternion> _distractorDirections = new List<Quaternion>();

    private bool _stimuliInScene = false;
    public bool targetPresent;
    public bool answeredPresent;
    
    public int[] stimuliSizes = {21, 35};
    private int _stimuliSize;
    private int _targetIndex;

    public int numberOfTrials = 5; // Originally 192
    public int currentTrial = 0;

    public float stimuliOnsetTime;
    public float lastReactionTime;
    // TODO: SAVE ANSWER IN VARIABLE
    
    private GameObject[] _stimuliGOs;
    private Random _rnd;
    

    // Controllers
    public OverlayMenuUI overlayScript;
    public SteamVR_Action_Boolean grabPinch;
    public SteamVR_Action_Boolean grabGrip;
    public SteamVR_Input_Sources leftInput = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources rightInput = SteamVR_Input_Sources.RightHand;

    private void Start()
    {
        _experimentManager = GetComponentInParent<ExperimentManager>();
        

        _distractorDirections.Add(Quaternion.Euler(-90, 0, 180));
        _distractorDirections.Add(Quaternion.Euler(-90, 0, 0));
        _distractorDirections.Add(Quaternion.Euler(90, 0, 0));
        _distractorDirections.Add(Quaternion.Euler(90, 0, 180));

        // Get transforms of every spawn point "cube" of the empty parent GO "SpawnPoints"
        foreach (Transform child in spawnPointsGameObject.transform)
        {
            _spawnPointsList.Add(child);
        }
    }

    public void Update()
    {
        if (_experimentManager.LocalPlayerReady && _experimentManager.RemotePlayerReady &&
            currentTrial <= numberOfTrials)
        {
            SpawnStimuli();
        }

        if (CheckAlreadyAnswered())
        {
            GiveTargetFeedback();
        }

        if (overlayScript.hmdUsed)
        {
            if (grabGrip.GetStateDown(SteamVR_Input_Sources.Any))
            {
                _experimentManager.LocalResponseGiven = false;

                _experimentManager.LocalPlayerReady = !_experimentManager.LocalPlayerReady;
            }

            if (grabPinch.GetStateDown(leftInput) & !CheckAlreadyAnswered())
            {
                HandleResponse(true);
            }

            if (grabPinch.GetStateDown(rightInput))
            {
                HandleResponse(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Y) & !CheckAlreadyAnswered())
            {
                HandleResponse(true);
            }

            if (Input.GetKeyDown(KeyCode.N) & !CheckAlreadyAnswered())
            {
                HandleResponse(false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _experimentManager.LocalResponseGiven = false;

                _experimentManager.LocalPlayerReady = !_experimentManager.LocalPlayerReady;
            }
        }
    }

    public void SetRandomObject(float randomSeed)
    {
        _rnd = new Random((int) randomSeed);
    }

    private bool CheckAlreadyAnswered()
    {
        return _experimentManager.LocalResponseGiven | _experimentManager.RemoteResponseGiven;
    }

    private void HandleResponse(bool answer)
    {
        lastReactionTime = Time.time - stimuliOnsetTime;
        _experimentManager.LocalResponseGiven = true;
        answeredPresent = answer;

        // TODO: CHANGE / REMOVE THIS
        Debug.Log(targetPresent == answeredPresent ? "Correct!" : "Incorrect!");
        Debug.Log("RT was " + lastReactionTime + " seconds");

        //GiveTargetFeedback();
    }

    private void SpawnStimuli()
    {
        
        // Increment current Trial
        currentTrial++;
        
        // Reset Answers
        _experimentManager.LocalPlayerReady = false;

        // If stimuli already in scene, delete before spawning new ones
        if (_stimuliInScene)
        {
            foreach (GameObject stimulus in _stimuliGOs)
            {
                Destroy(stimulus);
            }
        }
        
        // Target-Present or Target-Absent Trial
        targetPresent = _rnd.NextDouble() < stimulusPresenceRate;

        // Stimuli-Size
        _stimuliSize = stimuliSizes[_rnd.Next(stimuliSizes.Length)];
        _chosenSpawnPoints = _stimuliSize == 21 ? _spawnPointsList.GetRange(0, 21)  : _spawnPointsList;
     



        if (targetPresent)
        {
            // Randomly select the target position among the spawn points and 
            // create another list for distractor spawn points

            _targetIndex = _rnd.Next(_chosenSpawnPoints.Count);
            var distractorSpawnPoints = RemoveTargetIndexFromList(_chosenSpawnPoints, _targetIndex);

            // Spawn Target Prefab
            targetGO = Instantiate(targetPrefab, _chosenSpawnPoints[_targetIndex]);

            // Spawn Distractor Objects with randomly preselected orientation
            foreach (var spawnPoint in distractorSpawnPoints)
            {
                var distractorDirection = _distractorDirections[_rnd.Next(_distractorDirections.Count)];
                var tmpGameObject = Instantiate(distractorPrefab, spawnPoint);
                tmpGameObject.transform.GetChild(0).rotation = distractorDirection;
            }
        }
        else
        {
            // only spawn distractor objects
            foreach (var spawnPoint in _chosenSpawnPoints)
            {
                var distractorDirection = _distractorDirections[_rnd.Next(_distractorDirections.Count)];
                var tmpGameObject = Instantiate(distractorPrefab, spawnPoint);
                tmpGameObject.transform.GetChild(0).rotation = distractorDirection;
            }
        }

        // Save GOs in list for later deletion.
        _stimuliGOs = GameObject.FindGameObjectsWithTag("stimulus");
        _stimuliInScene = true;
        
        // Set stimuli onset time
        stimuliOnsetTime = Time.time;
    }

    // Method for removing TargetIndex from List of possible SpawnPoints. Return a new list with remaining spawn points
    private List<Transform> RemoveTargetIndexFromList(List<Transform> list, int targetIndex)
    {
        var listCount = list.Count;
        var result = new Transform[listCount - 1];
        list.CopyTo(0, result, 0, targetIndex);
        list.CopyTo(targetIndex + 1, result, targetIndex, listCount - 1 - targetIndex);

        return new List<Transform>(result);
    }

    private void GiveTargetFeedback()
    {

        // TODO: Extend Feedback
        if (targetPresent)
        {
            targetGO.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
        
        

        
    }
}
