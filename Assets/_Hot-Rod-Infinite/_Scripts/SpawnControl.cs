using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnControl : MonoBehaviour
{
    private SplineComputer _splineComputer;
    private CheckPointManager _checkPointManager;

    [Header("Cars")]
    [SerializeField] private GameObject[] cars;
    [SerializeField] private List<GameObject> carsOnRoad;

    public List<GameObject> carsOnRoad1 => carsOnRoad;

    [Header("Checks")] [SerializeField] private GameObject check;
    [SerializeField] private List<GameObject> checksOnRoad;


    public List<double> carsDistances;
    public List<double> checksDistances;

    private void Awake()
    {
        _splineComputer = FindObjectOfType<SplineComputer>();
        _checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    private void Start()
    {
        FindNewTrack();
    }


    private int carsN;
    public void SpawnNewCar()
    {
        if (carsN >= cars.Length)
        {
            carsN = 0;
        }
        var newCar = Instantiate(cars[carsN],
            _splineComputer.GetPoint(0).position,
            quaternion.identity);
        carsOnRoad.Add(newCar);
        carsN++;
    }
    int pos = 1;
    public void SpawnNewCheck()
    {
        if (pos < _checkPointManager.posToAppearChecks1.Count)
        {
            var newCheck = Instantiate(check,
                _checkPointManager.posToAppearChecks1[pos].transform.position,
                _checkPointManager.posToAppearChecks1[pos].transform.rotation, _checkPointManager.posToAppearChecks1[pos].transform);
            checksOnRoad.Add(newCheck);
            pos++;
        }
    }


    public void FindNewTrack()
    {
        for (int i = 0; i < carsOnRoad.Count; i++)
        {
            carsOnRoad[i].GetComponent<SplineFollower>().spline = FindObjectOfType<SplineComputer>();
        }

        for (int i = 0; i < _checkPointManager.posToAppearChecks1.Count; i++)
        {
            _checkPointManager.posToAppearChecks1[i].GetComponent<SplineFollower>().spline = FindObjectOfType<SplineComputer>();
        }
    }

    private void SaveCarsAndCheck()
    {
        for (int i = 0; i < checksOnRoad.Count; i++)
        {
            checksDistances.Add(_checkPointManager.posToAppearChecks1[i].GetComponent<SplineFollower>().GetPercent());
            PlayerPrefs.SetFloat("Check" + i + "Distance",Convert.ToSingle(checksDistances[i]));
        }
        for (int i = 0; i < carsOnRoad.Count; i++)
        {
            carsDistances.Add(carsOnRoad[i].GetComponent<SplineFollower>().GetPercent());
            PlayerPrefs.SetFloat("Car" + i + "Distance",Convert.ToSingle(carsDistances[i]));
        }
       
        PlayerPrefs.SetInt("CarsToSave", carsOnRoad.Count);
        PlayerPrefs.SetInt("CheckToSave", checksOnRoad.Count);
        
    }

    private void ApplySaves()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("CarsToSave") - 1; i++)
        {
            if(PlayerPrefs.GetInt("IsClosed") == 1) SpawnNewCar();
            carsOnRoad[i].GetComponent<SplineFollower>().SetPercent(PlayerPrefs.GetFloat("Car" + i +"Distance"));
        }

        for (int i = 0; i < PlayerPrefs.GetInt("CheckToSave") - 1; i++)
        {
            if(PlayerPrefs.GetInt("IsClosed") == 1) SpawnNewCheck();
            _checkPointManager.posToAppearChecks1[i].GetComponent<SplineFollower>().SetPercent(PlayerPrefs.GetFloat("Check" + i +"Distance"));
        }
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus) ApplySaves();
        else SaveCarsAndCheck();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("IsClosed", 1);
    }
    
    
    
    ////////////////////////////////////////////////////////////////
    public void FindNewTrack1(SplineComputer[] splines)
    {
        for (int i = 0; i < carsOnRoad.Count; i++)
        {
            var randomN = Random.Range(0, splines.Length);
            carsOnRoad[i].GetComponent<SplineFollower>().spline = splines[randomN];
        }

        for (int i = 0; i < _checkPointManager.posToAppearChecks1.Count; i++)
        {
            var randomN = Random.Range(0, splines.Length);
            _checkPointManager.posToAppearChecks1[i].GetComponent<SplineFollower>().spline = splines[randomN];
        }
    }
}
