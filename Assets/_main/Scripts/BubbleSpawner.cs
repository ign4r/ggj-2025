using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject[] bubblePrefab;      // Array de prefabs de burbujas
    public Transform[] spawnPositions;    // Array de posiciones de spawn
    public float spawnInterval = 2f;      // Intervalo de tiempo entre cada spawn


    void Start()
    {
        // Iniciar la corutina que maneja el spawn de las burbujas
        StartCoroutine(SpawnBubbles());
    }

    // Coroutine que maneja el spawn de las burbujas
    IEnumerator SpawnBubbles()
    {
        while (true) // Loop infinito para spawnear burbujas continuamente
        {
            // Selecciona una posición aleatoria del array de posiciones
            int randomPositionIndex = Random.Range(0, spawnPositions.Length);
            Transform spawnPosition = spawnPositions[randomPositionIndex];

            // Selecciona aleatoriamente un prefab de burbuja
            int randomBubbleIndex = Random.Range(0, bubblePrefab.Length);
            GameObject selectedBubblePrefab = bubblePrefab[randomBubbleIndex];

            // Instancia el prefab en la posición aleatoria seleccionada
            GameObject bubble = Instantiate(selectedBubblePrefab, spawnPosition.position, Quaternion.Euler(0, 90, 0));
           

            // Espera el tiempo definido antes de hacer otro spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
