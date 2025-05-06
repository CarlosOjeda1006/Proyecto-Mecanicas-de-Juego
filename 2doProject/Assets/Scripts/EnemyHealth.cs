using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void DamageEnemy(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
