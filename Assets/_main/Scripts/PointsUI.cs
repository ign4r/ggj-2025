using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    public Text redPointsText; // Reference to the UI Text component for red points
    public Text bluePointsText; // Reference to the UI Text component for blue points
    public Text greenPointsText; // Reference to the UI Text component for green points

    private void Start()
    {
        // Ensure the PointsManager Singleton is available
        if (PointsManager.Instance == null)
        {
            Debug.LogError("PointsManager Singleton not found!");
        }
        else
        {
            UpdatePointsUI(); // Update points on UI at the start
        }
    }

    private void Update()
    {
        // Always check if the Singleton exists before updating points
        if (PointsManager.Instance != null)
        {
            UpdatePointsUI();
        }
        else
        {
            Debug.LogError("PointsManager is not assigned.");
        }
    }

    private void UpdatePointsUI()
    {
        if (PointsManager.Instance != null)
        {
            // Update text for each color's points
            redPointsText.text = $"{PointsManager.Instance.redPoints}/{maxPoints}";
            bluePointsText.text = $"{PointsManager.Instance.bluePoints}/{maxPoints}";
            greenPointsText.text = $"{PointsManager.Instance.greenPoints}/{maxPoints}";
        }
    }
}
