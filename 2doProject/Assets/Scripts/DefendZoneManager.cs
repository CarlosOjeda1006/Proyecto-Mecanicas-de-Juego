using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefendZoneManager : MonoBehaviour
{
    public Slider invasionSlider;
    public int maxEnemiesInZone = 5;

    public float timeToWin = 60f; // Tiempo para ganar en segundos
    private float timer;
    public Text timerText; // Texto UI para mostrar el tiempo restante

    private bool gameEnded = false;

    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void Start()
    {
        invasionSlider.maxValue = maxEnemiesInZone;
        invasionSlider.value = 0;

        timer = timeToWin;
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    private void Update()
    {
        if (gameEnded) return;

        enemiesInZone.RemoveAll(enemy => enemy == null);
        UpdateUI();

        // Temporizador de victoria
        timer -= Time.deltaTime;
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();

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
        if (enemiesInZone.Count >= maxEnemiesInZone)
        {
            gameEnded = true;
            Debug.Log("¡Has perdido! La zona fue invadida.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        Debug.Log("¡Has ganado! Defendiste la zona con éxito.");
        // Aquí puedes cargar otra escena o mostrar una pantalla de victoria
        // Por ejemplo: SceneManager.LoadScene("WinScene");
    }
}
