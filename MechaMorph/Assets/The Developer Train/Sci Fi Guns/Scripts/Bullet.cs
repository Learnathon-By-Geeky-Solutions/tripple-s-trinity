using UnityEngine;

namespace TheDeveloperTrain.SciFiGuns
{

    public class Bullet : MonoBehaviour
    {
        [HideInInspector]
        public float speed = 1.0f;
        void Start()
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            Destroy(gameObject, 5f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
        }
    }
}