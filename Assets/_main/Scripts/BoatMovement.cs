using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Asegura que el objeto tenga un Rigidbody 3D
public class BoatMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento
    public float rotationSpeed = 200f; // Velocidad de rotaci�n en Y
    public float decelerationRate = 1f; // Tasa de desaceleraci�n cuando se deja de empujar las burbujas
    private Rigidbody rb; // Referencia al Rigidbody del barco
    private float currentRotationY = -90f; // Rotaci�n inicial en Y
    private bool isDecelerating = false; // Controla si estamos en proceso de desaceleraci�n

    void Awake()
    {
        // Obtener la referencia al Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener entrada del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear un vector de direcci�n en el plano X-Y
        Vector3 direction = new Vector3(horizontal, vertical, 0);

        // Mover el barco utilizando Rigidbody para aplicar la f�sica
        if (direction.magnitude > 0.1f)
        {
            // Asegurarse de que la direcci�n sea normalizada
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
        }

        // Cambiar rotaci�n solo si hay entrada horizontal
        if (horizontal != 0)
        {
            currentRotationY = horizontal > 0 ? -90f : 90f; // Girar a la derecha o a la izquierda
        }

        // Aplicar la rotaci�n en Y utilizando Rigidbody
        rb.rotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica si el submarino sale de una colisi�n con un objeto con el tag "Bubble"
        if (collision.gameObject.CompareTag("Bubble") && rb.velocity.y > 0 && !isDecelerating)
        {
            // Iniciar la corutina de desaceleraci�n solo una vez
            StartCoroutine(DecelerateUpwardMovement());
        }
    }

    // Corutina que desacelera gradualmente el movimiento hacia arriba
    IEnumerator DecelerateUpwardMovement()
    {
        print("holaaa");    
        // Marcamos que estamos en el proceso de desaceleraci�n
        isDecelerating = true;

        // Mientras el submarino se est� moviendo hacia arriba, desaceleramos gradualmente
        while (rb.velocity.y > 0)
        {
            float newYVelocity = Mathf.MoveTowards(rb.velocity.y, 0, decelerationRate * Time.deltaTime);
            rb.velocity = new Vector3(rb.velocity.x, newYVelocity, rb.velocity.z);

            yield return null; // Espera un frame antes de continuar con el siguiente ciclo
        }

        // Cuando termine la desaceleraci�n, desmarcamos el flag
        isDecelerating = false;
    }
}
