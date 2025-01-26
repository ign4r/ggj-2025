using System.Collections;
using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class PacManEffect : MonoBehaviour
{
    public SharkMovement sharkM; // Esto deber�a ser el componente que controla al tibur�n
    public bool isShark = false; // Indicador de si este es el tibur�n o no
    public float wrapOffset = 0.1f; // C�mo de lejos del borde se debe hacer el wrap
    private float screenWidth;
    public float wrapDelay = 1f; // Tiempo de espera antes de hacer el wrap solo para el tibur�n
    public bool isCoroutineRunning = false;

    // Event to notify when the entity wraps around
    public UnityEvent OnWrap;

    void Start()
    {
        // Obtener el ancho de la pantalla en unidades del mundo
        screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;

        // Asegurarse de que el UnityEvent est� inicializado
        if (OnWrap == null)
        {
            OnWrap = new UnityEvent();
        }
    }

    void Update()
    {
        // Obtener la posici�n actual del personaje
        Vector3 position = transform.position;

        // Comprobar si el personaje ha pasado el borde derecho
        if (position.x > screenWidth / 2 + wrapOffset)
        {
            if (isShark)
                StartCoroutine(WrapToLeftSide()); // Iniciar la coroutine para esperar antes de hacer el wrap solo si es el tibur�n
            else
                WrapImmediatelyLeft(); // Hacer el wrap inmediato si no es el tibur�n
        }
        // Comprobar si el personaje ha pasado el borde izquierdo
        else if (position.x < -screenWidth / 2 - wrapOffset)
        {
            if (isShark)
                StartCoroutine(WrapToRightSide()); // Iniciar la coroutine para esperar antes de hacer el wrap solo si es el tibur�n
            else
                WrapImmediatelyRight(); // Hacer el wrap inmediato si no es el tibur�n
        }
    }

    // Coroutine para manejar el wrap despu�s de un retraso (solo para el tibur�n)
    private IEnumerator WrapToLeftSide()
    {
        if (!isCoroutineRunning) // Aseguramos que solo se ejecute una vez
        {
            isCoroutineRunning = true; // Marcamos que la coroutine est� en ejecuci�n
            float randomDelay = Random.Range(1f, 3f); // Genera un delay aleatorio entre 1 y 3 segundos
            print(randomDelay);
            yield return new WaitForSeconds(randomDelay); // Esperamos el tiempo aleatorio
            Vector3 position = transform.position;
            position.x = -screenWidth / 2 - wrapOffset; // Realizamos el wrap a la izquierda
            transform.position = position;
            TriggerWrap(); // Activamos el evento de wrap
            isCoroutineRunning = false; // Marcamos que la coroutine ha terminado
        }
    }

    private IEnumerator WrapToRightSide()
    {
        if (!isCoroutineRunning) // Aseguramos que solo se ejecute una vez
        {
            isCoroutineRunning = true; // Marcamos que la coroutine est� en ejecuci�n
            float randomDelay = Random.Range(1f, 3f); // Genera un delay aleatorio entre 1 y 3 segundos
            print(randomDelay);
            yield return new WaitForSeconds(randomDelay); // Esperamos el tiempo aleatorio
            Vector3 position = transform.position;
            position.x = screenWidth / 2 + wrapOffset; // Realizamos el wrap a la derecha
            transform.position = position;
            TriggerWrap(); // Activamos el evento de wrap
            isCoroutineRunning = false; // Marcamos que la coroutine ha terminado
        }
    }

    // Wrap inmediato para otros personajes
    private void WrapImmediatelyLeft()
    {
        Vector3 position = transform.position;
        position.x = -screenWidth / 2 - wrapOffset; // Wrap inmediato al lado izquierdo
        transform.position = position;

        TriggerWrap(); // Activar el evento
    }

    private void WrapImmediatelyRight()
    {
        Vector3 position = transform.position;
        position.x = screenWidth / 2 + wrapOffset; // Wrap inmediato al lado derecho
        transform.position = position;

        TriggerWrap(); // Activar el evento
    }

    // M�todo para manejar la l�gica de wrap
    private void TriggerWrap()
    {
        Debug.Log("Entity has wrapped around the screen!"); // Mostrar mensaje en consola
        OnWrap.Invoke(); // Activar el UnityEvent
    }

    // M�todo booleano para comprobar si el personaje est� en la zona de efecto Pac-Man
    public bool IsInPacManEffectZone()
    {
        Vector3 position = transform.position;

        // Comprobar si el personaje est� fuera de los l�mites de la pantalla (a punto de hacer el wrap)
        if (position.x > screenWidth / 2 + wrapOffset || position.x < -screenWidth / 2 - wrapOffset)
        {
            return true;
        }

        return false;
    }
}
