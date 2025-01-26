using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance { get; private set; }

    // Points for different colors
    public int redPoints = 0;
    public int bluePoints = 0;
    public int greenPoints = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object when changing scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    // Method to add points for a specific color
    public void AddPoints(int colorID)
    {
        switch (colorID)
        {
            case 0: // Red color
                redPoints += 1;
                break;
            case 1: // Blue color
                bluePoints += 1;
                break;
            case 2: // Green color
                greenPoints += 1;
                break;
            default:
                Debug.LogWarning("Unknown color ID");
                break;
        }
        Debug.Log($"Points Added. Total Points: Red: {redPoints}, Blue: {bluePoints}, Green: {greenPoints}");
    }

    // Method to reset all points
    public void ResetPoints()
    {
        redPoints = 0;
        bluePoints = 0;
        greenPoints = 0;
        Debug.Log("All points reset.");
    }
}
