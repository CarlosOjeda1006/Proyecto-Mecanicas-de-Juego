using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyProgressManager : MonoBehaviour
{
    public static EnemyProgressManager instance;

    public Slider progressBar;
    public int totalEnemiesToKill = 20;
    private int enemiesKilled = 0;

    public TextMeshProUGUI killCounterText;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        progressBar.maxValue = totalEnemiesToKill;
        progressBar.value = 0;
        killCounterText.text = "0 / " + totalEnemiesToKill;
    }

    public void RegisterKill()
    {
        enemiesKilled++;
        progressBar.value = enemiesKilled;
        killCounterText.text = enemiesKilled + " / " + totalEnemiesToKill;
        if (enemiesKilled >= totalEnemiesToKill)
        {
            Debug.Log("¡Has ganado la partida!");
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("Siguiente nivel");
        GameManager.instance.PlayerDeath();
    }
}

