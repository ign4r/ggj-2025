using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  // Asegura que el GameObject tenga un Rigidbody
public class BubbleMovement : MonoBehaviour
{
    public float upSpeed = 2f;           // Velocidad de ascenso
    public float zigzagSpeed = 2f;       // Velocidad de zigzagueo (más suave)
    public float zigzagAmplitude = 0.5f; // Amplitud del zigzagueo (menor para movimientos suaves)
    private float timeOffset;            // Desfase temporal para el zigzagueo
    private Rigidbody rb;
    public int color_ID;// El Rigidbody para las físicas

    void Start()
    {
        // Asegurarse de que el Rigidbody esté asignado
        rb = GetComponent<Rigidbody>();

        // Inicializa el desfase aleatorio para el zigzagueo
        timeOffset = Random.Range(0f, Mathf.PI * 2); // Desfase para un zigzagueo más natural
    }

    void FixedUpdate()
    {
        // Movimiento hacia arriba con la física (manteniendo la velocidad en Y)
        rb.velocity = new Vector3(rb.velocity.x, upSpeed, 0f);  // Fuerza en Z puesta a 0

        // Movimiento zigzagueante natural usando seno para un movimiento más suave
        float zigzagMovement = Mathf.Sin(Time.time * zigzagSpeed + timeOffset) * zigzagAmplitude;

        // Aplica el zigzagueo al Rigidbody en el eje X
        rb.velocity = new Vector3(zigzagMovement, rb.velocity.y, 0f);  // Mantiene la velocidad en Z en 0
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            Destroy(gameObject);
        }
    }
 
}

