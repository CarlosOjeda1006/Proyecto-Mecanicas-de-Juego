using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float lifeTime = 5f;
    public Rigidbody theRigidbody;

    public int damage = 10;

    public bool damageEnemy = true;
    public bool damagePlayer = false;

    void Start()
    {
        theRigidbody.linearVelocity = transform.forward * bulletSpeed;
        Invoke("DestroySelf", lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && damageEnemy)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null) enemy.DamageEnemy(damage);
        }

        if (other.CompareTag("Player") && damagePlayer)
        {
            if (PlayerHealth.instance != null)
                PlayerHealth.instance.DamagePlayer(damage);
        }

        Destroy(gameObject);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}

