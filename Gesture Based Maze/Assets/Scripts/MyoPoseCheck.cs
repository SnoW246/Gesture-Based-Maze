using System;
using System.Collections;
using System.Collections.Generic;
using Thalmic.Myo;
using UnityEngine;

public class MyoPoseCheck : MonoBehaviour {
    public bool isPoseCheckEnabled = true;
    public GameObject myo;
    public float checkNewMyoPoseRate = 1f;
    private float nextMyoPoseCheck = 0f;
    public bool areFingersSpread = false;

    Pose lastMyoPose;
    ThalmicMyo myoArmband;

    public delegate void PoseAction();
    public static event PoseAction onFingersSpread;
    public static event PoseAction stopFingersSpread;

    // Use this for initialization
    void Start () {
        myoArmband = myo.GetComponent<ThalmicMyo>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isPoseCheckEnabled)
        {
            GetCurrentPose();
            GetAction();
        }
    }

    private void GetCurrentPose()
    {
        if (Time.time > nextMyoPoseCheck)
        {
            lastMyoPose = myoArmband.pose;
            nextMyoPoseCheck = Time.time + checkNewMyoPoseRate;
        }
    }

    private void GetAction()
    {
        switch (myoArmband.pose)
        {
            case Pose.FingersSpread:
                if (onFingersSpread != null)
                {
                    onFingersSpread();
                    areFingersSpread = true;
                    print("fingers spread");
                }
                break;

            case Pose.DoubleTap:
            case Pose.Rest:
            case Pose.Unknown:
                StopSpreadingFingers();
                break;
        }
    }

    void StopSpreadingFingers()
    {
        if (areFingersSpread)
        {
            stopFingersSpread();
            areFingersSpread = false;
        }
    }
}