using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public ZombieSpawner zombieSpawner;
    private int score;
    public bool IsGameOver { get; private set; }

    public void Start()
    {
        IsGameOver = false;

        var findPlayer = GameObject.FindWithTag("Player");
        var playerHealth = findPlayer.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.OnDeath += EndGame;
        }
        
    }

    public void AddScore(int add)
    {
        score += add;
        uiManager.SetUpdateScore(score);
    }

    public void EndGame()
    {
        IsGameOver = true;
        uiManager.SetActiveGameOverUi(true);
        zombieSpawner.enabled = false;
    }
}
