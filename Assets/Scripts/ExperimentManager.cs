using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ExperimentManager : MonoBehaviour
{
    public GameObject spawnPointsGameObject;
    public GameObject distractorPrefab;
    public GameObject targetPrefab;
    public float stimulusPresenceRate = 0.5f; 
    
    private List<Transform> _spawnPointsList = new List<Transform>();
    private bool _stimuliInScene = false;
    private GameObject[] _stimuliGOs;
    private List<Quaternion> _distractorDirectons = new List<Quaternion>();
    private Random rnd = new Random();
    private bool targetPresent;
    private float stimuliOnsetTime;
    private float lastReactionTime;
    private void Start()
    {
        _distractorDirectons.Add(Quaternion.Euler(-90,0,180));
        _distractorDirectons.Add(Quaternion.Euler(-90,0,0 ));
        _distractorDirectons.Add(Quaternion.Euler(90,0,0 ));
        _distractorDirectons.Add(Quaternion.Euler(90,0,180 ));
       
        // Get transforms of every spawn point "cube" of the empty parent GO "SpawnPoints"
        foreach (Transform child in spawnPointsGameObject.transform)
        {
            _spawnPointsList.Add(child);
        }
    }

    private void Update()
    {
        // Just for testing
        // TODO: delete later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnStimuli();
        }

        // DUMMY ANSWERS 
        //TODO: Connect with GUI buttons
        if (Input.GetKeyDown(KeyCode.Y))
        {
            lastReactionTime = Time.time - stimuliOnsetTime;
            CheckAnswer(true);
            //SpawnStimuli();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            lastReactionTime = Time.time - stimuliOnsetTime;
            CheckAnswer(false);
            //SpawnStimuli();
        }
    }

    private void CheckAnswer(bool response)
    {
        //DUMMY FEEBACK
        // TODO: if response correct and target present -> Highlight Target!
        if (targetPresent == response)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
        
        Debug.Log("RT was " + lastReactionTime + " seconds");
    }

    private void SpawnStimuli()
    {
        // If stimuli already in scene, delete before spawning new ones
        if (_stimuliInScene)
        {
            foreach (GameObject stimulus in _stimuliGOs)
            {
                Destroy(stimulus);
            }
        }

        targetPresent = rnd.NextDouble() < stimulusPresenceRate;
        
        if (targetPresent)
        {
            // Randomly select the target position among the spawn points and 
            // create another list for distractor spawn points
        
            var targetIndex = rnd.Next(_spawnPointsList.Count);
            var distractorSpawnPoints = RemoveTargetIndexFromList(_spawnPointsList, targetIndex);
        
            // Spawn Target Prefab
            Instantiate(targetPrefab, _spawnPointsList[targetIndex]);
        
            // Spawn Distractor Objects with randomly preselected orientation
            foreach (var spawnPoint in distractorSpawnPoints)
            {
                var distractorDirection = _distractorDirectons[rnd.Next(_distractorDirectons.Count)];
                var tmp_go = Instantiate(distractorPrefab, spawnPoint);
                tmp_go.transform.GetChild(0).rotation = distractorDirection;
            }
        }
        else
        {
            foreach (var spawnPoint in _spawnPointsList)
            {
                var distractorDirection = _distractorDirectons[rnd.Next(_distractorDirectons.Count)];
                var tmp_go = Instantiate(distractorPrefab, spawnPoint);
                tmp_go.transform.GetChild(0).rotation = distractorDirection;
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
        list.CopyTo(targetIndex+1, result, targetIndex, listCount-1-targetIndex);
        
        return new List<Transform>(result);
    }
    
}
