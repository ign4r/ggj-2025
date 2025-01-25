using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public int ID_receptor;
    public GameObject explosionEffect; // Reference to your explosion prefab
    public Color explosionColor = Color.white; // Default explosion color

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the BubbleMovement script attached
        var bubble = collision.gameObject.GetComponent<BubbleMovement>();
        if (bubble != null)
        {
            if (bubble.color_ID == ID_receptor)
            {
                print("ACERTASTE BITCH");
            }
            else
            {
                // Trigger explosion
                TriggerExplosion(collision.transform.position);

                // Destroy the bubble
                Destroy(bubble.gameObject);
                print("SOS UN FRACA");
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
