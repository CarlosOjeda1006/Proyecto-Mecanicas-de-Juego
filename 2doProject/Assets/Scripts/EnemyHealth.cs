using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject ammoPickUp;
    public int currentHealth;
    private Vector3 DeathPos;
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
            DeathPos = transform.position;
            Instantiate(ammoPickUp, DeathPos, Quaternion.identity);
            // Destroy the enemy object
            Destroy(gameObject);
        }
    }
}
