using System;
using Dreamteck.Splines;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    [SerializeField] private SpawnControl spawnControl;
    [SerializeField] private float speedToAdd;
    [SerializeField] private float currentSpeed = 4;
    [SerializeField] private float speedDurationTime;
    public float currentSpeed1 => currentSpeed;
    private bool hasTouched;

    private void Start()
    {
        spawnControl = FindObjectOfType<SpawnControl>();
    }

    private void Update()
    {
        if (hasTouched == false)
        {
            DelayToResetSpeed();
        }
    }

    public void TouchButton()
    {
        TouchFunction();
    }

    void TouchFunction()
    {
        ChangeSpeed(currentSpeed+speedToAdd);
        ActiveParticle(true);
        hasTouched = false;
        t = 0;
    }

    private float t = 0;
    void DelayToResetSpeed()
    {
        float value = Mathf.Lerp(1, 0, t);
        t += Time.deltaTime / speedDurationTime;
        if (value == 0)
        {
            ActiveParticle(false);
            ChangeSpeed(currentSpeed);
        }
    }

    private void ChangeSpeed(float newSpeed)
    {
        for (int i = 0; i < spawnControl.carsOnRoad1.Count; i++)
        {
            spawnControl.carsOnRoad1[i].GetComponent<SplineFollower>().followSpeed = newSpeed;
        } 
    }
    public void SpeedUpgrade(float moreSpeed)
    {
        currentSpeed += moreSpeed;
    }
    

    private void ActiveParticle(bool active)
    {
        for (int i = 0; i < spawnControl.carsOnRoad1.Count; i++)
        {
            spawnControl.carsOnRoad1[i].GetComponent<CarCtrl>().PlayParticleFx(active);
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            PlayerPrefs.GetFloat("speed");
        }
        else
        {
            PlayerPrefs.SetFloat("speed", currentSpeed);
        }
    }
}
