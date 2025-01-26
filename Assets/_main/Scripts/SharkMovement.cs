using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SharkMovement : MonoBehaviour
{
    public float screenWidth; // Aseg�rate de definir este valor correctamente
    public float wrapOffset = 0.1f; // C�mo de lejos est� el personaje del borde para hacer el wrap
    public float lowerLimit = -1.84f; // L�mite inferior
    public float upperLimit = 4.9f;  // L�mite superior
    public float speed = 5f; // Velocidad de movimiento horizontal
    public bool moveRight = true; // Direcci�n del movimiento (true para derecha, false para izquierda)

    private Rigidbody rb;

    // Variables de control de movimiento vertical
    public float verticalSpeed = 0.1f; // Velocidad vertical progresiva (ajustable)
    private bool movingUp = false; // Determina si el tibur�n est� subiendo
    private bool movingDown = false; // Determina si el tibur�n est� bajando

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        movingDown = true;
    }
    private void Update()
    {
        // Movimiento horizontal del tibur�n
        float horizontal = moveRight ? -1f : 1f;
        Vector3 direction = new Vector3(horizontal, 0, 0).normalized;
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, 0);

        // Movimiento vertical progresivo (cuando toca los l�mites)
        if (transform.position.y <= lowerLimit && !movingUp)
        {
            print("UP");
            movingUp = true;  // El tibur�n empieza a subir
            movingDown = false;  // Detenemos el movimiento hacia abajo
        }
        else if (transform.position.y >= upperLimit && !movingDown)
        {
            print("DOWN");
            movingDown = true;  // El tibur�n empieza a bajar
            movingUp = false;  // Detenemos el movimiento hacia arriba
        }

        // Movimiento hacia arriba (cuando est� en el l�mite inferior)
        if (movingUp)
        {
            rb.velocity = new Vector3(rb.velocity.x, verticalSpeed, 0);
        }
        // Movimiento hacia abajo (cuando est� en el l�mite superior)
        else if (movingDown)
        {
            rb.velocity = new Vector3(rb.velocity.x, -verticalSpeed, 0);
        }
    }
}


