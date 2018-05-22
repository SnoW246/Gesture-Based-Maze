using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyoFunctions : MonoBehaviour {

    static ThalmicMyo myo;

    void Start()
    {
        myo = GetComponent<ThalmicMyo>();
    }

    public static bool IsMyoInXRotationRange(float xStart, float xEnd)
    {
        if (myo.transform.localRotation.eulerAngles.x > xStart && myo.transform.localRotation.eulerAngles.x < xEnd)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
