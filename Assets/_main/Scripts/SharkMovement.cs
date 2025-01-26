using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SharkMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public bool moveRight = true; // Direcci�n del movimiento (true para derecha, false para izquierda)
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Movimiento del tibur�n basado en la entrada del jugador
        // Movimiento del tibur�n en una direcci�n constante
        float horizontal = moveRight ? -1f : 1f;

        // Movimiento solo horizontal
        Vector3 direction = new Vector3(horizontal, 0, 0).normalized;

        rb.velocity = direction * speed;
    }


}

