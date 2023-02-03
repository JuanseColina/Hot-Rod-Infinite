using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cams;
    private int count;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            NewCam();
            
        }
    }

    private void ResetCams()
    {
        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].Priority = 0;
        }
    }

    private void NewCam()
    {
        ResetCams();
        count++;
        cams[count].Priority = 10;
    }
    
}
