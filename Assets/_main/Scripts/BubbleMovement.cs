using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  // Ensures the GameObject has a Rigidbody
public class BubbleMovement : MonoBehaviour
{
    public float upSpeed = 2f;           // Ascend speed
    public float zigzagSpeed = 2f;       // Zigzag speed (smooth)
    public float zigzagAmplitude = 0.5f; // Zigzag amplitude (lower for smoother movement)
    private float timeOffset;            // Time offset for the zigzag
    private Rigidbody rb;
    public int color_ID;                 // Color ID for the bubble
    public float force;

    private bool isZigzagging = true;    // Zigzag state

    public float wrapOffset = 0.1f;      // Offset for the PacMan effect
    private float screenWidth;           // Screen width in world units

    void Start()
    {
        // Ensure the Rigidbody is assigned
        rb = GetComponent<Rigidbody>();

        // Initialize random time offset for natural zigzag
        timeOffset = Random.Range(0f, Mathf.PI * 2); // Offset for a more natural zigzag movement

        // Get the screen width in world units (not in screen pixels)
        screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }

    void FixedUpdate()
    {
        // Apply vertical movement (upwards)
        rb.velocity = new Vector3(rb.velocity.x, upSpeed, 0f);  // Set Z-axis velocity to 0

        // Apply smooth zigzag movement using sine for natural motion
        float zigzagMovement = Mathf.Sin(Time.time * zigzagSpeed + timeOffset) * zigzagAmplitude;

        // Apply the zigzag to the Rigidbody's X-axis velocity
        rb.velocity = new Vector3(zigzagMovement, rb.velocity.y, 0f);  // Keep Z-axis velocity at 0

        // Apply the PacMan effect to wrap the position horizontally
        ApplyPacManEffect();
    }

    void ApplyPacManEffect()
    {
        // Get the current position of the bubble
        Vector3 position = transform.position;

        // Check if the bubble has moved past the right edge
        if (position.x > screenWidth / 2 + wrapOffset)
        {
            position.x = -screenWidth / 2 - wrapOffset; // Wrap to the left side
        }
        // Check if the bubble has moved past the left edge
        else if (position.x < -screenWidth / 2 - wrapOffset)
        {
            position.x = screenWidth / 2 + wrapOffset; // Wrap to the right side
        }

        // Update the bubble's position
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Dead"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Dead"))
        {
            Destroy(gameObject);
        }
    }
}
