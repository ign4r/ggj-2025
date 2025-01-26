using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public int ID_receptor; // The receptor's color ID
    public GameObject explosionEffect; // Reference to your explosion prefab
    public Color explosionColor = Color.white; // Default explosion color

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the BubbleMovement script attached
        var bubble = other.gameObject.GetComponent<BubbleMovement>();
        if (bubble != null)
        {
            if (bubble.color_ID == ID_receptor)
            {
                TriggerExplosion(other.transform.position);
                print("ACERTASTE BITCH");

                // Call the AddPoints method to increase points for the corresponding color
                if (PointsManager.Instance != null) // Check if the Singleton is properly assigned
                {
                    PointsManager.Instance.AddPoints(bubble.color_ID); // Pass the bubble's color ID
                }
                else
                {
                    Debug.LogWarning("PointsManager Singleton is not assigned.");
                }
            }
            else
            {
                Destroy(bubble.gameObject);
            }
        }
    }

    private void TriggerExplosion(Vector3 position)
    {
        // Instantiate the explosion effect at the bubble's position
        if (explosionEffect != null)
        {
            GameObject explosionInstance = Instantiate(explosionEffect, position, Quaternion.identity);

            // Modify the particle system's start color
            var particleSystem = explosionInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                var mainModule = particleSystem.main;
                mainModule.startColor = explosionColor;
            }
        }
        else
        {
            Debug.LogWarning("ExplosionEffect is not assigned!");
        }
    }
}
