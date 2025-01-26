using System.Collections;
using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class PacManEffect : MonoBehaviour
{
    public SharkMovement sharkM; // Esto debería ser el componente que controla al tiburón
    public bool isShark = false; // Indicador de si este es el tiburón o no
    public float wrapOffset = 0.1f; // Cómo de lejos del borde se debe hacer el wrap
    private float screenWidth;
    public float wrapDelay = 1f; // Tiempo de espera antes de hacer el wrap solo para el tiburón
    public bool isCoroutineRunning = false;

    // Event to notify when the entity wraps around
    public UnityEvent OnWrap;

    void Start()
    {
        // Obtener el ancho de la pantalla en unidades del mundo
        screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;

        // Asegurarse de que el UnityEvent esté inicializado
        if (OnWrap == null)
        {
            OnWrap = new UnityEvent();
        }
    }

    void Update()
    {
        // Obtener la posición actual del personaje
        Vector3 position = transform.position;

        // Comprobar si el personaje ha pasado el borde derecho
        if (position.x > screenWidth / 2 + wrapOffset)
        {
            if (isShark)
                StartCoroutine(WrapToLeftSide()); // Iniciar la coroutine para esperar antes de hacer el wrap solo si es el tiburón
            else
                WrapImmediatelyLeft(); // Hacer el wrap inmediato si no es el tiburón
        }
        // Comprobar si el personaje ha pasado el borde izquierdo
        else if (position.x < -screenWidth / 2 - wrapOffset)
        {
            if (isShark)
                StartCoroutine(WrapToRightSide()); // Iniciar la coroutine para esperar antes de hacer el wrap solo si es el tiburón
            else
                WrapImmediatelyRight(); // Hacer el wrap inmediato si no es el tiburón
        }
    }

    // Coroutine para manejar el wrap después de un retraso (solo para el tiburón)
    private IEnumerator WrapToLeftSide()
    {
        if (!isCoroutineRunning) // Aseguramos que solo se ejecute una vez
        {
            isCoroutineRunning = true; // Marcamos que la coroutine está en ejecución
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
            isCoroutineRunning = true; // Marcamos que la coroutine está en ejecución
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

    // Método para manejar la lógica de wrap
    private void TriggerWrap()
    {
        Debug.Log("Entity has wrapped around the screen!"); // Mostrar mensaje en consola
        OnWrap.Invoke(); // Activar el UnityEvent
    }

    // Método booleano para comprobar si el personaje está en la zona de efecto Pac-Man
    public bool IsInPacManEffectZone()
    {
        Vector3 position = transform.position;

        // Comprobar si el personaje está fuera de los límites de la pantalla (a punto de hacer el wrap)
        if (position.x > screenWidth / 2 + wrapOffset || position.x < -screenWidth / 2 - wrapOffset)
        {
            return true;
        }

        return false;
    }
}
