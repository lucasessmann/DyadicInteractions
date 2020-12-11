using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExperimentManager : MonoBehaviour
{

    public float numberOfTrials;

    private SpawningManager _spawningManager;
    private SavingManager _savingManager;
    private EyeTrackingManager _eyeTrackingManager;
    private UIManager _uiManager;
    private TimingManager _timingManager;
    private float _lastReactionTime;


    // Start is called before the first frame update
    void Start()
    {
        _spawningManager = GetComponentInChildren<SpawningManager>();
        _savingManager = GetComponentInChildren<SavingManager>();
        _eyeTrackingManager = GetComponentInChildren<EyeTrackingManager>();
        _uiManager = GetComponentInChildren<UIManager>();
        _timingManager = GetComponentInChildren<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Just for testing
        // TODO: delete later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spawningManager.SpawnStimuli();
        }

        // DUMMY ANSWERS 
        //TODO: Connect with GUI buttons
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _lastReactionTime = Time.time - _spawningManager.stimuliOnsetTime;
            CheckAnswer(true);
            //SpawnStimuli();
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            _lastReactionTime = Time.time - _spawningManager.stimuliOnsetTime;
            CheckAnswer(false);
            //SpawnStimuli();
        }

    }
    
    private void CheckAnswer(bool response)
    {
        //DUMMY FEEBACK
        // TODO: if response correct and target present -> Highlight Target!
        if (_spawningManager.targetPresent == response)
        {
            Debug.Log("Correct!");
            _spawningManager.GiveTargetFeedback();
        }
        else
        {
            Debug.Log("Incorrect!");
            _spawningManager.GiveTargetFeedback();
        }
        
        Debug.Log("RT was " + _lastReactionTime + " seconds");
    }
}
