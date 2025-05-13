using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("¡Nivel completado! Cargando siguiente nivel...");

        Object.FindFirstObjectByType<SceneFader>().FadeAndLoadScene("Nivel2");
    }
    


}

