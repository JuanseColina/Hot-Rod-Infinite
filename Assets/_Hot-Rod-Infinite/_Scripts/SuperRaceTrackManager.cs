using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SuperRaceTrackManager : MonoBehaviour
{
    [SerializeField] private int levels;
    public int levels1 => levels;
    [SerializeField] private GameObject[] tracks;
    [SerializeField] private SpawnControl spawnControl;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("actualTrack"))
        {
            DisableAllTrack();
            UpgradeSuperTrack(PlayerPrefs.GetInt("actualTrack"));
        }
    }

    public void UpgradeSuperTrack(int level)
    {
        DisableAllTrack();
        tracks[level].SetActive(true);
        spawnControl.FindNewTrack();
        PlayerPrefs.SetInt("actualTrack", level);
    }

    private void DisableAllTrack()
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            tracks[i].SetActive(false);
        }
    }
}