using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float rotationSpeed = 200f; // Velocidad de rotación
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Movimiento del submarino basado en la entrada del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        if (direction.magnitude > 0.1f)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        // Rotación del submarino
        if (horizontal != 0)
        {
            float rotationAngle = horizontal > 0 ? -90f : 90f;
            rb.rotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Detener la velocidad del submarino cuando salga de la colisión
        if (collision.gameObject.CompareTag("Bubble")) // Asegúrate de usar el tag correcto
        {
            rb.velocity = Vector3.zero;
        }
    }
}
