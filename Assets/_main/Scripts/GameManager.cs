using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int timelapse = 120;        // Contador de tiempo
    public int burstBubbles = 0;        // Contador de burbujas explotadas
    public int savedBubbles = 0;        // Contador de burbujas salvadas

    void Awake()
    {
        // Asegurarse de que solo haya una instancia de GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Aquí puedes incrementar el contador de tiempo (Timelapse)
        if (!IsGamePaused())  // Solo contar el tiempo si el juego no está en pausa
        {
            timelapse += Time.deltaTime;  // Sumar el tiempo que ha pasado
        }

        // Aquí podrías agregar otras actualizaciones que dependen del juego
    }

    // Métodos para incrementar los contadores
    public void IncrementBurstBubbles()
    {
        burstBubbles++;
        Debug.Log("Bursts: " + burstBubbles);
    }

    public void IncrementSavedBubbles()
    {
        savedBubbles++;
        Debug.Log("Saved: " + savedBubbles);
    }

    public void ResetCounters()
    {
        timelapse = 0;
        burstBubbles = 0;
        savedBubbles = 0;
    }

    public bool IsGamePaused()
    {
        return Time.timeScale == 0;
    }

    public void DisplayCounters()
    {
        Debug.Log("Timelapse: " + timelapse);
        Debug.Log("BurstBubbles: " + burstBubbles);
        Debug.Log("SavedBubbles: " + savedBubbles);
    }
}
