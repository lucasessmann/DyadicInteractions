using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
	// public variables to handle saving
	public bool logData = true;
	public float loggingInterval = 0.01f;
	
	// paths and json objects to log
	private string savePath;
	private string saveDir;
	private DataLog dataLog;
	
	// variables to keep track
	private int subjID = 0;  // TODO: get this value from the Experiment Manager instead
	private int currentTrial;
	private int logIndex;

	
	// reference variables to get information from
	private Transform remoteGazeSphere;
	private Transform localGazeSphere;
	private Transform spawningManager;
	private Transform experimentManager;
	private Transform eyeTrackingManager;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		// do nothing if we don't log
        if (!logData)
        {
            return;
        }

		// assign some game object references that we'll need
		experimentManager = this.transform.parent;
		spawningManager = experimentManager.Find("SpawningManager");
		eyeTrackingManager = experimentManager.Find("EyeTrackingManager");
		remoteGazeSphere = experimentManager.GetComponent<ExperimentManager>().RemoteGazeSphere;
		localGazeSphere = experimentManager.GetComponent<ExperimentManager>().LocalGazeSphere;
		
		// create a new saving folder for a new session
		saveDir = Path.Combine(Application.persistentDataPath, "session_" + System.DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-ff"));
		if(!Directory.Exists(saveDir))
		{
			//if folder doesn't exist, create it
			Directory.CreateDirectory(saveDir);	
		}
		
		// set the current trial to the spawning manager's
		currentTrial = spawningManager.GetComponent<SpawningManager>().currentTrial;
		
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
		if (currentTrial != spawningManager.GetComponent<SpawningManager>().currentTrial) {

			StartCoroutine("loggingRoutine");
			currentTrial = spawningManager.GetComponent<SpawningManager>().currentTrial;
		
		}
		
	}
		
	private IEnumerator loggingRoutine() {
		
		// create a new empty log for the next trial
		dataLog = new DataLog();
		
		// add stimulus variables at the beginning of the trial
		dataLog.currentTrial = spawningManager.GetComponent<SpawningManager>().currentTrial;
		dataLog.stimuliSizes = spawningManager.GetComponent<SpawningManager>().stimuliSizes;
		dataLog.targetPresent = spawningManager.GetComponent<SpawningManager>().targetPresent;
		if (dataLog.targetPresent) {
			dataLog.targetObjectPos = spawningManager.GetComponent<SpawningManager>().targetGO.transform.position;
		}
		
		logIndex = 0;
		while(!(experimentManager.GetComponent<ExperimentManager>().LocalResponseGiven && experimentManager.GetComponent<ExperimentManager>().RemoteResponseGiven)) {
		
		// add r variables that we want to log
		dataLog.index.Add(logIndex);
		dataLog.sysTime.Add(System.DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-ff"));
		dataLog.runTime.Add(Time.time);
		dataLog.remoteGazePos.Add(remoteGazeSphere.position);
		dataLog.localGazePos.Add(localGazeSphere.position);
		
		logIndex ++;
		yield return new WaitForSeconds(loggingInterval);
		}
		
		// add response variables at the end of trial
		dataLog.answerPresent = spawningManager.GetComponent<SpawningManager>().answeredPresent;
		dataLog.lastReactionTime = spawningManager.GetComponent<SpawningManager>().lastReactionTime;

		
		
		// save the current data log
		savePath = Path.Combine(saveDir, "sub" + subjID.ToString() + "_trial" + dataLog.currentTrial.ToString() + ".json");
		saveJson();
	}
	
	// save dataLog to savePath in a JSON format
	private void saveJson() {
		string stringDataLog = JsonUtility.ToJson(dataLog);
		System.IO.File.WriteAllText(savePath, stringDataLog);
	}
	
	
}


[System.Serializable]
public class DataLog
{
	
	// variables we save once
	public int currentTrial;
	public bool targetPresent;
	public int[] stimuliSizes;
	public Vector3 targetObjectPos;
	public bool answerPresent;
	public float lastReactionTime;
	
	// variables we log continuously
    public List<int> index = new List<int>();
	public List<Vector3> remoteGazePos = new List<Vector3>();
	public List<Vector3> localGazePos = new List<Vector3>();
	public List<float> runTime = new List<float>();
	public List<string> sysTime = new List<string>();

}