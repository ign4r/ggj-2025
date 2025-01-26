using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int currentPoints { get; private set; } = 0;

    // Method to add points
    public void AddPoints(int points)
    {
        currentPoints += points;
        Debug.Log($"Points Added: {points}. Total Points: {currentPoints}");
    }

    // Method to reset points
    public void ResetPoints()
    {
        currentPoints = 0;
        Debug.Log("Points reset.");
    }
}
