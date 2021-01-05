using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Camera_Tracking : MonoBehaviour
{
    
    public GameObject gazeSphere;

    public bool withEyeTracking;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        gazeSphere = GameObject.Find("GazeSphereLocal");
        withEyeTracking = false;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!withEyeTracking)
        {
            Vector3 direction = transform.TransformDirection(Vector3.forward);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 100f))
            {
                gazeSphere.transform.position = hit.point;
            }
        }

    }
}
