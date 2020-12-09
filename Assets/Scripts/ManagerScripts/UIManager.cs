using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject buttonCanvas;
    // Start is called before the first frame update
    void Start()
    {
        // Finding the necessary canvasses
        buttonCanvas = GameObject.Find("Buttons");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
