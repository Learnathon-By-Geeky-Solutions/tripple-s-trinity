using UnityEngine;

public class BallController : MonoBehaviour
{
    public float movementSpeed = 10f;
    private Rigidbody rb;

    private float _cachedHorizontal;
    private float _cachedVertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Cache input values in Update()
        _cachedHorizontal = Input.GetAxis("Horizontal");
        _cachedVertical = Input.GetAxis("Vertical");
    }

    // FixedUpdate is called at a fixed time step
    void FixedUpdate()
    {
        // Use cached input values for physics
        Vector3 movement = new Vector3(_cachedHorizontal, 0, _cachedVertical);
        rb.AddForce(movement * movementSpeed);
    }
}