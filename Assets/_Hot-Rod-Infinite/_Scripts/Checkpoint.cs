using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointManager _checkPointManager;

    [SerializeField] private GameObject canvasPoint;
    

    private void Start()
    {
        _checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameController.Instance.AddMoney(true, _checkPointManager.currentValueOfToAdd1);
        Instantiate(canvasPoint, (transform.position + Vector3.up+ Vector3.back *3 ), canvasPoint.transform.rotation);
    }

    
}
