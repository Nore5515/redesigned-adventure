using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI scoreText;
    [SerializeField]
    public PlayerHandler playerHandler;
    
    void OnEnable()
    {
        scoreText.text = playerHandler.playerStats.score.ToString();
    }

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
