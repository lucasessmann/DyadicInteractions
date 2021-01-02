using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
	// path/file variables
	public bool logData = true;
	public float savingInterval = 1f;
	
	
	private string savePath;
	private StreamWriter writer;
	
	// variables to write
	private int index = 0;
	private System.DateTime sysTime;
	
	
    // Start is called before the first frame update
    void Start()
    {
		sysTime = System.DateTime.Now;
        savePath = Application.persistentDataPath + "/" + sysTime.ToString("yyyy-MM-dd_hh-mm-ss-ff") + ".csv";
		
		// start saving the data
		if (logData) {
			writer = new StreamWriter(savePath, true);
			writer.WriteLine("Index;SysTime;UnityTime;Player1Rot;Player2Rot;Finished");
			writer.AutoFlush = true;
		
			StartCoroutine("SaveData");
		}

    }

    // Update is called once per frame
    void Update()
    {
		
		
	}
		
	private IEnumerator SaveData() {
		
		// change this while loop to return False when the game loop is over
		while(1==1) {
		//TODO: We use the current system datetime here
		// It would be better to (additionally) derive this time
		//from a game loop in the experimental manager
		sysTime = System.DateTime.Now;
        Debug.Log(sysTime.Second % 10);
		
		writer.WriteLine(index + ";" + sysTime + ";" + Time.time + ";0;0;0");
		Debug.Log("Got until iteration: " + index);
		index ++;
		yield return new WaitForSeconds(savingInterval);
		}
	}
	
	
}
