using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefendZoneManager : MonoBehaviour
{
    public Slider invasionSlider;
    public int maxEnemiesInZone = 5;

    private List<GameObject> enemiesInZone = new List<GameObject>();

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

    void Update()
    {
        enemiesInZone.RemoveAll(enemy => enemy == null);
        UpdateUI();
    }

    void UpdateUI()
    {
        invasionSlider.value = enemiesInZone.Count;
    }

    void CheckLoseCondition()
    {
        if (enemiesInZone.Count >= maxEnemiesInZone)
        {
            Debug.Log("¡Has perdido! La zona fue invadida.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Start()
    {
        invasionSlider.maxValue = maxEnemiesInZone;
        invasionSlider.value = 0;
    }
}


