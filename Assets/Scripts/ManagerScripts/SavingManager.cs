using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
	// public variables to handle saving
	public bool logData = false;
	public float loggingInterval = 0.5f;
	public string subID = "NOT_ASSIGNED";
	
	// paths and json objects to log
	private string savePath;
	private string saveDir;
	private DataLog dataLog;
	private string stringDataLog;
	
	// variables to keep track
	private int currentTrial;
	private int logIndex;

	
	// reference variables to get information from
	private ExperimentManager experimentManager;
	private SpawningManager spawningManager;
	private EyeTrackingManager eyeTrackingManager;
	private OverlayMenuUI overlayMenuUI;
	
	
	
    // Start is called before the first frame update
    void Start()
    {

		// assign some game object references that we'll need
		experimentManager = this.transform.parent.GetComponent<ExperimentManager>();
		spawningManager = this.transform.parent.Find("SpawningManager").GetComponent<SpawningManager>();
		eyeTrackingManager = this.transform.parent.Find("EyeTrackingManager").GetComponent<EyeTrackingManager>();
		overlayMenuUI = this.transform.parent.Find("UIManager").GetComponent<OverlayMenuUI>();
		
		// set the current trial to the spawning manager's
		currentTrial = spawningManager.currentTrial;
		
    }

    // Update is called once per frame
    void Update()
    {
		// do nothing if we don't log
        if (!logData)
        {
            return;
        }
		
		// start a new logging coroutine each trial
		if (experimentManager.LocalPlayerReady && experimentManager.RemotePlayerReady &&
            currentTrial != spawningManager.currentTrial) {

			StartCoroutine("loggingRoutine");
			currentTrial = spawningManager.currentTrial;
		
		}
		
	}
		
	private IEnumerator loggingRoutine() {
		
		// create a new saving folder for a new session
		// Since sub ID can be changed during experiment, this needs to be done here
		saveDir = Path.Combine(Application.persistentDataPath, "subj_" + subID);
		if(!Directory.Exists(saveDir))
		{
			//if folder doesn't exist, create it
			Directory.CreateDirectory(saveDir);	
		}
		
		// create a new empty log for the next trial
		dataLog = new DataLog();
		
		// add stimulus variables at the beginning of the trial
		dataLog.subID = subID;
		dataLog.currentTrial = spawningManager.currentTrial;
		dataLog.stimuliSize = spawningManager.stimuliSize;
		dataLog.targetPresent = spawningManager.targetPresent;
		if (dataLog.targetPresent) {
			dataLog.targetObjectPos = spawningManager.targetGO.transform.position;
		}
		if (overlayMenuUI.condition1PPressed) {
			dataLog.condition = "1P";
		} else if (overlayMenuUI.conditionSVPressed) {
			dataLog.condition = "SV";
		} else if (overlayMenuUI.conditionSGVPressed) {
			dataLog.condition = "SGV";
		} else if (overlayMenuUI.conditionSGPressed) {
			dataLog.condition = "SG";
		} else if (overlayMenuUI.conditionNCPressed) {
			dataLog.condition = "NC";
		}
		
		logIndex = 0;
		// probably we should stop recording only when both responses are given
		// !(experimentManager.LocalResponseGiven && experimentManager.RemoteResponseGiven)
		while(!(experimentManager.LocalResponseGiven || experimentManager.RemoteResponseGiven)) {

			// add all variables that we want to log
		dataLog.index.Add(logIndex);
		dataLog.sysTime.Add(System.DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-ff"));
		dataLog.runTime.Add(Time.time);
		dataLog.remoteGazePos.Add(experimentManager.RemoteGazeSphere.position);
		dataLog.localGazePos.Add(experimentManager.LocalGazeSphere.position);
		
		logIndex ++;
		yield return new WaitForSeconds(loggingInterval);
		}
		
		// add response variables at the end of trial
		dataLog.trialAnswer = spawningManager.trialAnswer;
		dataLog.lastReactionTime = spawningManager.lastReactionTime;

		// save the current data log
		savePath = Path.Combine(saveDir, "sub_" + subID + "_trial_" + dataLog.currentTrial.ToString() + ".json");
		System.IO.File.WriteAllText(savePath, JsonUtility.ToJson(dataLog));
	}
}


[System.Serializable]
public class DataLog
{
	
	// variables we save once
	public string subID;
	public string condition;
	public int currentTrial;
	public bool targetPresent;
	public int stimuliSize;
	public Vector3 targetObjectPos;
	public bool trialAnswer;
	public float lastReactionTime;

	
	// variables we log continuously
    public List<int> index = new List<int>();
	public List<Vector3> remoteGazePos = new List<Vector3>();
	public List<Vector3> localGazePos = new List<Vector3>();
	public List<float> runTime = new List<float>();
	public List<string> sysTime = new List<string>();

}