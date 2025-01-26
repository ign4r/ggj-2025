using UnityEngine;

public class FanEffect : MonoBehaviour
{
    public float force = 10f; // Fuerza de dispersión
    public float radius = 5f; // Radio en el que se aplica la fuerza
    private bool forceApplied = false; // Para asegurar que solo se aplica la fuerza una vez

    private void Start()
    {
        // Inicialización si se necesita aplicar la fuerza inmediatamente al comenzar

    }

    public void ApplyFanForce()
    {


        // Encuentra todos los colliders en el radio de la explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            // Solo aplicar la fuerza si el objeto tiene un Rigidbody y tiene el tag "Bubble"
            if (rb != null && collider.CompareTag("Bubble"))
            {
                // Obtiene la dirección hacia donde está mirando el ventilador (forward)
                Vector3 forceDirection = -transform.forward.normalized; // Dirección contraria al forward del objeto

                // Aplica la fuerza en la dirección contraria
                rb.AddForce(forceDirection * force, ForceMode.Impulse);

                // Marca que la fuerza ha sido aplicada
                forceApplied = true;
            }
        }
    }

    // Llamada para aplicar la fuerza a los objetos cuando se activa el ventilador (por ejemplo, con un botón o evento)
    private void OnTriggerEnter(Collider other)
    {
        // Solo aplica la fuerza si el objeto tiene un Rigidbody, el tag "Bubble" y si no se ha aplicado aún
        ApplyFanForce();
    }
}
