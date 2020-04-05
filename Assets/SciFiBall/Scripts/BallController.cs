using UnityEngine;

public class BallController : MonoBehaviour {

    private Rigidbody rb;

    public float speed;
    public float boostSpeed;
    public float jumpForce;

    public Transform spawnPoint;
    public Transform mainCam;
    public Transform transformContainer;

    private bool isGrounded = false;
    private Vector3 normalOfCollidedSurface;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        // Handle jump Calculations
        Vector3 jumpCalculation = (normalOfCollidedSurface * jumpForce + (transformContainer.up * jumpForce));
        Vector3 appliedJumpForce = isGrounded && Input.GetButtonDown("Jump") ? jumpCalculation : Vector3.zero;

        // Apply jump velocity to ball
        rb.AddForce(appliedJumpForce, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        transformContainer.position = mainCam.position;
        //transformContainer.rotation = mainCam.rotation;
        transformContainer.rotation = Quaternion.Euler(0, mainCam.rotation.eulerAngles.y, 0);

        // Get input axis values
        Vector3 horizontalSpeed = Input.GetAxis("Horizontal") * transformContainer.right;
        Vector3 forwardSpeed = Input.GetAxis("Vertical") * transformContainer.forward;
        float addedBoost = Input.GetAxis("Boost") * boostSpeed;

        // Combine axes and multiply them by speed values
        Vector3 appliedForce = (horizontalSpeed + forwardSpeed) * (speed + addedBoost);

        // Apply force to ball
        rb.AddForce(appliedForce, ForceMode.Acceleration);

    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
        normalOfCollidedSurface = Vector3.Normalize(transform.position - other.ClosestPoint(transform.position));
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    public void Reset()
    {
        transform.position = spawnPoint.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
