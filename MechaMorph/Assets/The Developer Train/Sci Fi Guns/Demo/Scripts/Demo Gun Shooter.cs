using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{
    public class DemoGunShooter : MonoBehaviour
    {


        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Gun[] guns = GameObject.FindObjectsByType<Gun>(FindObjectsSortMode.InstanceID);
                foreach (var gun in guns)
                {
                    gun.Shoot();
                }
            }
        }
    }
}