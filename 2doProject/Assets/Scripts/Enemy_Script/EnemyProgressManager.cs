using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

        // Solo ganar por kills si estás en Nivel_1
        if (enemiesKilled >= totalEnemiesToKill && SceneManager.GetActiveScene().name == "Nivel_1")
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Nivel_2")
        {
            Debug.Log("¡Juego completado! Regresando al menú final...");
            Object.FindFirstObjectByType<SceneFader>().FadeAndLoadScene("MenuVictoria");
        }
        else
        {
            Debug.Log("¡Nivel completado! Cargando siguiente nivel...");
            Object.FindFirstObjectByType<SceneFader>().FadeAndLoadScene("Nivel_2");
        }
    }
}
