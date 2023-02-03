using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    [SerializeField] private Button nextLevelButton;
    //[SerializeField] private Image newTrackImageUnlock;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private GameObject text;

    private void Start()
    {
        TweeningText();
    }

    private void Update()
    {
        moneyText.text = GameController.Instance.money1.ToString("F1");
    }

    public void UnlockNewTrack(bool toActive)
    {
        nextLevelButton.enabled = toActive;
        //newTrackImageUnlock.enabled = toActive;
    }

    public void TweeningText()
    {
        LeanTween.scale(text, Vector3.one * 1.2f, 1f).setLoopPingPong(-1);
    }
    
}
