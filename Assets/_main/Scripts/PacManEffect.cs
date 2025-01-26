using UnityEngine;

public class PacManEffect : MonoBehaviour
{
    public float wrapOffset = 0.1f; // How far from the edge the character should wrap
    private float screenWidth;

    void Start()
    {
        // Get screen width in world units (not screen pixels)
        screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }

    void Update()
    {
        // Get the current position of the character
        Vector3 position = transform.position;

        // Check if the character has moved past the right edge
        if (position.x > screenWidth / 2 + wrapOffset)
        {
            position.x = -screenWidth / 2 - wrapOffset; // Wrap to the left side
        }
        // Check if the character has moved past the left edge
        else if (position.x < -screenWidth / 2 - wrapOffset)
        {
            position.x = screenWidth / 2 + wrapOffset; // Wrap to the right side
        }

        // Update the position of the character
        transform.position = position;
    }
}
