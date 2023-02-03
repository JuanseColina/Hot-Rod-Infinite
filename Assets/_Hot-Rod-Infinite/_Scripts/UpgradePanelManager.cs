using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    private CanvasController _canvas;
    private SpawnControl _spawnControl;
    private CheckPointManager _checkPointManager;
    private TouchControl _touchControl;

    private SuperRaceTrackManager _superRaceTrackManager;

    [Header("New Track Button")]
    [SerializeField] private TextMeshProUGUI upgradeName;

    [Header("Upgrade Buttons")]
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private GameObject[] adVideoButton;
    [SerializeField] private Button[] buyButton;
    [SerializeField] private TextMeshProUGUI[] textLevel, moneyValueOfButton;
    [SerializeField] private int[] upgradeLevel;
    [SerializeField] private float[] moneyValue, plusToAddWhenBuy ;
    [SerializeField] private string text = "Lv. ", symbol = "$";
    [SerializeField] private float speedToUpgrade, moneyToUpgrade;
   

    //private int _levelToUnlockNextTrack = 1;
    
    
    //private float current;

    private void Start()
    {
        _canvas = FindObjectOfType<CanvasController>();
        _spawnControl = FindObjectOfType<SpawnControl>();
        _checkPointManager = FindObjectOfType<CheckPointManager>();
        _touchControl = FindObjectOfType<TouchControl>();
        _superRaceTrackManager = FindObjectOfType<SuperRaceTrackManager>();

        //StartWithNewParameters();
    }

    private void Update()
    {
        ButtonAdControl(); 
    }

    public void UpgradeButtonSelect(int id)
    {
        CheckMoneyToUpgrade(id);
    }

    public void UpgradeButtomAction(int id, Button button,
        TextMeshProUGUI levelText, int currentLevel)
    {
        UpgradeButtoms(id);
        LeanTween.scale(button.gameObject, Vector3.one * 1.05f, .075f).setLoopPingPong(1);
        upgradeLevel[currentLevel]++;
        levelText.text = text + upgradeLevel[currentLevel];
        if(id == 4)MaxLevelOfTrack(upgradeLevel[currentLevel], id);
        
        PlayerPrefs.SetInt("Lv of button" + id , upgradeLevel[currentLevel]);
        
    }

    public void CheckMoneyToUpgrade(int id)
    {
        if (moneyValue[id] <= GameController.Instance.money1)
        { 
            GameController.Instance.AddMoney(false, moneyValue[id]);
            
            var newValue = moneyValue[id] += plusToAddWhenBuy[id];
            moneyValueOfButton[id].text = newValue.ToString("00") + symbol;
            
            UpgradeButtomAction(id, upgradeButtons[id], textLevel[id],
                id);
            PlayerPrefs.SetFloat("Money value" + id, newValue);
        }
    }

    private void StartWithNewParameters()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (PlayerPrefs.HasKey("Money value" + i) || PlayerPrefs.HasKey("Lv of button" + i) )
            {
                moneyValue[i] = PlayerPrefs.GetFloat("Money value" + i);
                moneyValueOfButton[i].text = moneyValue[i].ToString("00") + symbol;

                upgradeLevel[i] = PlayerPrefs.GetInt("Lv of button" + i);
                textLevel[i].text = text + upgradeLevel[i];
            }
        }
    }

    private void ButtonAdControl()
    {
        for (int i = 0; i < moneyValue.Length; i++)
        {
            if (GameController.Instance.money1 >= moneyValue[i])
            {
                upgradeButtons[i].interactable = true;
                if(i > 4)buyButton[i].interactable = true;
                //newTrackButton.interactable = true;
            }
            else
            {
                upgradeButtons[i].interactable = false;
                if(i > 4)buyButton[i].interactable = false;
                // buyButton[i].SetActive(false);
               // adVideoButton[i].SetActive(true);
            }
        }
    }
    
    
    public void UpgradeButtoms(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                _touchControl.SpeedUpgrade(speedToUpgrade);
                break;
            case 1:
                _spawnControl.SpawnNewCar();
                break;
            case 2:
                _spawnControl.SpawnNewCheck();
                break;
            case 3:
                _checkPointManager.MoreMoneyUpgrade(moneyToUpgrade);
                break;
            case 4:
                _superRaceTrackManager.UpgradeSuperTrack(upgradeLevel[4]);
                break;
        }
    }

    private void MaxLevelOfTrack(float lv, int id)
    {
        if (lv >= _superRaceTrackManager.levels1 && id == 4)
        {
            upgradeName.text = "MAX";
            upgradeButtons[id].enabled = false;
            moneyValueOfButton[id].gameObject.SetActive(false);
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            StartWithNewParameters();
        }
    }
}
