using System;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
[RequireComponent(typeof(BoxCollider))]
public class CarCtrl : MonoBehaviour
{
    private TouchControl touchControl;
    private SplineFollower _splineFollower;


    private bool isReadyForTurbo;
    
    [SerializeField] private float speed = 10;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject[] wheels;

    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        touchControl = FindObjectOfType<TouchControl>();
        _splineFollower.spline = FindObjectOfType<SplineComputer>();

        
        _splineFollower.followSpeed = touchControl.currentSpeed1;
        _splineFollower.wrapMode = SplineFollower.Wrap.Loop;

        //_splineFollower.startPosition = 3;
    }

    private void Update()
    {
        for (int i = 0; i < wheels.Length; i++)
        { 
            wheels[i].transform.Rotate(new Vector3(speed * Time.deltaTime,0,0));
        }
    }

    
    public void PlayParticleFx(bool active)
    {
        if (active)
        {
            _particleSystem.Play();
        }
        else
        {
            _particleSystem.Stop();
        }
    }

    //public Vector3 SplinePos()
    //{
        //return _splineFollower.spline.GetPointPosition(0);
    //}
}