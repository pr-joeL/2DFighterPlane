using UnityEngine;
using TMPro; // Important for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Assign this in the Inspector
    private int currentScore = 0;

    void Start()
    {
        UpdateScoreText(); // Initialize the displayed score
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
