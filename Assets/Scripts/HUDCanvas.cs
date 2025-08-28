using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour
{
    public Text scoreText;
    public Text ammoText;
    public Text enemyWave;
    public GameObject gameOver;

    public LivingEntity entity;

    private int score;

    private void Start()
    {
        entity.OnDeath += () => gameOver.SetActive(true);
    }

    public void ChangeScore(int amout)
    {
        score += amout;
        scoreText.text = $"Score : {score}";
    } 

    public void ChangeAmmo(int ammo , int maxAmmo)
    {
        ammoText.text = $"{ammo}/{maxAmmo}";
    }

    public void ChangeWave(int wave, int enemyCount)
    {
        enemyWave.text = $"Wave : {wave}\nEnemy Left : {enemyCount}";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
