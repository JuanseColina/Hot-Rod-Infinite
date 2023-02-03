using UnityEngine;
using Dreamteck.Splines;

public class TurboTrigger : MonoBehaviour
{
    [SerializeField] private bool activated;
    [SerializeField] private TouchControl _touchControl;
    private void OnTriggerStay(Collider other)
    {
        if (activated)
        {
            //var turbo = other.GetComponent<CarCtrl>();
            //turbo.TurboActive(_touchControl.currentSpeed1 * 2);
        }
        else
        {
            var turbo = other.GetComponent<CarCtrl>();
            //turbo.TurboActive(_touchControl.currentSpeed1 / 2);
        }
    }
}
