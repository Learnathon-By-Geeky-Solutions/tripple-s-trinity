using UnityEngine;

namespace TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui
{
    public class Billboard : MonoBehaviour
    {
        private Transform _cam;

        private void Start()
        {
            if (Camera.main != null) _cam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.forward = _cam.forward;
        }
    }
}