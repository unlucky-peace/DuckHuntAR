using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARtest : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool isActive = false;

    private void Awake()
    {
        ARSession.stateChanged += OnStateChanged;
    }

    private void OnStateChanged(ARSessionStateChangedEventArgs args)
    {
        Debug.Log(args.state);

        if (args.state == ARSessionState.Unsupported)
        {
            Application.Quit();
            return;
        }
        
        if (args.state == ARSessionState.Ready)
        {
            isActive = true;
        }
        
        else isActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        
        if(touch.phase != TouchPhase.Began) return;
        
        
    }
}
