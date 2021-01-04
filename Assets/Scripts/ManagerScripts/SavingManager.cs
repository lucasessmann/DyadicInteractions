using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
	// path/file variables
	public bool logData = true;
	public float loggingInterval = 0.01f;
	public float time = 0f;
	
	
	private string savePath;
	private DataLog dataLog;
	
	// variables to write
	private int logIndex = 0;
	private int trialNo = 1;  // TODO: get this value from the Experiment Manager instead
	private int subjID = 0;  // TODO: get this value from the Experiment Manager instead
	private System.DateTime sysTime;
	
	
    // Start is called before the first frame update
    void Start()
    {
		sysTime = System.DateTime.Now;
        savePath = Application.persistentDataPath + "/sub" + subjID.ToString() + "_trial" + trialNo.ToString() + ".json";
		
		// start saving the data
		if (logData) {
			dataLog = new DataLog();
			StartCoroutine("appendLog");
		}

    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		// replace the following statement with if trial = start
		if (time >= 30f && logData) {
			time = 0f;
			logIndex = 0;
			trialNo ++;
			
			// save the datalog and create a new empty log
			saveJson();
			dataLog = new DataLog();
			savePath = Application.persistentDataPath + "/sub" + subjID.ToString() + "_trial" + trialNo.ToString() + ".json";
		
		}
		
	}
		
	private IEnumerator appendLog() {
		
		// TODO: change this while loop to return False when the game loop is over
		while(1==1) {
		//TODO: We use the current system datetime here
		// It would be better to (additionally) derive this time
		//from a game loop in the experimental manager
		sysTime = System.DateTime.Now;
		
		// add all the variables that we want to log
		dataLog.index.Add(logIndex);
		dataLog.sysTime.Add(sysTime.ToString("yyyy-MM-dd_hh-mm-ss-ff"));
		dataLog.runTime.Add(Time.time);
		dataLog.remoteGazePos.Add(this.transform.parent.GetComponent<ExperimentManager>().RemoteGazeSphere.position);
		dataLog.localGazePos.Add(this.transform.parent.GetComponent<ExperimentManager>().LocalGazeSphere.position);
		
		logIndex ++;
		yield return new WaitForSeconds(loggingInterval);
		}
	}
	
	private void saveJson() {
		string stringDataLog = JsonUtility.ToJson(dataLog);
		System.IO.File.WriteAllText(savePath, stringDataLog);
	}
	
	
}


[System.Serializable]
public class DataLog {
	// introduce all variables that we want to log
    public int trialNo;
    public List<int> index = new List<int>();
	public List<string> sysTime = new List<string>();
	public List<float> runTime = new List<float>();
	public List<Vector3> remoteGazePos = new List<Vector3>();
	public List<Vector3> localGazePos = new List<Vector3>();
	
	}