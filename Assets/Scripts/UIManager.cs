using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("In-Game UI")]
    public GameObject inGameUI;
    public TMP_Text pointsText;
    public TMP_Text timeText;
    
    [Header("Game over UI")]
    public GameObject gameOverUI;
    public TMP_Text gameOverPointsText;
    
    public void UpdatePoints(int points)
    {
        pointsText.text = points.ToString();
    }

    public void UpdateTimeLeft(int timeLeft)
    {
        timeText.text = timeLeft.ToString();
    }

    // Used from Event Listeners to hide the in-game UI and show the Game Over UI
    public void GameOver(int points)
    {
        inGameUI.SetActive(false);
        gameOverPointsText.text = points.ToString();
        gameOverUI.SetActive(true);
    }

    // Used from Event Listeners to setup the in-game UI
    public void StartGame(int time)
    {
        UpdatePoints(0);
        UpdateTimeLeft(time);
        inGameUI.SetActive(true);
        gameOverUI.SetActive(false);
    }
}
