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
    
    private List<Transform> _spawnPointsList = new List<Transform>();
    private bool _stimuliInScene = false;
    private GameObject[] _stimuliGOs;
    private List<Quaternion> _distractorDirectons = new List<Quaternion>();
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
        
        // Randomly select the target position among the spawn points and 
        // create another list for distractor spawn points
        var rnd = new Random();
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
        
        // Save GOs in list for later deletion.
        _stimuliGOs = GameObject.FindGameObjectsWithTag("stimulus");
        _stimuliInScene = true;
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
