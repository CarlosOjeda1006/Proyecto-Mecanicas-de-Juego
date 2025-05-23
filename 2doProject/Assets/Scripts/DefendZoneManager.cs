using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DefendZoneManager : MonoBehaviour
{
    public Slider invasionSlider;
    public int maxEnemiesInZone = 5;

    public float timeToWin = 60f; // Tiempo para ganar en segundos
    private float timer;
    public TextMeshProUGUI timerText;

    private bool gameEnded = false;

    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void Start()
    {
        invasionSlider.maxValue = maxEnemiesInZone;
        invasionSlider.value = 0;

        timer = timeToWin;
        UpdateTimerText();
    }

    private void Update()
    {
        if (gameEnded) return;

        enemiesInZone.RemoveAll(enemy => enemy == null);
        UpdateUI();

        // Temporizador de victoria
        timer -= Time.deltaTime;
        UpdateTimerText();

        if (timer <= 0f)
        {
            WinGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesInZone.Contains(other.gameObject))
        {
            enemiesInZone.Add(other.gameObject);
            UpdateUI();
            CheckLoseCondition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInZone.Remove(other.gameObject);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        invasionSlider.value = enemiesInZone.Count;
    }

    private void CheckLoseCondition()
    {
        if (gameEnded) return;

        if (enemiesInZone.Count >= maxEnemiesInZone)
        {
            gameEnded = true;
            Debug.Log("¡Has perdido! La zona fue invadida.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        Debug.Log("¡Has ganado! Defendiste la zona con éxito.");
        // Llama a la victoria global
        EnemyProgressManager.instance?.WinGame();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
