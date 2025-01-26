using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    public Text pointsText; // Reference to the UI Text component
    public PointsManager pointsManager;

    private void Start()
    {
        pointsManager = FindObjectOfType<PointsManager>();
        UpdatePointsUI();
    }

    private void Update()
    {
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        pointsText.text = $"Points: {pointsManager.currentPoints}";
    }
}
