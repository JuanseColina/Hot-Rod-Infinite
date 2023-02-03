using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private float money;
    public float money1 => money;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddMoney(bool add, float value)
    {
        if (add)
        {
            money += value;
        }
        else
        {
            money -= value;
        }
    }


    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
        {
            PlayerPrefs.SetFloat("money", money);
        }
        else
        {
            if(PlayerPrefs.HasKey("money")) money = PlayerPrefs.GetFloat("money");
        }
    }



    [Header("FOR ADS")]
    public GameObject caja,cube;
    public float speed;
    private bool banderal;

    public bool botons;
    public GameObject panel, buy;

    public UpgradePanelManager panelManager;
    
    [SerializeField] private SpawnControl _spawnControl;
    [SerializeField] private SplineComputer[] _splineComputersLast;
    [SerializeField] private SplineComputer[] _splineComputersFour;
    [SerializeField] private SplineComputer[] _splineComputersTwo;
    private void Update()
    {
        if (botons == false)
        {
            panel.SetActive(false);
            buy.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            buy.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            caja.SetActive(true);
            cube.SetActive(true);
            banderal = true;
            panel.SetActive(false);
            buy.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            _spawnControl.FindNewTrack1(_splineComputersLast);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _spawnControl.FindNewTrack1(_splineComputersTwo);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _spawnControl.FindNewTrack1(_splineComputersFour);
        }
        if(banderal) caja.transform.Rotate(0, -speed * Time.deltaTime, 0);

        if (Input.GetKeyUp(KeyCode.Z))
        {
            panelManager.UpgradeButtonSelect(0);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            panelManager.UpgradeButtonSelect(1);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            panelManager.UpgradeButtonSelect(2);
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            panelManager.UpgradeButtonSelect(3);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            panelManager.UpgradeButtonSelect(4);
        }
    }
}
