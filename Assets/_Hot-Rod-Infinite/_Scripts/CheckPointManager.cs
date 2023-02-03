using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private float currentValueOfCheck = 1;
    [SerializeField] private List<GameObject> posToAppearChecks;

    public List<GameObject> posToAppearChecks1 => posToAppearChecks;
    public float currentValueOfToAdd1 => currentValueOfCheck;

    private void Start()
    {
        for (int i = 0; i < posToAppearChecks.Count; i++)
        {
            posToAppearChecks[i].GetComponent<SplineFollower>().spline = FindObjectOfType<SplineComputer>();
        }
    }

    public void MoreMoneyUpgrade(float newValue)
    {
        currentValueOfCheck += newValue;
    }
}
