using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefendZoneManager : MonoBehaviour
{
    public Slider invasionSlider;
    public Text timerText;           // Texto para mostrar el tiempo transcurrido
    public int maxEnemiesInZone = 5;
    public float timeToWin = 60f;    // Tiempo que debes defender para ganar (en segundos)

    private List<GameObject> enemiesInZone = new List<GameObject>();
    private float timer = 0f;
    private bool hasWon = false;

    private void Start()
    {
        invasionSlider.maxValue = maxEnemiesInZone;
        invasionSlider.value = 0;
        UpdateTimerUI();
    }

    private void Update()
    {
        // Limpiar lista de enemigos nulos
        enemiesInZone.RemoveAll(enemy => enemy == null);

        // Actualizar barra de invasión
        UpdateUI();

        // Si perdiste, no seguir contando el tiempo
        if (hasWon) return;

        // Incrementar temporizador
        timer += Time.deltaTime;
        UpdateTimerUI();

        // Revisar si ganaste
        if (timer >= timeToWin)
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

    void UpdateUI()
    {
        invasionSlider.value = enemiesInZone.Count;
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";
    }

    void CheckLoseCondition()
    {
        if (enemiesInZone.Count >= maxEnemiesInZone)
        {
            Debug.Log("¡Has perdido! La zona fue invadida.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void WinGame()
    {
        hasWon = true;
        Debug.Log("¡Has ganado! Has defendido la zona el tiempo necesario.");
        // Aquí puedes cargar una escena de victoria o mostrar un UI de ganar
        // Ejemplo: SceneManager.LoadScene("WinScene");
    }
}
