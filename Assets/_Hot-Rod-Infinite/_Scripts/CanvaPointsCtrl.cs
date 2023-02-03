using TMPro;
using UnityEngine;

public class CanvaPointsCtrl : MonoBehaviour
{
    private CheckPointManager _checkPointManager;
    [SerializeField] private TextMeshProUGUI pointText;

    private void Awake()
    {
        _checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(FindObjectOfType<Camera>().transform);
        pointText.text = "+" + _checkPointManager.currentValueOfToAdd1.ToString("F1");
        LeanTween.moveLocalY(gameObject, transform.position.y + 2 , .5f).setOnComplete((() =>
        {
            Destroy(this.gameObject);
        }));
    }
}
