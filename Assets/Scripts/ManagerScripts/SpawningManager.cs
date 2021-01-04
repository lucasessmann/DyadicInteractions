using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawningManager : MonoBehaviour
{
    public GameObject spawnPointsGameObject;
    public GameObject distractorPrefab;
    public GameObject targetPrefab;
    private GameObject _targetGO;
    public float stimulusPresenceRate = 0.5f;

    private ExperimentManager _experimentManager;

    private List<Transform> _spawnPointsList = new List<Transform>();
    private List<Transform> _chosenSpawnPoints;
    private List<Quaternion> _distractorDirections = new List<Quaternion>();

    private bool _stimuliInScene = false;
    public bool targetPresent;
    
    private int[] _stimuliSizes = {21, 35};
    private int _stimuliSize;
    private int _targetIndex;
    
    
    public int randomSeed = 7;

    public float stimuliOnsetTime;
    private float _lastReactionTime;
    
    private GameObject[] _stimuliGOs;
    private Random _rnd;

    private void Start()
    {
        _experimentManager = GetComponentInParent<ExperimentManager>();
        _rnd = new Random(randomSeed);

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
        // Just for testing
        // TODO: delete later
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnStimuli();
        }
        
        if(_experimentManager.LocalResponseGiven | _experimentManager.RemoteResponseGiven)
        {
            //HandleResponse(_experimentManager.respondedTargetPresent);
            GiveTargetFeedback();
        }

        // DUMMY ANSWERS 
        //TODO: Connect with GUI buttons
        if (Input.GetKeyDown(KeyCode.Y))
        {
            HandleResponse(true);
            //SpawnStimuli();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            HandleResponse(false);
            //SpawnStimuli();
        }
    }

    private void HandleResponse(bool answeredPresent)
    {
        _lastReactionTime = Time.time - stimuliOnsetTime;
        _experimentManager.LocalResponseGiven = true;
        _experimentManager.LocalRespondedTargetPresent = answeredPresent;
        //CheckAnswer(answeredPresent);

        Debug.Log(targetPresent == answeredPresent ? "Correct!" : "Incorrect!");
        Debug.Log("RT was " + _lastReactionTime + " seconds");

        //GiveTargetFeedback();
    }

    private void SpawnStimuli()
    {
        // Reset Answers
        _experimentManager.LocalResponseGiven = false;

        // If stimuli already in scene, delete before spawning new ones
        if (_stimuliInScene)
        {
            foreach (GameObject stimulus in _stimuliGOs)
            {
                Destroy(stimulus);
            }
        }

        targetPresent = _rnd.NextDouble() < stimulusPresenceRate;

        _stimuliSize = _stimuliSizes[_rnd.Next(_stimuliSizes.Length)];
        _chosenSpawnPoints = _stimuliSize == 21 ? _spawnPointsList.GetRange(0, 21)  : _spawnPointsList;
     



        if (targetPresent)
        {
            // Randomly select the target position among the spawn points and 
            // create another list for distractor spawn points

            _targetIndex = _rnd.Next(_chosenSpawnPoints.Count);
            var distractorSpawnPoints = RemoveTargetIndexFromList(_chosenSpawnPoints, _targetIndex);

            // Spawn Target Prefab
            _targetGO = Instantiate(targetPrefab, _chosenSpawnPoints[_targetIndex]);

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

    public void GiveTargetFeedback()
    {
        // TODO: Extend Feedback
        if (targetPresent)
        {
            _targetGO.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }

        
    }
}
