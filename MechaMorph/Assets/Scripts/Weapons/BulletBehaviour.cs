using UnityEngine;

namespace TrippleTrinity.MechaMorph
{
    public class BulletBehaviour : MonoBehaviour
    {
        public float bulletSpeed = 20f;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * bulletSpeed *  Time.deltaTime);
        }

        private void OnEnable()
        {
            Destroy(gameObject, 2f);
        }
    }
}
