using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExperimentManager : MonoBehaviour
{
    //

    public Transform LocalGazeSphere;
    
    public Transform RemoteGazeSphere;
    public Transform playerPosition;
    public Transform startPositionExperiment;
    
    // TODO: Try remote and local bool variables
    public bool LocalResponseGiven;
    //public bool LocalRespondedTargetPresent;
    
    public bool RemoteResponseGiven;
    //public bool RemoteRespondedTargetPresent;

    public bool LocalPlayerReady;
    
    public bool RemotePlayerReady;
    
    public bool startExperimentPress= false;
    public bool useHDMI;
    public bool withEyeTracking = false;
    
    
    
    
    //---------------------------------------------

    private static ExperimentManager _Instance;
    public static ExperimentManager Instance()
    {
        Debug.Assert(_Instance!=null);
        return _Instance;
    }
    
    [Header("Experiment Settings"), Range(1.0f, 10.0f)]
    public float WarmUpTime = 3.0f;

    [Range(1.0f, 10.0f)]
    public float InterpolationFactor = 5.0f;

    [Header("Required References")]
    public NetworkManager NetMan;
    
    public TextMesh CountdownDiplay;
    public GameObject playerSteam;
   
    

    private EExperimentStatus Status;
    private float WarmUpTimer;

    


    public void ReceivedUserStateUpdate(UserState incomingState)
    {

        RemoteGazeSphere.position = Vector3.Lerp(RemoteGazeSphere.position,         incomingState.GazeSpherePosition,      Time.deltaTime * InterpolationFactor);

        RemoteResponseGiven = incomingState.responseGiven;
        //RemoteRespondedTargetPresent = incomingState.respondedTargetPresent;
        RemotePlayerReady = incomingState.playerReady;

        //EyetrackingRemote

        //Debug.Log(incomingState.TargetPosition);

        /* 
        if (NetMan.IsServer()==false)
        { 
            dronePositionController.SetPositionAsPercentage(incomingState.TargetPosition);
        }*/

    }

    public void SetExperimentStatus(EExperimentStatus status)
    {
        Status = status;
        if (NetMan.IsServer())
        {
            NetMan.BroadCastExperimentStatusUpdate(Status);
            
        }

        if (status == EExperimentStatus.Waiting)
        {
            CountdownDiplay.text = "Waiting...";
            CountdownDiplay.gameObject.SetActive(true);
        }
        else if (status == EExperimentStatus.WarmUp)
        {
            WarmUpTimer = WarmUpTime;
            CountdownDiplay.gameObject.SetActive(true);
        }
        else // Running
        {
            
            CountdownDiplay.gameObject.SetActive(false);
            
        }
    }

    public EExperimentStatus GetExperimentStatus()
    {
        return Status;
    }

    public void ClientDisconected()
    {
        if (NetMan.IsServer())
        {
            switch (Status)
            {
                case EExperimentStatus.Waiting:
                    break;

                case EExperimentStatus.WarmUp:
                case EExperimentStatus.Running:
                    AbortExperiment();
                    break;
            }
        }
        else
        {
            Status = EExperimentStatus.Waiting;
        }
    }

    public void AbortExperiment()
    {
        if (!NetMan.IsServer())
        {
            Debug.LogWarning("Experiment abortion is only possible as server!");
            return;
        }
        if (Status == EExperimentStatus.Waiting)
        {
            return;
        }
        
        
        //Save data 
        
        SetExperimentStatus(EExperimentStatus.Waiting);
        Debug.Log("Experiment aborted!");
    }

    public void StartExperiment()
    {
        if (!NetMan.IsServer())
        {
            Debug.LogWarning("Experiment initialization is only possible as server!");
            return;
        }

        if (Status != EExperimentStatus.Waiting)
        {
            Debug.LogWarning("Cannot start Experiment, already in progress!");
            return;
        }

        int numParticipants = NetMan.GetNumConnections() + 1;
        if (numParticipants != 2)
        {
            Debug.LogWarningFormat("Experiment can only be started with exactly two participants! Currently: {0}", numParticipants);
            return;
        }

        SetExperimentStatus(EExperimentStatus.WarmUp);
    }

    private void Awake()
    {
        _Instance = this;
    }
    
    private void Start()
    {

        Debug.Assert(NetMan != null, "Sample Network Manager is not set");
        
        Debug.Assert(RemoteGazeSphere != null, "Remote GazeSphere is not set");
        Debug.Assert(LocalGazeSphere != null, "Local GazeSphere is not set");

        Debug.Assert(CountdownDiplay != null, "Countdown text mesh is not set!");
        
        Status = EExperimentStatus.Waiting;
    }

    private void Update()
    {
        if (startExperimentPress)
        {
            playerPosition.position = startPositionExperiment.position;
            playerPosition.localEulerAngles = new Vector3(0, 0, 0);
            // player is set to ready state
            // startExperiment = false;
            // LocalPlayerReady = true;
        }
        
        if (NetMan.GetState() == ENetworkState.Running)
        {
            //NetMan.BroadcastExperimentState(LocalGazeSphere, LocalResponseGiven, LocalRespondedTargetPresent, LocalPlayerReady);
            NetMan.BroadcastExperimentState(LocalGazeSphere, LocalResponseGiven, LocalPlayerReady);
         //   NetMan.BroadcastVRAvatarUpdate(LocalHead, LocalHandLeft, LocalHandRight);
        }

        if (Status == EExperimentStatus.WarmUp)
        {
            WarmUpTimer -= Time.deltaTime;
            if (WarmUpTimer <= 0.0f)
            {
                WarmUpTimer = 0.0f;
                if (NetMan.IsServer())
                {
                    SetExperimentStatus(EExperimentStatus.Running);
                }
            }
            //CountdownDiplay.text = "Get Ready\n" + Mathf.Ceil(WarmUpTimer).ToString();
        }
    }
}
