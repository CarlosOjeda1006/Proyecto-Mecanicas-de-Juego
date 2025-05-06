using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed, lifeTime;
    public Rigidbody theRigidbody;

    public int damage;

    public bool damageEnemy, damagePlayer;
    void Start()
    {
        
    }
    void Update()
    {
        theRigidbody.linearVelocity = transform.forward * bulletSpeed;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
        }

        if(other.gameObject.tag == "Player" && damagePlayer) 
        {
            PlayerHealth.instance.DamagePlayer(damage);
        }

        Destroy(gameObject);
    }
}
