using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int maxHealth, currentHealth;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;       
        UI.instance.healthSlider.maxValue = maxHealth;
        UI.instance.healthSlider.value = currentHealth;
        UI.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);

            currentHealth = 0;

            GameManager.instance.PlayerDeath();
        }

        UI.instance.healthSlider.value = currentHealth;
        UI.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }

    public void HealthPlayer(int heal)
    {
        currentHealth += heal;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UI.instance.healthSlider.value = currentHealth;
        UI.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }
}
